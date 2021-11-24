using System;
using System.Collections;
using Gameplay.Boneco;
using Gameplay.Capabilities.CapabilityProps;
using Gameplay.Decks;
using Gameplay.Input;
using Gameplay.NewInput;
using Gameplay.NewInput.Decks;
using Gameplay.OldInput;
using Gameplay.StateMachine;
using Gameplay.StateMachine.ActionsAndStates;
using UnityEngine;

namespace Gameplay.Capabilities
{
    public class JumpCapability : Capability
    {
        public JumpCapabilityProps jumpCapabilityProps;
        private BonecoController _bonecoController;
        public BonecoMovementCapabilityProps bonecoMovementCapabilityProps;
        public OldInputBroadCasterScriptableObject oldInputBroadCasterScriptableObject;

        public InputBroadcaster inputBroadcaster;

        public StateMachine.StateMachine _stateMachine;
        public JumpingState jumpingState;

        public int airJump = 1;
        private void Start()
        {
            _deckManager = GetComponent<DeckManager>();

            oldInputBroadCasterScriptableObject = _deckManager.GetInputBroadCasterScriptableObject();
            inputBroadcaster = _deckManager.GetNewInputBroadcaster();
            
            bonecoMovementCapabilityProps = _deckManager.GetPlayerMovementCapabilityProps();
            jumpCapabilityProps = _deckManager.GetJumpCapabilityProps();
            _bonecoController = GetComponent<BonecoController>();
            _stateMachine = _deckManager.GetStateMachine(gameObject);
            jumpingState = _deckManager.GetJumpJumpingState(gameObject);
        }

        public void VariableJumpHeight()
        {
            if (bonecoMovementCapabilityProps.velocity.y > jumpCapabilityProps.minJumpVelocity) {
                bonecoMovementCapabilityProps.velocity.y = jumpCapabilityProps.minJumpVelocity; 
            }
        }

        public IEnumerator EnterCapabilityZ()
        {
            if (bonecoMovementCapabilityProps.wallSliding)
            {
                if (bonecoMovementCapabilityProps.wallDirX == inputBroadcaster.NewInputDirection.x)
                {
                    bonecoMovementCapabilityProps.velocity.x = -bonecoMovementCapabilityProps.wallDirX * jumpCapabilityProps.wallJumpClimb.x;
                    bonecoMovementCapabilityProps.velocity.y = jumpCapabilityProps.wallJumpClimb.y;
                }
                else if (inputBroadcaster.NewInputDirection.x == 0)
                {
                    bonecoMovementCapabilityProps.velocity.x = -bonecoMovementCapabilityProps.wallDirX * jumpCapabilityProps.wallJumpOff.x;
                    bonecoMovementCapabilityProps.velocity.y = jumpCapabilityProps.wallJumpOff.y;
                }
                else
                {
                    bonecoMovementCapabilityProps.velocity.x = -bonecoMovementCapabilityProps.wallDirX * bonecoMovementCapabilityProps.wallLeap.x;
                    bonecoMovementCapabilityProps.velocity.y = bonecoMovementCapabilityProps.wallLeap.y;
                }
                yield return null;
                _stateMachine.SetState(jumpingState);
            }

            if (bonecoMovementCapabilityProps.grounded)
            {
                if (_bonecoController.collisions.IsSlidingDownMaxSlope())
                {
                    print("descending slope");
                    if (IsAThing() != IsSomething())
                    {
                        bonecoMovementCapabilityProps.velocity.y = jumpCapabilityProps.maxJumpVelocity * _bonecoController.collisions.slopeNormal.y;
                        bonecoMovementCapabilityProps.velocity.x = jumpCapabilityProps.maxJumpVelocity * _bonecoController.collisions.slopeNormal.x;
                    }
                }   
                else
                {
                    bonecoMovementCapabilityProps.velocity.y = jumpCapabilityProps.maxJumpVelocity;
                    jumpCapabilityProps.canDoubleJump = true;
                }
            } else if (jumpCapabilityProps.canDoubleJump && jumpCapabilityProps.airJump > 0)
            {
                // DOUBLE JUMP
                bonecoMovementCapabilityProps.velocity.y = jumpCapabilityProps.maxJumpVelocity;
                jumpCapabilityProps.airJump--;
                jumpCapabilityProps.canDoubleJump = false;
            }
            yield return null;
            _stateMachine.SetState(jumpingState);
        }

        public override void ExitCapability()
        {
            
        }

        //TODO better name for this method
        float IsAThing()
        {
            return (inputBroadcaster.NewInputDirection.x);
        }

        //TODO better name for this method
        float IsSomething()
        {
            return (-Mathf.Sign (_bonecoController.collisions.slopeNormal.x));
        }
    }
}
