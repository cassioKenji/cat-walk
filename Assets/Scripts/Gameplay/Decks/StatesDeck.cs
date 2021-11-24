using Gameplay.Decks;
using Gameplay.StateMachine;
using Gameplay.StateMachine.ActionsAndStates;
using UnityEngine;

namespace Gameplay.NewInput.Decks
{
    [CreateAssetMenu(fileName = "StatesDeck", menuName = "BonecoDeck/StatesDeck")]
    public class StatesDeck : BonecoDeck
    {
        [SerializeField]
        private JumpingState _jumpingState;
        
        [SerializeField]
        private GroundedState _groundedState;
        
        [SerializeField]
        private HitToRightState _hitToRightState;
        
        [SerializeField]
        private HitToLeftState _hitToLeftState;
        
        [SerializeField]
        private NeverHittedState _neverHittedState;
        
        [SerializeField]
        private BouncedState _bouncedState;
        
        [SerializeField]
        private PlatedState _platedState;

        [SerializeField]
        private WastedState _wastedState;
        
        [SerializeField]
        private RecycledState _recycledState;
        
        [SerializeField]
        private NoneState _noneState;
        
        [SerializeField]
        private RunningState _runningState;
        
        [SerializeField]
        private IdleState _idleState;
        
        [SerializeField]
        private CrouchState _crouchState;
        
        [SerializeField]
        private FallingState _fallingState;
        
        [SerializeField]
        private MeleeAction _meleeAction;

        [SerializeField] 
        private StateMachine.StateMachine _stateMachine;

        [SerializeField] 
        private StateStack _stateStack;
        
        [SerializeField] 
        private ActionStack _actionStack;
        
        [SerializeField] 
        private StateBroadcast stateBroadcast;
        
        [SerializeField] 
        private ActionBroadcast actionBroadcast;
        
        public JumpingState JumpingState(GameObject gameObject)
        {
            if (_jumpingState == null)
            {
                _jumpingState = gameObject.AddComponent(typeof(JumpingState)) as JumpingState;
            }

            return _jumpingState;
        }
        
        public GroundedState GroundedState(GameObject gameObject)
        {
            if (_groundedState == null)
            {
                _groundedState = gameObject.AddComponent(typeof(GroundedState)) as GroundedState;
            }

            return _groundedState;
        }
        
        public HitToRightState HitToRightState(GameObject gameObject)
        {
            if (_hitToRightState == null)
            {
                _hitToRightState = gameObject.AddComponent(typeof(HitToRightState)) as HitToRightState;
            }

            return _hitToRightState;
        }
        
        public HitToLeftState HitToLeftState(GameObject gameObject)
        {
            if (_hitToLeftState == null)
            {
                _hitToLeftState = gameObject.AddComponent(typeof(HitToLeftState)) as HitToLeftState;
            }

            return _hitToLeftState;
        }
        
        public NeverHittedState NeverHittedState(GameObject gameObject)
        {
            if (_neverHittedState == null)
            {
                _neverHittedState = gameObject.AddComponent(typeof(NeverHittedState)) as NeverHittedState;
            }

            return _neverHittedState;
        }
        
        public BouncedState BouncedState(GameObject gameObject)
        {
            if (_bouncedState == null)
            {
                _bouncedState = gameObject.AddComponent(typeof(BouncedState)) as BouncedState;
            }

            return _bouncedState;
        }
        
        public PlatedState PlatedState(GameObject gameObject)
        {
            if ( _platedState == null)
            {
                _platedState = gameObject.AddComponent(typeof(PlatedState)) as PlatedState;
            }

            return _platedState;
        }
        
        public WastedState WastedState(GameObject gameObject)
        {
            if ( _wastedState == null)
            {
                _wastedState = gameObject.AddComponent(typeof(WastedState)) as WastedState;
            }

            return _wastedState;
        }
        
        public RecycledState RecycledState(GameObject gameObject)
        {
            if ( _recycledState == null)
            {
                _recycledState = gameObject.AddComponent(typeof(RecycledState)) as RecycledState;
            }

            return _recycledState;
        }
        
        public NoneState NoneState(GameObject gameObject)
        {
            if (_noneState == null)
            {
                _noneState = gameObject.AddComponent(typeof(NoneState)) as NoneState;
            }

            return _noneState;
        }
        
        public RunningState RunningState(GameObject gameObject)
        {
            if (_runningState == null)
            {
                _runningState = gameObject.AddComponent(typeof(RunningState)) as RunningState;
            }

            return _runningState;
        }
        
        public IdleState IdleState(GameObject gameObject)
        {
            if (_idleState == null)
            {
                _idleState = gameObject.AddComponent(typeof(IdleState)) as IdleState;
            }

            return _idleState;
        }
        
        public MeleeAction MeleeAction(GameObject gameObject)
        {
            if (_meleeAction == null)
            {
                _meleeAction = gameObject.AddComponent(typeof(MeleeAction)) as MeleeAction;
            }

            return _meleeAction;
        }
        
        public CrouchState CrouchState(GameObject gameObject)
        {
            if (_crouchState == null)
            {
                _crouchState = gameObject.AddComponent(typeof(CrouchState)) as CrouchState;
            }

            return _crouchState;
        }
        
        public FallingState FallingState(GameObject gameObject)
        {
            if ((_fallingState) == null)
            {
                _fallingState = gameObject.AddComponent(typeof(FallingState)) as FallingState;
            }

            return _fallingState;
        }
        
        public StateMachine.StateMachine StateMachine(GameObject gameObject)
        {
            if (_stateMachine == null)
            {
                _stateMachine = gameObject.AddComponent(typeof(StateMachine.StateMachine)) as StateMachine.StateMachine;
            }

            return _stateMachine;
        }
        
        public StateStack StateStack(GameObject gameObject)
        {
            if (_stateStack == null)
            {
                _stateStack = gameObject.AddComponent(typeof(StateMachine.StateStack)) as StateMachine.StateStack;
            }

            return _stateStack;
        }
        
        public ActionStack ActionStack(GameObject gameObject)
        {
            if (_actionStack == null)
            {
                _actionStack = gameObject.AddComponent(typeof(StateMachine.ActionStack)) as StateMachine.ActionStack;
            }

            return _actionStack;
        }
        
        public StateBroadcast StateBroadcast()
        {
            if (stateBroadcast == null)
            {
                stateBroadcast = ScriptableObject.CreateInstance<StateBroadcast>();
            }

            return stateBroadcast;
        }
        
        public ActionBroadcast ActionBroadcast()
        {
            if (actionBroadcast == null)
            {
                actionBroadcast = ScriptableObject.CreateInstance<ActionBroadcast>();
            }

            return actionBroadcast;
        }
    }
}
