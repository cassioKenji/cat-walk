using Gameplay.StateMachine;
using UnityEngine;

namespace Gameplay.Food
{
    public class FoodMotor
    {
        private readonly Transform _foodTransform;
        private readonly FoodMovementProps _foodMovementProps;

        public FoodMotor(Transform foodTransform, FoodMovementProps foodMovementMovementCapabilityProps)
        {
            _foodTransform = foodTransform;
            _foodMovementProps = foodMovementMovementCapabilityProps;
        }

        public void Tick(Vector3 moveAmount)
        {
            _foodTransform.Translate(moveAmount);
        }
    }
}
