using UnityEngine;

namespace Gameplay.Capabilities.CapabilityProps
{
    public class BounceOnWallCapabilityProps : ScriptableObject
    {
        public float capabilityDuration = 0.1f;
        public float capabilityPerformed;

        public float bounceYVelocity = 30;
        public float bounceXVelocity = -5;
    }
}
