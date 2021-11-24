using Gameplay.StateMachine;
using UnityEngine;

namespace Gameplay.Boneco
{
    public class BonecoMotor
    {
        private readonly Transform _bonecoTransform;
        private readonly StateBroadcast _stateBroadcast;
        
        public BonecoMotor(Transform bonecoTransform, StateBroadcast stateBroadcast)
        {
            _bonecoTransform = bonecoTransform;
            _stateBroadcast = stateBroadcast;
        }

        public void Tick(Vector3 moveAmount)
        {
            if (IsCrouchedState())
            {
                //TODO implement a more reusable and with better legibility way of overwrite x if crouched
                moveAmount.x = 0;
            }

            _bonecoTransform.Translate(moveAmount);
        }

        bool IsCrouchedState()
        {
            return (_stateBroadcast.state == States.Crouch);
        }
    }
}
