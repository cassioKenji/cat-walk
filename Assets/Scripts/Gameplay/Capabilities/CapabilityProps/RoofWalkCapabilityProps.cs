using UnityEngine;

namespace Gameplay.Capabilities.CapabilityProps
{
    [CreateAssetMenu(menuName = "CapabilityProps/RoofWalk")]
    public class RoofWalkCapabilityProps : CapabilityPropsScriptableObject
    {
        public float capabilityDuration;
        public float capabilityPerformed;

        public float coolDownTick = 0f;
        public float coolDown = 1f;
    }
}
