using System.Collections.Generic;
using Gameplay.Capabilities;
using Gameplay.Decks;
using UnityEngine;

namespace Gameplay.NewInput.Decks
{
    [CreateAssetMenu(fileName = "CapabilitiesDeck", menuName = "BonecoDeck/CapabilitiesDeck")]
    public class CapabilitiesDeck : BonecoDeck
    {
        [SerializeField]
        private JumpCapability _jumpCapability;
        
        [SerializeField]
        private MeleeCapability _meleeCapability;
        
        [SerializeField]
        private BounceOnWallCapability _bounceOnWallCapability;
        
        [SerializeField]
        private  List<Capability> capabilities;
        
        public JumpCapability JumpCapability(GameObject gameObject)
        {
            if (_jumpCapability == null)
            {
                _jumpCapability = gameObject.AddComponent(typeof(JumpCapability)) as JumpCapability;
            }
            return _jumpCapability;
        }
        
        public MeleeCapability MeleeCapability(GameObject gameObject)
        {
            if (_meleeCapability == null)
            {
                _meleeCapability = gameObject.AddComponent(typeof(MeleeCapability)) as MeleeCapability;
            }
            return _meleeCapability;
        }
        
        public BounceOnWallCapability BounceOnWallCapability(GameObject gameObject)
        {
            if (_bounceOnWallCapability == null)
            {
                _bounceOnWallCapability = gameObject.AddComponent(typeof(BounceOnWallCapability)) as BounceOnWallCapability;
            }
            return _bounceOnWallCapability;
        }
    }
}
