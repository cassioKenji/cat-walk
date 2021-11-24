using System.Collections;
using Gameplay.Capabilities.CapabilityProps;
using Gameplay.Decks;
using Gameplay.NewInput.Decks;
using Gameplay.OldInput;
using Gameplay.StateMachine;
using Gameplay.StateMachine.ActionsAndStates;
using UnityEngine;

namespace Gameplay.Capabilities
{
    public class WallSlideCapability : Capability
    {
        public DeckManager deckManager;
        
        public JumpCapabilityProps jumpCapabilityProps;
        public WallSlideProps capabilityProps;
        public StateBroadcast stateBroadcast;
        public BonecoMovementCapabilityProps bonecoMovementCapabilityProps;
        public OldInputBroadCasterScriptableObject oldInputBroadCasterScriptableObject;
        
        public StateMachine.StateMachine stateMachine;
        public WallSlideState wallSlideState;

        private void Start()
        {
            deckManager = GetComponent<DeckManager>();
            bonecoMovementCapabilityProps = deckManager.GetPlayerMovementCapabilityProps();
            
            stateMachine = GetComponent<StateMachine.StateMachine>();
            wallSlideState = GetComponent<WallSlideState>();
        }

        public override IEnumerator EnterCapability()
        {
            bonecoMovementCapabilityProps.wallSliding = true;
            jumpCapabilityProps.airJump = 2;

            if (bonecoMovementCapabilityProps.velocity.y < -bonecoMovementCapabilityProps.wallSlideSpeedMax) {
                bonecoMovementCapabilityProps.velocity.y = -bonecoMovementCapabilityProps.wallSlideSpeedMax;
            }

            if (bonecoMovementCapabilityProps.timeToWallUnstick > 0) {
                bonecoMovementCapabilityProps.velocityXSmoothing = 0;
                bonecoMovementCapabilityProps.velocity.x = 0;

                if (oldInputBroadCasterScriptableObject.playerDirectionalInput.x != bonecoMovementCapabilityProps.wallDirX && oldInputBroadCasterScriptableObject.playerDirectionalInput.x != 0)
                {
                    DecreaseTimeToWallUnstick();
                }
                else
                {
                    ResetTimeToWallUnstick();
                }
            }
            else
            {
                ResetTimeToWallUnstick();
            }
            
            yield return null;
            stateMachine.SetState(wallSlideState);
        }

        void ResetTimeToWallUnstick()
        {
            bonecoMovementCapabilityProps.timeToWallUnstick = bonecoMovementCapabilityProps.wallStickTime;
        }

        void DecreaseTimeToWallUnstick()
        {
            bonecoMovementCapabilityProps.timeToWallUnstick -= Time.deltaTime;
        }
    }
}
