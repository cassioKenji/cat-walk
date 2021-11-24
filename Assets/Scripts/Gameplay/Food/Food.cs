using System;
using System.Collections.Generic;
using Gameplay.Capabilities;
using Gameplay.Decks;
using Gameplay.Melee;
using Gameplay.StateMachine;
using Gameplay.StateMachine.ActionsAndStates;
using UnityEngine;

namespace Gameplay.Food
{
    public class Food : MonoBehaviour
    {
        public DeckManager _deckManager;
        
        [SerializeField]
        private FoodMovementProps foodMovementMovementCapabilityProps;
        
        [SerializeField]
        private FoodController _foodController;

        [SerializeField]
        private StateMachine.StateMachine _stateMachine;
        
        [SerializeField]
        private StateBroadcast _foodStateBroadcast;

        [SerializeField]
        private FallingState _fallingState;
        
        [SerializeField]
        private BouncedState _bouncedState;
        
        [SerializeField]
        private PlatedState _platedState;

        [SerializeField] 
        private WastedState _wastedState;
        
        [SerializeField] 
        private RecycledState _recycledState;
        
        [SerializeField]
        private GroundedState _groundedState;
        
        [SerializeField]
        private HitToRightState _hitToRightState;
        
        [SerializeField]
        private HitToLeftState _hitToLeftState;
        
        [SerializeField]
        private NeverHittedState _neverHittedState;

        [SerializeField]
        private Vector2 _directionToMove = Vector2.zero;

        [SerializeField] 
        private float xVelocityToMove = 0f;
        
        [SerializeField] 
        private float yVelocityToMove = 0f;

        [SerializeField]
        private Dictionary<HitStrength, float> _hitStrengths; // = new Dictionary<HitStrength, float>();
        
        #region Capabilities fields

        public List<Capability>       capabilities;
        public BounceOnWallCapability bounceOnWallCapability;

        #endregion

        private const String BandejaLabel = "Bandeja";
        private const String RecycleLabel = "Recycle";

        private Food _thisFoodComponent;

        [SerializeField]
        private GameObject _myStaticVersion;

        private void Awake()
        {
            _deckManager = GetComponent<DeckManager>();
            capabilities = new List<Capability>();
        }

        private void Start()
        {
            _thisFoodComponent = GetComponent<Food>();
            //_myStaticVersion = FoodPoolManager.Instance.Get();
            
            _stateMachine = _deckManager.GetStateMachine(gameObject);
            foodMovementMovementCapabilityProps = _deckManager.GetFoodMovementProps();
            _foodStateBroadcast = _deckManager.GetStateBroadcast();
            _stateMachine = _deckManager.GetStateMachine(gameObject);
            _fallingState = _deckManager.GetFallingState(gameObject);
            _groundedState = _deckManager.GetGroundedState(gameObject);
            _hitToRightState = _deckManager.GetHitToRightState(gameObject);
            _hitToLeftState = _deckManager.GetHitToLeftState(gameObject);
            _neverHittedState = _deckManager.GetNeverHittedState(gameObject);
            _bouncedState = _deckManager.GetBouncedState(gameObject);
            _platedState = _deckManager.GetPlatedState(gameObject);
            _wastedState = _deckManager.GetWastedState(gameObject);
            _recycledState = _deckManager.GetRecycledState(gameObject);
            
            _foodController = GetComponent<FoodController>();
            
            CalculatePropsValues();
            
            _hitStrengths = new Dictionary<HitStrength, float>();
            HitStrenghtDictSetup();

            bounceOnWallCapability = _deckManager.GetBounceOnWallCapability(gameObject);
            
            bounceOnWallCapability.Initialize(this);
            capabilities.Add(bounceOnWallCapability);
        }

        private void Update()
        {
            if(IsFreshNewFood())
            {
                _stateMachine.SetState(_neverHittedState);
            }

            if (HasVerticalCollisions())
            {
                foodMovementMovementCapabilityProps.velocity.y = 0;
            }

            if (_foodStateBroadcast.state != States.Plated || _foodStateBroadcast.state != States.Recycled)
            {
                CalculateVelocity();
                _foodController.Move(foodMovementMovementCapabilityProps.velocity * Time.deltaTime, _directionToMove);
            }
            
            StopFallingIfHitToTheTheLeftOrToRight();
            
            if (HasHorizontalCollisions())
            {
                xVelocityToMove = 0;
            }

            if (HasBouncedInTheWall())
            {
                if (_foodStateBroadcast.state == States.HitToRight)
                {
                    _foodController.collisionMask = LayerMask.GetMask(BandejaLabel);
                }
                
                if (_foodStateBroadcast.state == States.HitToLeft)
                {
                    _foodController.collisionMask = LayerMask.GetMask(RecycleLabel);
                }
                
                _stateMachine.SetState(_bouncedState);
                UseCapability(bounceOnWallCapability);
            }

            if (IsPlated())
            {
                SetPlatedWatedOrRecycled(_platedState, BandejaLabel);
            }

            if (IsWasted())
            {
                SetPlatedWatedOrRecycled(_wastedState);
            }

            if (IsRecycled())
            {
                SetPlatedWatedOrRecycled(_recycledState, RecycleLabel);
            }
        }

        private void SetPlatedWatedOrRecycled(IAnythingState state, String layerLabel)
        {
            SetPlatedWatedOrRecycled(state);
            
            //TODO research a better way to change layer instead of using magic strings
            gameObject.layer = LayerMask.NameToLayer(layerLabel);
        }
        
