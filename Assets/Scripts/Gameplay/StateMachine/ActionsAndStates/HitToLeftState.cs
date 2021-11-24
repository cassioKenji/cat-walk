using Gameplay.Decks;
using Gameplay.Food;
using Gameplay.Utils.Debug.DebugPrinting;
using UnityEngine;

namespace Gameplay.StateMachine.ActionsAndStates
{
    public class HitToLeftState : MonoBehaviour, IAnythingState
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
            
        }

        public void OnStateEnter()
        {
            DebugLogPrinterController.current.PrintStatesLog($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: {this.GetType().Name}");
            stateBroadcast.state = States.HitToLeft;
        }

        public void OnStateExit()
        {
            DebugLogPrinterController.current.PrintStatesLog($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: {this.GetType().Name}");
            stateBroadcast.was = States.HitToLeft;
        }
    }
}
