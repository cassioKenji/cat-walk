using System;
using System.Collections.Generic;
using Gameplay.Capabilities;
using Gameplay.Decks;
using Gameplay.Food.Plate;
using Gameplay.Kitchen;
using Gameplay.Melee;
using Gameplay.StateMachine;
using Gameplay.StateMachine.ActionsAndStates;
using UnityEngine;

namespace Gameplay.Food
{
    public class DynamicFood : Food
    {
        private bool _isStatic = false;
        public new bool IsStatic
        {
            get => _isStatic;
        }

        [SerializeField]
        private KitchenManager _kitchenManager;

        [SerializeField]
        private DeckManager _kitchenManagerDeckManager;

        [SerializeField] 
        private FoodPoolManager _foodPool;
        
        [SerializeField] 
        private FoodPoolManager _staticFoodPool;
        
        [SerializeField]
        private DeckManager _deckManager;
        
        [SerializeField]
        private FoodMovementProps _foodMovementMovementCapabilityProps;
        
        [SerializeField]
        private FoodController _foodController;

        [SerializeField] 
        public FoodType foodType;

        #region States
        
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

        #endregion

        [SerializeField]
        private Vector2 _directionToMove = Vector2.zero;

        [SerializeField] 
        private float xVelocityToMove = 0f;
        
        [SerializeField] 
        private float yVelocityToMove = 0f;

        [SerializeField]
        private Dictionary<HitStrength, float> _hitStrengths;
        
        #region Capabilities fields

        public List<Capability>       capabilities;
        public BounceOnWallCapability bounceOnWallCapability;

        #endregion

        private const String PlateLabel = "Plate";
        private const String RecycleLabel = "Recycle";

        private Food _thisFoodComponent;

        private void Awake()
        {
            _deckManager = GetComponent<DeckManager>();
            capabilities = new List<Capability>();
        }

        private void Start()
        {
            _thisFoodComponent = GetComponent<DynamicFood>();
            DeckSetup();

            _foodController = GetComponent<FoodController>();
            
            CalculatePropsValues();
            
            _hitStrengths = new Dictionary<HitStrength, float>();
            HitStrenghtDictSetup();

            bounceOnWallCapability = _deckManager.GetBounceOnWallCapability(gameObject);
            
            bounceOnWallCapability.Initialize(this);
            capabilities.Add(bounceOnWallCapability);

            _kitchenManager = FindObjectOfType<KitchenManager>();
            _kitchenManagerDeckManager = _kitchenManager.DeckManager;
            _foodPool = _kitchenManagerDeckManager.GetHamburgerBunBottomPoolManager(gameObject);
            _staticFoodPool = _kitchenManagerDeckManager.GetStaticHamburgerBunBottomPoolManager(gameObject);
        }

