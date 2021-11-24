using System;
using UnityEngine;

namespace Gameplay.StateMachine
{
    [CreateAssetMenu(menuName = "StateMachine/PlayerActionSO")]
    public class ActionBroadcast : ScriptableObject
    {
        public Actions action = Actions.None;
        public Actions was = Actions.None;
        
        public void ExitGame()
        {
            action = Actions.None;
        }
    }
    
    public enum Actions
    {
        None,
        Melee
    }
}
