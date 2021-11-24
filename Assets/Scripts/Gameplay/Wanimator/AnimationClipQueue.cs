using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Wanimator
{
    public class AnimationClipQueue : MonoBehaviour
    {
        [SerializeField]
        public Queue<WanimationClip> queue;

        public int count = 0;
        public string currentState = "";

        void Awake()
        {
            queue = new Queue<WanimationClip>();
        }
    }
}