        private void Update()
        {
            if(IsFreshNewFood())
            {
                _stateMachine.SetState(_neverHittedState);
            }

            if (HasVerticalCollisions())
            {
                _foodMovementMovementCapabilityProps.velocity.y = 0;
            }

            if (_foodStateBroadcast.state != States.Plated || _foodStateBroadcast.state != States.Recycled)
            {
                CalculateVelocity();
                _foodController.Move(_foodMovementMovementCapabilityProps.velocity * Time.deltaTime, _directionToMove);
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
                    _foodController.collisionMask = LayerMask.GetMask(PlateLabel);
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
                SetPlatedWatedOrRecycled(_platedState, PlateLabel);
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
            _foodMovementMovementCapabilityProps.velocity.y = 0;
            _foodMovementMovementCapabilityProps.xVelocityToMove = 0;
            DisableFood();
        }

        public void GetHit(float input, int faceDir, HitStrength hitStrength)
        {
            if (_foodStateBroadcast.state == States.NeverHitted)
            {
                //TODO remember to implement a better juice in here
                //gameObject.transform.Rotate(0,0, UnityEngine.Random.Range(-4,4));
                
                _foodMovementMovementCapabilityProps.velocity.y = 0;
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
                _foodMovementMovementCapabilityProps.xVelocityToMove = Mathf.Abs(newXVelocityToMove);
            }
            else if (IsHitingToTheLeft(inputDirection, faceDir))
            {
                _stateMachine.SetState(_hitToLeftState);
                _foodMovementMovementCapabilityProps.xVelocityToMove = newXVelocityToMove;
            }

            _foodMovementMovementCapabilityProps.currentXVelocity = _foodMovementMovementCapabilityProps.xVelocityToMove;
        }

        private HitStrength GetHitStrenghtVelocity(HitStrength hitStrength)
        {
            return hitStrength;
        }

        void CalculateVelocity()
        { 
            _foodMovementMovementCapabilityProps.cachedTargetVelocityX = _directionToMove.x * _foodMovementMovementCapabilityProps.moveSpeed;
            _foodMovementMovementCapabilityProps.velocity.x = _foodMovementMovementCapabilityProps.xVelocityToMove; //TODO adjust by the hit strenght: slow if punch, fast if kick; maybe;
            _foodMovementMovementCapabilityProps.velocity.y += _foodMovementMovementCapabilityProps.gravity * Time.deltaTime;
            
            if (_foodMovementMovementCapabilityProps.velocity.y < _foodMovementMovementCapabilityProps.maxGravity)
            {
                _foodMovementMovementCapabilityProps.velocity.y = _foodMovementMovementCapabilityProps.maxGravity;
            }
        }

        void CalculatePropsValues()
        {
            _foodMovementMovementCapabilityProps.positiveGravityCache =
                _foodMovementMovementCapabilityProps.gravity = -177;
            _foodMovementMovementCapabilityProps.negativeGravityCache =
                Math.Abs(_foodMovementMovementCapabilityProps.positiveGravityCache);
        }

        public override void EnableFood()
        {
            _thisFoodComponent.enabled = true;
        }
        
        public override void DisableFood()
        {
            _thisFoodComponent.enabled = false;
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
                _foodMovementMovementCapabilityProps.velocity.y = 0;
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
            _hitStrengths.Add(HitStrength.Normal, _foodMovementMovementCapabilityProps.hittedMoveSpeed);
            _hitStrengths.Add(HitStrength.Fierce, _foodMovementMovementCapabilityProps.hardHittedMoveSpeed);
        }
        
        void UseCapability(Capability monobehaviourCapability)
        {
            monobehaviourCapability.StartCapability();
        }
        
        lksadjlkasdjl //TODO foi mais ou menos aqui q parei
        private void OnTriggerEnter2D(Collider2D hittedGOCollider2D)
        {
            //TODO aqui a comida que tocou o objeto abaixo vai ter de se substituir  ao detectar se tocou uma comida estatica ou se tocou o Plate.
            //o bloco abaixo não é elegante pois tem diversos ifs. só os nomes das variáveis já são um smell. estudar uma abordagem mais OOP.
            //é provavel que este bloco nao seja atualizado num futuro proximo, entao se lembre sempre que comentarios mentem.
            
            //GameObject thingThatIFell = hittedGOCollider2D.gameObject;
            //Debug.Log(thingThatIFell.name);
            //var componentFromThingThatIFell = thingThatIFell.GetComponent<Food>();
            
            //if (thingThatIFell == null)
            //{
            //    return;
            //}

            //if (_foodController.collisionMask == 8)
            //{
            //    var cachedStaticFood = _staticFoodPool.Get();
            //    cachedStaticFood.transform.position = thingThatIFell.transform.position;
                //_cachedStaticFood.transform.parent = gameObject.transform;
            //    cachedStaticFood.SetActive(true);
            
            //    componentFromThingThatIFell.EnableFood();
            //    _foodPool.ReturnToPool(thingThatIFell);
            //}
            //transformFromFoodThaFellAboveMe.parent = gameObject.transform;
        }

        private void DeckSetup()
        {
            _stateMachine       = _deckManager.GetStateMachine(gameObject);
            _stateMachine       = _deckManager.GetStateMachine(gameObject);
            _fallingState       = _deckManager.GetFallingState(gameObject);
            _groundedState      = _deckManager.GetGroundedState(gameObject);
            _bouncedState       = _deckManager.GetBouncedState(gameObject);
            _platedState        = _deckManager.GetPlatedState(gameObject);
            _wastedState        = _deckManager.GetWastedState(gameObject);
            _hitToLeftState     = _deckManager.GetHitToLeftState(gameObject);
            _hitToRightState    = _deckManager.GetHitToRightState(gameObject);
            _recycledState      = _deckManager.GetRecycledState(gameObject);
            _neverHittedState   = _deckManager.GetNeverHittedState(gameObject);
            _foodStateBroadcast = _deckManager.GetStateBroadcast();
            _foodMovementMovementCapabilityProps = _deckManager.GetFoodMovementProps();
        }
    }
}
