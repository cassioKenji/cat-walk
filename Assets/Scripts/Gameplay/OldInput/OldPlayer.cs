using System;
using System.Collections.Generic;
using Gameplay.Capabilities;
using Gameplay.Capabilities.CapabilityProps;
using Gameplay.Decks;
using Gameplay.NewInput.Decks;
using Gameplay.StateMachine;
using Gameplay.StateMachine.ActionsAndStates;
using UnityEngine;

namespace Gameplay.OldInput
{
    public class OldPlayer : MonoBehaviour
    {
        public DeckManager deckManager;
        
        public JumpCapabilityProps jumpCapabilityProps;
        public MeleeCapabilityProps meleeCapabilityProps;
        public RoofWalkCapabilityProps roofWalkCapabilityProps;
        public WallSlideProps wallSlideProps;
        
        public BonecoMovementCapabilityProps bonecoMovementCapabilityProps;
        public OldInputBroadCasterScriptableObject oldInputBroadCasterScriptableObject;

        public StateMachine.StateMachine stateMachine;
        
        Controller2D controller2D;

        public JumpingState jumpingState;
        public GroundedState groundedState;
        public RunningState runningState;
        public CrouchState crouchState;
        
        public List<Capability> capabilities;
        public List<Capability> Capabilities { get { return capabilities; } }

        private JumpCapability _jumpCapability;
        private MeleeCapability _meleeCapability;
        private WallSlideCapability _wallSlideCapability;
        private RoofWalkCapability _roofWalkCapability;
        private RoofRunningState _roofRunningState;
        private RoofedState _roofedState;
        private WallSlideState _wallSlideState;

        private SpriteRenderer _spriteRenderer;

        public StateBroadcast stateBroadcast;

        private void Awake()
        {
            deckManager = GetComponent<DeckManager>();
            bonecoMovementCapabilityProps = deckManager.GetPlayerMovementCapabilityProps();
            
            stateMachine = GetComponent<StateMachine.StateMachine>();

            capabilities = new List<Capability>();
            InitializeCapabilities();
        }

        void Start()
        {
            controller2D = GetComponent<Controller2D>();

            jumpingState = GetComponent<JumpingState>();
            groundedState = GetComponent<GroundedState>();
            runningState = GetComponent<RunningState>();
            crouchState = GetComponent<CrouchState>();
            _roofRunningState = GetComponent<RoofRunningState>();
            _roofedState = GetComponent<RoofedState>();
            _wallSlideState = GetComponent<WallSlideState>();

            bonecoMovementCapabilityProps.positiveGravityCache = bonecoMovementCapabilityProps.gravity = -(2 * jumpCapabilityProps.maxJumpHeight) / Mathf.Pow (jumpCapabilityProps.timeToJumpApex, 2);
            bonecoMovementCapabilityProps.negativeGravityCache = Math.Abs(bonecoMovementCapabilityProps.positiveGravityCache);
            
            jumpCapabilityProps.wallJumpOff.y = jumpCapabilityProps.maxJumpVelocity = Mathf.Abs(bonecoMovementCapabilityProps.gravity) * jumpCapabilityProps.timeToJumpApex;
            jumpCapabilityProps.minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (bonecoMovementCapabilityProps.gravity) * jumpCapabilityProps.minJumpHeight);

            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            ResetJumpCount();
            CalculateVelocity();
            CheckRoofCollisions();

            if (bonecoMovementCapabilityProps.grounded)
            {
                stateMachine.SetState(groundedState);
            }
            
            if (IsGroundedRunning())
            {
                stateMachine.SetState(runningState);
            }

            if(IsCrouched()){
                stateMachine.SetState(crouchState);
            }

            if (IsTouchingRoof() 
                && (stateBroadcast.was == States.Jumping || stateBroadcast.state == States.Jumping) 
                && (oldInputBroadCasterScriptableObject.direction == Direction.Up ||
                    oldInputBroadCasterScriptableObject.direction == Direction.UpLeft ||
                    oldInputBroadCasterScriptableObject.direction == Direction.UpRight))
            {
               StartCoroutine(_roofWalkCapability.EnterCapability());
            }

            if (((stateBroadcast.state == States.Roofed) || (stateBroadcast.state == States.RoofRunning)) &&  !IsRunning())
            {
                stateMachine.SetState(_roofedState);
            }

