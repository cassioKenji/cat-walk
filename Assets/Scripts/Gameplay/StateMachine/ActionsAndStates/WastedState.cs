using Gameplay.Decks;
using Gameplay.Utils.Debug.DebugPrinting;
using UnityEngine;

namespace Gameplay.StateMachine.ActionsAndStates
{
    public class WastedState : MonoBehaviour, IAnythingState
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
            _stateBroadcast.state = States.Wasted;
        }

        public void OnStateExit()
        {
            DebugLogPrinterController.current.PrintStatesLog($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: {this.GetType().Name}");
            _stateBroadcast.was = States.Wasted;
        }
    }
}
