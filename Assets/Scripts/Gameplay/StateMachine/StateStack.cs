using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.StateMachine
{
    public class StateStack : MonoBehaviour
    {
        [SerializeField]
        public Stack<IAnythingState> stack;

        public int count = 0;
        public string currentState = "";

        void Awake()
        {
            stack = new Stack<IAnythingState>();
        }

        void Update()
        {
            //count = stack.Count;
            //currentState = stack.Peek().ToString();
        }
    }
}