            if (IsRoofedRunning())
            {
                stateMachine.SetState(_roofRunningState);
            }

            if (IsWallSliding())
            {
                stateMachine.SetState(_wallSlideState);
            }

            HandleWallSliding ();

            controller2D.Move (bonecoMovementCapabilityProps.velocity * Time.deltaTime, oldInputBroadCasterScriptableObject.playerDirectionalInput);

            if (HasVerticalCollisions()) {
                if (IsSlidingDownMaxSlope()) {
                    //stateMachine.SetState(isSlidingDownMaxSlopeState);
                    bonecoMovementCapabilityProps.velocity.y += controller2D.collisions.slopeNormal.y * -bonecoMovementCapabilityProps.gravity * Time.deltaTime;
                } else {
                    bonecoMovementCapabilityProps.velocity.y = 0;
                }
            }

            UpdatePlayerPosition();
        }

        public void SetDirectionalInput(Vector2 input)
        {
            oldInputBroadCasterScriptableObject.playerDirectionalInput = input;
        }

        public void OnJumpInputUp()
        {
            _jumpCapability.VariableJumpHeight();
        }

        public void OnJumpInputDown()
        {
            if (CanUseJump()){
                StartCoroutine(_jumpCapability.EnterCapabilityZ());
            }
        }

        public void OnMeleeButtonDown()
        {
            if (_meleeCapability.CanUse())
            {
                UseCapability(_meleeCapability);    
            }
        }

        public void OnJumpInputDownSebastian() {
            if (bonecoMovementCapabilityProps.wallSliding) {
                if (bonecoMovementCapabilityProps.wallDirX == oldInputBroadCasterScriptableObject.playerDirectionalInput.x) {
                    bonecoMovementCapabilityProps.velocity.x = -bonecoMovementCapabilityProps.wallDirX * jumpCapabilityProps.wallJumpClimb.x;
                    bonecoMovementCapabilityProps.velocity.y = jumpCapabilityProps.wallJumpClimb.y;
                }
                else if (oldInputBroadCasterScriptableObject.playerDirectionalInput.x == 0) {
                    bonecoMovementCapabilityProps.velocity.x = -bonecoMovementCapabilityProps.wallDirX * jumpCapabilityProps.wallJumpOff.x;
                    bonecoMovementCapabilityProps.velocity.y = jumpCapabilityProps.wallJumpOff.y;
                }
                else {
                    bonecoMovementCapabilityProps.velocity.x = -bonecoMovementCapabilityProps.wallDirX * bonecoMovementCapabilityProps.wallLeap.x;
                    bonecoMovementCapabilityProps.velocity.y = bonecoMovementCapabilityProps.wallLeap.y;
                }
            }
            
            if (IsGrounded()) {
                if (IsSlidingDownMaxSlope()) {
                    if (oldInputBroadCasterScriptableObject.playerDirectionalInput.x != -Mathf.Sign (controller2D.collisions.slopeNormal.x)) { // not jumping against max slope
                        bonecoMovementCapabilityProps.velocity.y = jumpCapabilityProps.maxJumpVelocity * controller2D.collisions.slopeNormal.y;
                        bonecoMovementCapabilityProps.velocity.x = jumpCapabilityProps.maxJumpVelocity * controller2D.collisions.slopeNormal.x;
                    }
                } else {
                    bonecoMovementCapabilityProps.velocity.y = jumpCapabilityProps.maxJumpVelocity;
                }
            }
        }

        void UpdatePlayerPosition()
        {
            bonecoMovementCapabilityProps.position = this.transform.position;
        }

        bool IsGrounded()
        {
            return (controller2D.collisions.below);
        }

        bool IsTouchingRoof()
        {
            return (controller2D.collisions.above);
        }

        bool IsCrouched()
        {
            return (bonecoMovementCapabilityProps.grounded && (oldInputBroadCasterScriptableObject.direction == Direction.Down ||
                                                              oldInputBroadCasterScriptableObject.direction == Direction.DownRight ||
                                                              oldInputBroadCasterScriptableObject.direction == Direction.DownLeft));
        }

        bool IsGroundedRunning()
        {
            return (bonecoMovementCapabilityProps.grounded && IsRunning());
        }

        bool IsRunning()
        {
            return (oldInputBroadCasterScriptableObject.playerDirectionalInput.x != 0);
        }

