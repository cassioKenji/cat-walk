using System;
using System.Collections;
using Gameplay.Capabilities.CapabilityProps;
using Gameplay.Decks;
using Gameplay.Food;
using Gameplay.StateMachine;
using Gameplay.StateMachine.ActionsAndStates;
using UnityEngine;

namespace Gameplay.Capabilities
{
    public class BounceOnWallCapability : Capability
    {
        public DeckManager deckManager;
        
        public BounceOnWallCapabilityProps bounceOnWallCapabilityProps;
        public FoodMovementProps foodMovementProps;
        public StateBroadcast _foodStateBroadcast;
        
        public StateMachine.StateMachine stateMachine;
        public BouncedState _thisState;

        public float cachedXvelocity;

        protected override void Awake()
        {
            deckManager = GetComponent<DeckManager>();
            base.Awake();
        }

        private void Start()
        {
            bounceOnWallCapabilityProps = deckManager.GetBounceOnWallCapabilityProps();
            foodMovementProps           = deckManager.GetFoodMovementProps();
            stateMachine                = deckManager.GetStateMachine(gameObject);
            _foodStateBroadcast         = deckManager.GetStateBroadcast();
            _thisState                  = deckManager.GetBouncedState(gameObject);
        }

        public override IEnumerator EnterCapability()
        {
            foodMovementProps.velocity.y = bounceOnWallCapabilityProps.bounceYVelocity;
            stateMachine.SetState(_thisState);

            if (foodMovementProps.xVelocityToMove > 0)
            {
                foodMovementProps.xVelocityToMove = bounceOnWallCapabilityProps.bounceXVelocity;
            } else
            {
                foodMovementProps.xVelocityToMove = Math.Abs(bounceOnWallCapabilityProps.bounceXVelocity);
            }
            
            while (bounceOnWallCapabilityProps.capabilityPerformed <= bounceOnWallCapabilityProps.capabilityDuration)
            {
                canUse = false;
                foodMovementProps.xVelocityToMove += Time.deltaTime;
                bounceOnWallCapabilityProps.capabilityPerformed += Time.deltaTime;

                yield return null;
            }
            foodMovementProps.xVelocityToMove = 0;
            canUse = true;
            foodMovementProps.gravity = foodMovementProps.maxGravity = foodMovementProps.gravityAfterBounce;
        }
    }
}
