using System;
using System.Collections;
using Gameplay.Capabilities.CapabilityProps;
using Gameplay.Decks;
using Gameplay.Input;
using Gameplay.Managers;
using Gameplay.Melee;
using Gameplay.NewInput;
using Gameplay.NewInput.Decks;
using Gameplay.NewInput.NewMelee;
using Gameplay.StateMachine;
using Gameplay.StateMachine.ActionsAndStates;
using UnityEngine;
using MeleePoolManager = Gameplay.Melee.MeleePoolManager;

namespace Gameplay.Capabilities
{
    public class MeleeCapability : Capability
    {
        public DeckManager deckManager;
        public MeleePoolManager meleePoolManager;
        
        public MeleeCapabilityProps meleeCapabilityProps;
        public BonecoMovementCapabilityProps bonecoMovementCapabilityProps;
        public InputBroadcaster inputBroadcaster;
        
        public StateMachine.StateMachine stateMachine;
        public MeleeAction meleeAction;

        public GameObject cachedMelee;

        public bool leftMelee = false;

        protected override void Awake()
        {
            deckManager = GetComponent<DeckManager>();
            base.Awake();
        }

        private void Start()
        {
            bonecoMovementCapabilityProps = deckManager.GetPlayerMovementCapabilityProps();
            meleePoolManager              = deckManager.GetNewMeleePoolManager(gameObject);
            meleeCapabilityProps          = deckManager.GetMeleeCapabilityProps();
            inputBroadcaster              = deckManager.GetNewInputBroadcaster();

            stateMachine = deckManager.GetStateMachine(gameObject);
            meleeAction  = deckManager.GetMeleeAction(gameObject);
        }

        public override IEnumerator EnterCapability()
        {
            cachedMelee = meleePoolManager.Get();

            if ((inputBroadcaster.HasAnyLeftInput || bonecoMovementCapabilityProps.faceDir == -1) && canUse)
            {
                leftMelee = true;
            }
            
            if ((inputBroadcaster.HasAnyRightInput || bonecoMovementCapabilityProps.faceDir == 1) && canUse)
            {
                leftMelee = false;
            }

            cachedMelee.transform.position = gameObject.transform.position;
            if (leftMelee)
            {
                cachedMelee.transform.Rotate(0,0, 180);
            }

            stateMachine.SetAction(meleeAction);
            
            while (meleeCapabilityProps.capabilityPerformed <= meleeCapabilityProps.capabilityDuration)
            {
                canUse = false;
                cachedMelee.SetActive(true);
                meleeCapabilityProps.capabilityPerformed += Time.deltaTime;
                
                yield return null;
            }
            stateMachine.UnsetActualAction();

            if (leftMelee)
            {
                cachedMelee.transform.Rotate(0,0,180);
            }

            canUse = true;
            leftMelee = false;
            
            meleeCapabilityProps.capabilityPerformed = 0f;
            
            meleePoolManager.ReturnToPool(cachedMelee);
        }
    }
}
