using Gameplay.Capabilities.CapabilityProps;
using Gameplay.Decks;
using Gameplay.Input;
using Gameplay.Managers;
using UnityEngine;

namespace Gameplay.Melee
{
    public class BaseMelee : MonoBehaviour, IPoolable
    {
        [SerializeField] 
        private DeckManager _deckManager;

        public BoxCollider2D hitBox;
        
        private MeleeMotor _meleeMotor;

        [SerializeField] 
        private InputBroadcaster playerInputBroadcaster;

        [SerializeField]
        private BonecoMovementCapabilityProps _bonecoMovementCapabilityProps;

        [SerializeField] 
        private HitStrength _hitStrength = HitStrength.Fierce;
        
        

        private void Awake()
        {
            hitBox = GetComponent<BoxCollider2D>();
            _meleeMotor = new MeleeMotor(transform);
        }

        private void Start()
        {
            playerInputBroadcaster         = _deckManager.GetNewInputBroadcaster();
            _bonecoMovementCapabilityProps = _deckManager.GetPlayerMovementCapabilityProps();
        }

        public void SetOwner(Transform ownerTransform, DeckManager ownerDeck)
        {
            _deckManager = ownerDeck;
            _meleeMotor.SetTransformOwner(ownerTransform);
        }

        private void Update()
        {
            _meleeMotor.Tick();
        }

        #region Collisions
        
        private void OnTriggerEnter2D(Collider2D hittedGOCollider2D)
        {
            var hitted = hittedGOCollider2D.gameObject.GetComponent<Food.Food>();
            if (hitted == null)
            {
                return;
            }
            //TODO it is Melee's responsability to know how much force to pass already as a float 
            hitted.GetHit(playerInputBroadcaster.XInput, _bonecoMovementCapabilityProps.faceDir, HitStrength.Fierce);
        }

        #endregion
    }
    
    public enum HitStrength
    {
        Normal,
        Fierce,
    }
}