        bool IsRoofedRunning()
        {
            return (stateBroadcast.state == States.Roofed && IsRunning());
        }

        public void OnJumpInputUpSebastian() {
            if (bonecoMovementCapabilityProps.velocity.y > jumpCapabilityProps.minJumpVelocity) {
                bonecoMovementCapabilityProps.velocity.y = jumpCapabilityProps.minJumpVelocity;
            }
        }

        void CheckRoofCollisions()
        {
            bonecoMovementCapabilityProps.touchingRoof = IsTouchingRoof();
        }

        void ResetJumpCount()
        {
            bonecoMovementCapabilityProps.grounded = IsGrounded();

            if (!bonecoMovementCapabilityProps.grounded)
            {
                jumpCapabilityProps.canDoubleJump = true;
            }
            else
            {
                jumpCapabilityProps.canDoubleJump = false;
                jumpCapabilityProps.airJump = 1;
            }
        }

        void HandleWallSliding() {
            bonecoMovementCapabilityProps.wallDirX = (controller2D.collisions.left) ? -1 : 1;
            bonecoMovementCapabilityProps.wallSliding = false;
            if (IsWallSliding()) {
                
                UseCapability(_wallSlideCapability);
                
            }
        }

        bool IsWallSliding()
        {
            return ((ThereIsLeftCollision() ||
                     ThereIsRightCollision()) && 
                    !ThereIsBelowCollision() && bonecoMovementCapabilityProps.velocity.y < 0);// && 
                    //inputBroadCasterScriptableObject.playerDirectionalInput.x == wallDirX);
        }

        bool ThereIsLeftCollision()
        {
            return (controller2D.collisions.left);
        }
        
        bool ThereIsRightCollision()
        {
            return (controller2D.collisions.right);
        }
        
        bool ThereIsBelowCollision()
        {
            return (controller2D.collisions.below);
        }

        void CalculateVelocity() {
            bonecoMovementCapabilityProps.cachedTargetVelocityX = oldInputBroadCasterScriptableObject.playerDirectionalInput.x * bonecoMovementCapabilityProps.moveSpeed;
            bonecoMovementCapabilityProps.velocity.x = Mathf.SmoothDamp (bonecoMovementCapabilityProps.velocity.x, bonecoMovementCapabilityProps.cachedTargetVelocityX, ref bonecoMovementCapabilityProps.velocityXSmoothing, (controller2D.collisions.below)?bonecoMovementCapabilityProps.accelerationTimeGrounded:bonecoMovementCapabilityProps.accelerationTimeAirborne);
            bonecoMovementCapabilityProps.velocity.y += bonecoMovementCapabilityProps.gravity * Time.deltaTime;
        }
        
        bool IsSlidingDownMaxSlope()
        {
            return (controller2D.collisions.slidingDownMaxSlope);
        }

        bool HasVerticalCollisions()
        {
            return (controller2D.collisions.above || controller2D.collisions.below);
        }

        //TODO automate this block
        void InitializeCapabilities()
        {
            _jumpCapability = GetComponent<JumpCapability>();
            _jumpCapability.Initialize(jumpCapabilityProps.coolDown, this);

            _meleeCapability = GetComponent<MeleeCapability>();
            _meleeCapability.Initialize(meleeCapabilityProps.coolDown, this);
            
            _roofWalkCapability = GetComponent<RoofWalkCapability>();
            _roofWalkCapability.Initialize(roofWalkCapabilityProps.coolDown, this);


            _wallSlideCapability = GetComponent<WallSlideCapability>();
            _wallSlideCapability.Initialize(wallSlideProps.coolDown, this);
            
            capabilities.Add(_jumpCapability);
            capabilities.Add(_meleeCapability);
            capabilities.Add(_roofWalkCapability);
            capabilities.Add(_wallSlideCapability);
        }

        void InitializeIndividualCapability(Capability monobehaviourCapability)
        {
            Capability componentType = monobehaviourCapability;
            //componentType.Initialize();
        }

        void UseCapability(Capability monobehaviourCapability)
        {
            monobehaviourCapability.StartCapability();
        }

        bool CanUseJump()
        {
            if (stateBroadcast.state == States.RoofRunning)
            {
                return false;
            }

            return true;
        }
    }
}
