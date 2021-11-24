using System;
using System.Collections.Generic;
using Gameplay.Capabilities;
using Gameplay.Capabilities.CapabilityProps;
using Gameplay.Decks;
using Gameplay.Input;
using Gameplay.Melee;
using Gameplay.StateMachine;
using Gameplay.StateMachine.ActionsAndStates;
using UnityEngine;

namespace Gameplay.Boneco
{
    public class Boneco : MonoBehaviour
    {
        public DeckManager deckManager;

        #region StatesAndActions fields

        public StateMachine.StateMachine stateMachine;
        public StateBroadcast      stateBroadcast;
        public ActionBroadcast     actionBroadcast;
        public IdleState                 idleState;
        public GroundedState             groundedState;
        public RunningState              runningState;
        public CrouchState               crouchState;

        #endregion

        #region GenericBroadCasters fields

        public BonecoMovementCapabilityProps bonecoMovementCapabilityProps;
        public BonecoController              bonecoController;
        public InputBroadcaster              inputBroadcaster;

        #endregion 
        
        #region Capabilities fields

        public List<Capability>    capabilities;
        public JumpCapability      jumpCapability;
        public JumpCapabilityProps jumpCapabilityProps;
        public MeleeCapability     meleeCapability;

        #endregion

        #region Melee fields

        public MeleePoolManager meleePoolManager;

        #endregion
        
        private void Awake()
        {
            deckManager = GetComponent<DeckManager>();
            capabilities = new List<Capability>();
        }

        private void Start()
        {
            DeckSetup();
            bonecoController = GetComponent<BonecoController>();
            CalculatePropsValues();
            
            jumpCapability.Initialize(this);
            capabilities.Add(jumpCapability);
            
            meleeCapability.Initialize(this);
            capabilities.Add(meleeCapability);
        }

        void Update()
        {
            CalculateVelocity();
            bonecoController.Move(bonecoMovementCapabilityProps.velocity * Time.deltaTime, inputBroadcaster.NewInputDirection);
           
            ResetJumpCount();
            
            if (IsGroundedNotRunning() && !IsCrouched())
            {
                stateMachine.SetState(idleState);
            }
            
            if (IsGroundedRunning())
            {
                stateMachine.SetState(runningState);
            }
            
            if (HasVerticalCollisions())
            {
                bonecoMovementCapabilityProps.velocity.y = 0;
            }

            if (IsCrouched())
            {
                stateMachine.SetState(crouchState);
            }
        }

        #region Inputs
        
        public void OnJumpInputUp()
        {
            jumpCapability.VariableJumpHeight();
        }
        
        public void OnJumpButtontDown()
        {
            StartCoroutine(jumpCapability.EnterCapabilityZ());
        }
        
        public void OnMeleeButtonDown()
        {
            if (meleeCapability.CanUse())
            {
                UseCapability(meleeCapability);    
            }
        }
        
        #endregion

        private void CalculateVelocity() {
            bonecoMovementCapabilityProps.cachedTargetVelocityX = inputBroadcaster.NewInputDirection.x * bonecoMovementCapabilityProps.moveSpeed;
            bonecoMovementCapabilityProps.velocity.x = Mathf.SmoothDamp (bonecoMovementCapabilityProps.velocity.x, bonecoMovementCapabilityProps.cachedTargetVelocityX, ref bonecoMovementCapabilityProps.velocityXSmoothing, (bonecoController.collisions.below)?bonecoMovementCapabilityProps.accelerationTimeGrounded:bonecoMovementCapabilityProps.accelerationTimeAirborne);
            bonecoMovementCapabilityProps.velocity.y += bonecoMovementCapabilityProps.gravity * Time.deltaTime;
        }

        void UseCapability(Capability monobehaviourCapability)
        {
            monobehaviourCapability.StartCapability();
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

        #region Setups
        void CalculatePropsValues()
        {
            bonecoMovementCapabilityProps.positiveGravityCache = bonecoMovementCapabilityProps.gravity = -(2 * jumpCapabilityProps.maxJumpHeight) / Mathf.Pow (jumpCapabilityProps.timeToJumpApex, 2);
            bonecoMovementCapabilityProps.negativeGravityCache = Math.Abs(bonecoMovementCapabilityProps.positiveGravityCache);
            
            jumpCapabilityProps.wallJumpOff.y = jumpCapabilityProps.maxJumpVelocity = Mathf.Abs(bonecoMovementCapabilityProps.gravity) * jumpCapabilityProps.timeToJumpApex;
            jumpCapabilityProps.minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (bonecoMovementCapabilityProps.gravity) * jumpCapabilityProps.minJumpHeight);  
        }
        
        private void DeckSetup()
        {
            inputBroadcaster           = deckManager.GetNewInputBroadcaster();
            bonecoMovementCapabilityProps = deckManager.GetPlayerMovementCapabilityProps();
            
            jumpCapability = deckManager.GetJumpCapability(gameObject);
            jumpCapability.Initialize(this);
            jumpCapabilityProps = deckManager.GetJumpCapabilityProps();
            
            meleeCapability = deckManager.GetMeleeCapability(gameObject);
            meleeCapability.Initialize(this);
 
            stateMachine            = deckManager.GetStateMachine(gameObject);
            stateBroadcast          = deckManager.GetStateBroadcast();
            actionBroadcast         = deckManager.GetBonecoActionBroadcast();
            groundedState = deckManager.GetGroundedState(gameObject);
            runningState  = deckManager.GetRunningState(gameObject);
            idleState     = deckManager.GetIdleState(gameObject);
            crouchState   = deckManager.GetCrouchState(gameObject);

            meleePoolManager = deckManager.GetNewMeleePoolManager(gameObject);
        }
        
        #endregion

        #region SomeChecks
        
        bool IsGroundedRunning()
        {
            return (bonecoMovementCapabilityProps.grounded && IsRunning() && !IsCrouched());
        }
        
        bool IsGroundedNotRunning()
        {
            return (bonecoMovementCapabilityProps.grounded && !IsRunning());
        }

        bool IsRunning()
        {
            return (inputBroadcaster.NewInputDirection.x != 0);
        }
        
        bool IsGrounded()
        {
            return (bonecoController.collisions.below);
        }
        
        bool HasVerticalCollisions()
        {
            return (bonecoController.collisions.above || bonecoController.collisions.below);
        }

        bool IsCrouched()
        {
            return (IsDownInputting() && IsGrounded());
        }

        bool IsDownInputting()
        {
            return (inputBroadcaster.NewInputDirection.y < 0);
        }

        #endregion
    }
}
