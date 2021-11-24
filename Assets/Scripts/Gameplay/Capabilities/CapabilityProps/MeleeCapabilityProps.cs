using UnityEngine;

namespace Gameplay.Capabilities.CapabilityProps
{
    [CreateAssetMenu(menuName = "CapabilityProps/Melee")]
    public class MeleeCapabilityProps : CapabilityPropsScriptableObject
    {
        public float capabilityDuration = 0.25f;
        public float capabilityPerformed;

        public float coolDownTick = 0f;
        public float coolDown = 1f;
    }
}
