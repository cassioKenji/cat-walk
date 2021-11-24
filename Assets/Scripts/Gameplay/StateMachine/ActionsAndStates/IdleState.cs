using Gameplay.Decks;
using Gameplay.Utils.Debug.DebugPrinting;
using UnityEngine;

namespace Gameplay.StateMachine.ActionsAndStates
{
    public class IdleState : MonoBehaviour, IAnythingState
    {
        public DeckManager deckManager;
        public StateBroadcast stateBroadcast;

        public void Awake()
        {
            deckManager = GetComponentInParent<DeckManager>();
        }

        public void Start()
        {
            stateBroadcast = deckManager.GetStateBroadcast();
        }

        public void OnStateTick()
        {
            // aqui eu coloco alguma logica se for o caso
        }

        public void OnStateEnter()
        {
            DebugLogPrinterController.current.PrintStatesLog($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: {this.GetType().Name}");
            stateBroadcast.state = States.Idle;
        }

        public void OnStateExit()
        {
            DebugLogPrinterController.current.PrintStatesLog($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: {this.GetType().Name}");
            stateBroadcast.was = States.Idle;
        }

        //TODO remove this ExitGame() from this class. 
        private void OnDisable()
        {
            stateBroadcast.ExitGame();
        }
    }
}
