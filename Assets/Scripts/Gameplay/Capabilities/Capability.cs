using System;
using System.Collections;
using Gameplay.Capabilities.CapabilityProps;
using Gameplay.Decks;
using Gameplay.NewInput;
using Gameplay.NewInput.Decks;
using Gameplay.OldInput;
using UnityEngine;

namespace Gameplay.Capabilities
{
    //TODO humble object?
    public class Capability : MonoBehaviour, ICapability
    {
        public DeckManager _deckManager;
        
        public OldPlayer character;
        public Boneco.Boneco boneco;

        public Food.Food food;
        //public State thisState;
        public AudioClip thisSfx;
        public bool canUse = true;

        OnCapabilityUseArgs.OnCapabilityUseEventArgs _eventArgs;

        public virtual void Initialize(float coolDownTimeSetting, OldPlayer oldPlayer)
        {
            this.character    = oldPlayer;
            SetupCapability();
        }
        
        public virtual void Initialize(Boneco.Boneco boneco)
        {
            this.boneco    = boneco;
            SetupCapability();
        }
        
        public virtual void Initialize(Food.Food food)
        {
            this.food    = food;
            SetupCapability();
        }

        public EventHandler<OnCapabilityUseArgs.OnCapabilityUseEventArgs> OnCapabilityUse;

        protected virtual void Awake()
        {
            EventArgsInitialize();
        }
        
        protected virtual void Update()
        {
            CoolDownTick();
        }
        
        public virtual void SetupCapability()
        {
            //cooldownTimer = coolDownTime;
            //character.SetState(thisState);
        }
        
        public virtual void StartCapability()
        {
            StartCoroutine(EnterCapability());
        }

        public virtual void StopCapability()
        {
            StopCoroutine(EnterCapability());
        }

        public virtual IEnumerator EnterCapability()
        {
            yield return null;
        }

        public virtual void ExitCapability()
        {
            TearDownCapability();
        }

        public virtual void TearDownCapability()
        {
            
        }
        
        void CoolDownTick()
        {
            //if (cooldownTimer > 0f)
            {
            //    cooldownTimer -= Time.deltaTime;
            }
        }

        public bool CanUse()
        {
            return canUse;
        }

        public void PlayCapabilitySfx()
        {
            OnCapabilityUse.Invoke(this, _eventArgs);
        }

        void EventArgsInitialize()
        {
            _eventArgs = new OnCapabilityUseArgs.OnCapabilityUseEventArgs {thisSfx = this.thisSfx};
        }
    }
}
