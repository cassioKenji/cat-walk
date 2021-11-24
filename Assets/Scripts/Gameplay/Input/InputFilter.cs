using System;
using Gameplay.Input;
using UnityEngine;

namespace Gameplay.NewInput
{
    public class InputFilter : MonoBehaviour
    {
        [SerializeField]
        private InputController inputController;
        private void Awake()
        {
            inputController = GetComponent<InputController>();
        }
    }
}
