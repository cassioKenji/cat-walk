using Gameplay.Decks;
using Gameplay.Food;
using Gameplay.Utils.Debug.DebugPrinting;
using UnityEngine;

namespace Gameplay.StateMachine.ActionsAndStates
{
    public class PlatedState : MonoBehaviour, IAnythingState
    {
        private DeckManager    _deckManager;
        private StateBroadcast _stateBroadcast;
        
        public void Awake()
        {
            _deckManager = GetComponentInParent<DeckManager>();
        }

        public void Start()
        {
            _stateBroadcast = _deckManager.GetStateBroadcast();
        }
        
        public void OnStateTick()
        {
            
        }

        public void OnStateEnter()
        {
            DebugLogPrinterController.current.PrintStatesLog($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: {this.GetType().Name}");
            _stateBroadcast.state = States.Plated;
        }

        public void OnStateExit()
        {
            DebugLogPrinterController.current.PrintStatesLog($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: {this.GetType().Name}");
            _stateBroadcast.was = States.Plated;
        }
    }
}