using System.Collections;
using Gameplay.Capabilities.CapabilityProps;
using Gameplay.Decks;
using Gameplay.NewInput.Decks;
using Gameplay.StateMachine;
using Gameplay.StateMachine.ActionsAndStates;
using UnityEngine;

namespace Gameplay.Capabilities
{
    public class RoofWalkCapability : Capability
    {
        public DeckManager deckManager;
        
        public RoofWalkCapabilityProps _capabilityProps;
        public JumpCapabilityProps jumpCapabilityProps;
        
        private Controller2D _controller2D;
        public StateBroadcast stateBroadcast;
        public BonecoMovementCapabilityProps bonecoMovementCapabilityProps;

        public RoofedState roofedState;
        public StateMachine.StateMachine _stateMachine;

        private float lastYPosition;
        private float lastYPositionMargin = 0.08f;
        
        private void Start()
        {
            deckManager = GetComponent<DeckManager>();
            bonecoMovementCapabilityProps = deckManager.GetPlayerMovementCapabilityProps();
            
            _stateMachine = GetComponent<StateMachine.StateMachine>();
            roofedState = GetComponent<RoofedState>();
        }
        public override IEnumerator EnterCapability()
        {
            if (stateBroadcast.state == States.Roofed || stateBroadcast.state == States.RoofRunning) yield break;
            lastYPosition = character.transform.position.y + lastYPositionMargin;
            jumpCapabilityProps.airJump = 1;
            
            _stateMachine.SetState(roofedState);
            
            while ((_capabilityProps.capabilityPerformed <= _capabilityProps.capabilityDuration)) 
            {
                if((lastYPosition) < character.transform.position.y) _capabilityProps.capabilityPerformed = _capabilityProps.capabilityDuration;
                
                canUse = false;
                bonecoMovementCapabilityProps.gravity = bonecoMovementCapabilityProps.negativeGravityCache;
                
                _capabilityProps.capabilityPerformed += Time.deltaTime;

                yield return null;
            }
            
            bonecoMovementCapabilityProps.gravity = bonecoMovementCapabilityProps.positiveGravityCache;
            _stateMachine.UnsetActualState();
            canUse = true;
            _capabilityProps.capabilityPerformed = 0f;
        }
    }
}
