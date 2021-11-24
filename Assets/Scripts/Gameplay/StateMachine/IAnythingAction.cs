using UnityEngine;

namespace Gameplay.StateMachine
{
    public interface IAnythingAction
    {
        public abstract void OnActionTick();
        public abstract void OnActionEnter();
        public abstract void OnActionExit();
    }
}
