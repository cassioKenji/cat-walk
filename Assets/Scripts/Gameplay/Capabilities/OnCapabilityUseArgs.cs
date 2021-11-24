using System;
using UnityEngine;

namespace Gameplay.Capabilities
{
    public class OnCapabilityUseArgs : MonoBehaviour
    {
        public class OnCapabilityUseEventArgs : EventArgs
        {
            public AudioClip thisSfx;
        }
    }
}
