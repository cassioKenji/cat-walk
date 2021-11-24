using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Gameplay.Capabilities.CapabilityProps;
using Gameplay.Decks;
using UnityEngine;

namespace Gameplay.NewInput.Decks
{
    [CreateAssetMenu(fileName = "PropsDeck", menuName = "BonecoDeck/PropsDeck")]
    public class PropsDeck : BonecoDeck
    {
        private BonecoMovementCapabilityProps _bonecoMovementCapabilityProps;
        private JumpCapabilityProps           _jumpCapabilityProps;
        private MeleeCapabilityProps          _meleeCapabilityProps;
        private RoofWalkCapabilityProps       _roofWalkCapabilityProps;
        private WallSlideProps                _wallSlideProps;
        
        public BonecoMovementCapabilityProps BonecoMovementCapabilityProps()
        {
            if (_bonecoMovementCapabilityProps == null)
            {
                _bonecoMovementCapabilityProps = ScriptableObject.CreateInstance<BonecoMovementCapabilityProps>();
            }

            return _bonecoMovementCapabilityProps;
        }
        
        public JumpCapabilityProps JumpCapabilityProps()
        {
            if (_jumpCapabilityProps == null)
            {
                _jumpCapabilityProps = ScriptableObject.CreateInstance<JumpCapabilityProps>();
            }

            return _jumpCapabilityProps;
        }
        
        public MeleeCapabilityProps MeleeCapabilityProps()
        {
            if (_meleeCapabilityProps == null)
            {
                _meleeCapabilityProps = ScriptableObject.CreateInstance<MeleeCapabilityProps>();
            }

            return _meleeCapabilityProps;
        }
        
        public RoofWalkCapabilityProps RoofWalkCapabilityProps()
        {
            if (_roofWalkCapabilityProps == null)
            {
                _roofWalkCapabilityProps = ScriptableObject.CreateInstance<RoofWalkCapabilityProps>();
            }

            return _roofWalkCapabilityProps;
        }
        
        public WallSlideProps WallSlideProps()
        {
            if (_wallSlideProps == null)
            {
                _wallSlideProps = ScriptableObject.CreateInstance<WallSlideProps>();
            }

            return _wallSlideProps;
        }
    }
}
