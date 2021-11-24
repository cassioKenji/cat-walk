using UnityEngine;

namespace Gameplay.Capabilities.CapabilityProps
{
    [CreateAssetMenu(menuName = "CapabilityProps/Cooldown")]
    public class CooldownProps : CapabilityPropsScriptableObject
    {
        public float coolDown = 0;
        public float countDownTick = 0;

        public bool CanUse()
        {
            return coolDown <= 0;
        }
    }
}
