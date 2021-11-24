using UnityEngine;

namespace Gameplay.Capabilities.CapabilityProps
{
    [CreateAssetMenu(menuName = "CapabilityProps/PlayerMovement")]
    public class BonecoMovementCapabilityProps : ScriptableObject
    {
        public int faceDir = 0;
        
        public float accelerationTimeAirborne = 0.1f;
        public float accelerationTimeGrounded = 0.08f;
        public float moveSpeed = 20;
        public Vector2 wallLeap = new Vector2(32, 32);
        public float wallSlideSpeedMax = 3;
        public float wallStickTime = 0.3f;
        public float timeToWallUnstick = 0.3f;
        public Vector3 velocity;
        public float velocityXSmoothing;
        public float gravity = -177;
        public float negativeGravityCache = 177;
        public float positiveGravityCache = -177;
        public bool grounded = false;
        public bool wasGrounded = false;
        public bool touchingRoof = false;
        public bool wallSliding;
        public int wallDirX = 1;
        public float cachedTargetVelocityX;

        public Vector3 position = new Vector3(-20, -10, -10);

    }
}
