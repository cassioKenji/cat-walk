using UnityEngine;

namespace Gameplay.Capabilities.CapabilityProps
{
    [CreateAssetMenu(menuName = "CapabilityProps/Jump")]
    public class JumpCapabilityProps : CapabilityPropsScriptableObject
    {
        public float coolDown = 0.3f;
        public float maxJumpHeight = 8;
        public float minJumpHeight = 2;
        public float timeToJumpApex = .3f;
        public Vector2 wallJumpClimb = new Vector2(48, 48);
        public Vector2 wallJumpOff = new Vector2(48, 48);
        public int airJump = 1;
        public bool canDoubleJump = true;
        public float maxJumpVelocity;
        public float minJumpVelocity;
    }
}
