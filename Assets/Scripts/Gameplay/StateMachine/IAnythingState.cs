using UnityEngine;

namespace Gameplay.StateMachine
{
    public interface IAnythingState
    {
        public abstract void Awake();
        public abstract void Start();
        public abstract void OnStateTick();
        public abstract void OnStateEnter();
        public abstract void OnStateExit(); //talvez sempre no state exit eu coloque o unset das flags
    }
}
