using Gameplay.Decks;
using Gameplay.Utils.Debug.DebugPrinting;
using UnityEngine;

namespace Gameplay.StateMachine.ActionsAndStates
{
    public class GroundedState : MonoBehaviour, IAnythingState
    {
        [SerializeField]
        private DeckManager _deckManager;
        
        [SerializeField]
        public StateBroadcast _stateBroadcast;
        
        public void Awake()
        {
            _deckManager = GetComponent<DeckManager>();
            
        }

        public void Start()
        {
            _stateBroadcast = _deckManager.GetStateBroadcast();
        }

        public void OnStateTick()
        {
            // aqui eu coloco alguma logica se for o caso
        }

        public void OnStateEnter()
        {
            DebugLogPrinterController.current.PrintStatesLog($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: {this.GetType().Name}");
            _stateBroadcast.state = States.Grounded;
        }

        public void OnStateExit()
        {
            DebugLogPrinterController.current.PrintStatesLog($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: {this.GetType().Name}");
            _stateBroadcast.was = States.Grounded;
        }
        
        private void OnDisable()
        {
            _stateBroadcast.ExitGame();
        }
    }
}
