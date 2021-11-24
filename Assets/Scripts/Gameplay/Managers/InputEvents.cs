using System;
using UnityEngine;

namespace Gameplay.Managers
{
    public class InputEvents : MonoBehaviour
    {
        public static InputEvents current;

        private void Awake()
        {
            current = this;
        }

        public event Action OnMoveHorizontal;

        public void OnMeleeHitTriggerEnter(float input, float faceDir)
        {
            if (OnMoveHorizontal != null)
            {
                OnMoveHorizontal();
            }
        }
    }
}
