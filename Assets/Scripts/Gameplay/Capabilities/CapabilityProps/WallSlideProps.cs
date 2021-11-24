using UnityEngine;

namespace Gameplay.Capabilities.CapabilityProps
{
    [CreateAssetMenu(menuName = "CapabilityProps/WallSlide")]
    public class WallSlideProps : CapabilityPropsScriptableObject
    {
        public float capabilityDuration;
        public float capabilityPerformed;

        public float coolDownTick = 0f;
        public float coolDown = 0.3f;
    }
}