        private void SetPlatedWatedOrRecycled(IAnythingState state)
        {
            _stateMachine.SetState(state);
            foodMovementMovementCapabilityProps.velocity.y = 0;
            foodMovementMovementCapabilityProps.xVelocityToMove = 0;
            _thisFoodComponent.enabled = false;
        }

        public void GetHit(float input, int faceDir, HitStrength hitStrength)
        {
            if (_foodStateBroadcast.state == States.NeverHitted)
            {
                //TODO remember to implement a better juice in here
                //gameObject.transform.Rotate(0,0, UnityEngine.Random.Range(-4,4));
                
                foodMovementMovementCapabilityProps.velocity.y = 0;
                CalculateHittedVelocity(input, faceDir, hitStrength);   
            }
        }

        void CalculateHittedVelocity(float inputDirection, int faceDir, HitStrength hitStrength)
        {
            var newXVelocityToMove = _hitStrengths[hitStrength];
            
            //TODO logic to get velocity to move is not Food's responsability 
            if (IsHitingToTheRight(inputDirection, faceDir))
            {
                _stateMachine.SetState(_hitToRightState);
                foodMovementMovementCapabilityProps.xVelocityToMove = Mathf.Abs(newXVelocityToMove);
            }
            else if (IsHitingToTheLeft(inputDirection, faceDir))
            {
                _stateMachine.SetState(_hitToLeftState);
                foodMovementMovementCapabilityProps.xVelocityToMove = newXVelocityToMove;
            }

            foodMovementMovementCapabilityProps.currentXVelocity = foodMovementMovementCapabilityProps.xVelocityToMove;
        }

        private HitStrength GetHitStrenghtVelocity(HitStrength hitStrength)
        {
            return hitStrength;
        }

        void CalculateVelocity()
        { 
            foodMovementMovementCapabilityProps.cachedTargetVelocityX = _directionToMove.x * foodMovementMovementCapabilityProps.moveSpeed;
            foodMovementMovementCapabilityProps.velocity.x = foodMovementMovementCapabilityProps.xVelocityToMove; //TODO adjust by the hit strenght: slow if punch, fast if kick; maybe;
            foodMovementMovementCapabilityProps.velocity.y += foodMovementMovementCapabilityProps.gravity * Time.deltaTime;
            
            if (foodMovementMovementCapabilityProps.velocity.y < foodMovementMovementCapabilityProps.maxGravity)
            {
                foodMovementMovementCapabilityProps.velocity.y = foodMovementMovementCapabilityProps.maxGravity;
            }
        }

        void CalculatePropsValues()
        {
            foodMovementMovementCapabilityProps.positiveGravityCache =
                foodMovementMovementCapabilityProps.gravity = -177;
            foodMovementMovementCapabilityProps.negativeGravityCache =
                Math.Abs(foodMovementMovementCapabilityProps.positiveGravityCache);
        }


        


        #region SomeChecks
        
        bool HasVerticalCollisions()
        {
            return (_foodController.collisions.above || _foodController.collisions.below);
        }
        
        bool HasHorizontalCollisions()
        {
            return (_foodController.collisions.left || _foodController.collisions.right);
        }
        
        bool IsGrounded()
        {
            return (_foodController.collisions.below);
        }

        bool IsHitingToTheRight(float inputDirection, int faceDir)
        {
            return (inputDirection > 0 || faceDir > 0);
        }

        bool IsHitingToTheLeft(float inputDirection, int faceDir)
        {
            return (inputDirection < 0 || faceDir < 0);
        }

        bool IsFreshNewFood()
        {
            return ((_foodStateBroadcast.was != States.HitToLeft || _foodStateBroadcast.was != States.HitToRight ||
                     _foodStateBroadcast.was != States.None)
                    && _foodStateBroadcast.state == States.None);
        }

        void StopFallingIfHitToTheTheLeftOrToRight()
        {
            if (_foodStateBroadcast.state == States.HitToLeft || _foodStateBroadcast.state == States.HitToRight)
            {
                foodMovementMovementCapabilityProps.velocity.y = 0;
            }
        }

        bool HasBouncedInTheWall()
        {
            if (HasHorizontalCollisions() && (_foodStateBroadcast.state == States.HitToLeft ||
                                              _foodStateBroadcast.state == States.HitToRight))
            {
                return true;
            }
            return false;
        }

        bool IsPlated()
        {
            return (_foodStateBroadcast.was == States.HitToRight && _foodStateBroadcast.state == States.Bounced && IsGrounded());
        }
        
        bool IsRecycled()
        {
            return (_foodStateBroadcast.was == States.HitToLeft && _foodStateBroadcast.state == States.Bounced &&
                    IsGrounded());
        }

        bool IsWasted()
        {
            return (_foodStateBroadcast.was == States.None && _foodStateBroadcast.state == States.NeverHitted &&
                    IsGrounded());
        }

        #endregion
        
        private void HitStrenghtDictSetup()
        {
            _hitStrengths.Add(HitStrength.Normal, foodMovementMovementCapabilityProps.hittedMoveSpeed);
            _hitStrengths.Add(HitStrength.Fierce, foodMovementMovementCapabilityProps.hardHittedMoveSpeed);
        }
        
        void UseCapability(Capability monobehaviourCapability)
        {
            monobehaviourCapability.StartCapability();
        }
    }
}
