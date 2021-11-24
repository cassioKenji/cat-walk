using UnityEngine;

namespace Gameplay.Melee
{
    public class MeleeMotor
    {
        private Transform _ownerTransform;
        private readonly Transform _meleeTransform;
        
        public MeleeMotor(Transform meleeTransform)
        {
            _meleeTransform = meleeTransform;
        }
        
        public void SetTransformOwner(Transform ownerTransform)
        {
            _ownerTransform = ownerTransform;
        }

        public void Tick()
        {
            _meleeTransform.position = _ownerTransform.position;
        }
    }
}
