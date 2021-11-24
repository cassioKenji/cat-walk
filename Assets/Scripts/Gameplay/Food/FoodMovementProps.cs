using UnityEngine;

namespace Gameplay.Food
{
    public class FoodMovementProps : ScriptableObject
    {
        public int faceDir = 0;
        
        public Vector3 velocity;
        public float gravity = -5;
        public float gravityAfterBounce = -50;
        public float negativeGravityCache = 5;
        public float positiveGravityCache = -5;
        public float cachedTargetVelocityX;
        public float currentXVelocity = 0;
        public float xVelocityToMove = 0;
        public float moveSpeed = 20;

        public float hittedMoveSpeed = -50;
        public float hardHittedMoveSpeed = -100;

        public float maxGravity = -5;
    }
}
