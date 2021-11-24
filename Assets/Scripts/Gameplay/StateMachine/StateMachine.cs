using Gameplay.Decks;
using Gameplay.StateMachine.ActionsAndStates;
using UnityEngine;

namespace Gameplay.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        public DeckManager deckManager;
        
        [SerializeField]
        public StateStack states;
        public ActionStack actions;
        
        IAnythingState currentState;
        IAnythingAction currentAction;
        
        private NoneAction _noneAction;
        public  NoneState _noneState;
        public GroundedState groundedState;
        
        private void Awake()
        {
            deckManager = GetComponent<DeckManager>();
            
            //actions = GetComponent<ActionStack>();
            //states = GetComponent<StateStack>();
        }

        private void Start()
        {
            AddActionStack();
            AddStatesStack();
            AddNoneAction();
            AddNoneState();
            AddGroundeState();
            //_noneAction = GetComponent<NoneAction>();
            //_noneState = GetComponent<NoneState>();
            //groundedState = GetComponent<GroundedState>();
            //currentState = groundedState;
            //Debug.Log("void Start() - currentState is holding: " + currentState.ToString() );
            //Debug.Log("void Start() - GroundedState content now is: " + groundedState.ToString() );

            //SetAction(_noneAction);
            //SetState(groundedState);
        }
        
        public void SetAction(IAnythingAction action)
        {
            if (IsCurrentActionEqualsTo(action)) return;
            
            PopLastAction()?.OnActionExit();
            currentAction = action;
            
            currentAction.OnActionEnter();

            AddActionToStack();
        }

        public bool IsCurrentActionEqualsTo(IAnythingAction action)
        {
            return action == currentAction;
        }

        public void UnsetActualAction()
        {
            SetAction(_noneAction);
        }
        
        public void UnsetActualState()
        {
            SetState(_noneState);
        }

        public void SetState(IAnythingState state)
        {
            if (IsCurrentStateEqualsTo(state)) return;

            PopLastState()?.OnStateExit();

            state.OnStateEnter();
            
            currentState = state;
            AddStateToStack();
        }

        public void ExitState()
        {
            currentState?.OnStateExit();
        }

        public bool IsCurrentStateEqualsTo(IAnythingState state)
        {
            return currentState == state ;
        }
        
        private void AddStateToStack()
        {
            states.stack.Push(currentState);
        }

        private void AddActionToStack()
        {
            actions.stack.Push(currentAction);
        }

        public IAnythingState PopLastState()
        {
            if (states.stack.Count < 1) return null;
            return states.stack.Pop();
        }
        public IAnythingAction PopLastAction()
        {
            if (actions.stack.Count < 1) return null;
            return actions.stack.Pop(); 
        }

        public void DebugStateMachine()
        {
            Debug.Log("stack count: " + states.stack.Count);
            Debug.Log("currentState variable is holding: " + currentState.ToString() );
            
            while (states.stack.Count > 0)
            {
                Debug.Log(states.stack.Pop()?.ToString());
            }
            
        }

        public void ClearStates()
        {
            states.stack.Clear();
        }

        public void Initialize(IAnythingState state, IAnythingAction action)
        {
 
        }

        private void AddStatesStack()
        {
            if (states != null)
            {
                return;
            }
            states = gameObject.AddComponent(typeof(StateStack)) as StateStack;
        }
        
        private void AddActionStack()
        {
            if (states != null)
            {
                return;
            }
            actions = gameObject.AddComponent(typeof(ActionStack)) as ActionStack;
        }

        private void AddNoneState()
        {
            _noneState = gameObject.AddComponent(typeof(NoneState)) as NoneState;
        }
        
        private void AddNoneAction()
        {
            _noneAction = gameObject.AddComponent(typeof(NoneAction)) as NoneAction;
        }
        
        private void AddGroundeState()
        {
            groundedState = gameObject.AddComponent(typeof(GroundedState)) as GroundedState;
        }
        
        private void AddFallingState()
        {
            //fallingState = gameObject.AddComponent(typeof(FallingState)) as FallingState;
        }
    }
}
