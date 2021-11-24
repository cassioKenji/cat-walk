using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.StateMachine
{
    public class ActionStack : MonoBehaviour
    {
        public Stack<IAnythingAction> stack;

        void Awake()
        {
            stack = new Stack<IAnythingAction>();
        }
    }
}
