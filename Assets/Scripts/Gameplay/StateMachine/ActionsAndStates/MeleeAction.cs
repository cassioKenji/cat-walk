using Gameplay.Decks;
using Gameplay.Utils.Debug.DebugPrinting;
using UnityEngine;

namespace Gameplay.StateMachine.ActionsAndStates
{
    public class MeleeAction : MonoBehaviour, IAnythingAction
    {
        private DeckManager     _deckManager;
        private ActionBroadcast _actionBroadcast;
        
        public void Awake()
        {
            _deckManager = GetComponent<DeckManager>();
        }
        
        public void Start()
        {
            _actionBroadcast = _deckManager.GetBonecoActionBroadcast();
        }
        
        public void OnActionTick()
        {
            throw new System.NotImplementedException();
        }

        public void OnActionEnter()
        {
            DebugLogPrinterController.current.PrintStatesLog($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: {this.GetType().Name}");
            _actionBroadcast.action = Actions.Melee;
        }

        public void OnActionExit()
        {
            DebugLogPrinterController.current.PrintStatesLog($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: {this.GetType().Name}");
            _actionBroadcast.was = Actions.Melee;
        }
    }
}
