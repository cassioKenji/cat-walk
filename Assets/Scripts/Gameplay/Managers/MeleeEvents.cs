using System;
using UnityEngine;

namespace Gameplay.Managers
{
    public class MeleeEvents : MonoBehaviour
    {
        public static MeleeEvents current;

        private void Awake()
        {
            current = this;
        }

        public event Action<float, float> onMeleeHitTriggerEnter;

        public void OnMeleeHitTriggerEnter(float input, float faceDir)
        {
            if (onMeleeHitTriggerEnter != null)
            {
                onMeleeHitTriggerEnter(input, faceDir);
            }
        }
    }
}
