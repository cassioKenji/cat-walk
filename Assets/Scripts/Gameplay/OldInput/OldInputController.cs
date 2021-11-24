using Gameplay.StateMachine;
using Rewired;
using UnityEngine;

namespace Gameplay.OldInput
{
    public class OldInputController : MonoBehaviour
    {
        public int playerId = 0;
        public Rewired.Player player;

        private const string XAxisActionName = "MoveHorizontal";
        private const string YAxisActionName = "MoveVertical";
        private const string JumpActionName = "Jump";
        private const string MeleeActionName = "Melee";

        public bool JumpButtonDown, MeleeButtonDown;
        public bool JumpButtonUp;
        
        public Vector2 _moveVector; 
        //private Controller2D _controller2D;
        
        float _moveSpeed = 500f;

        private global::Gameplay.OldInput.OldPlayer _oldPlayer;

        private OldInputBroadcaster _oldInputBroadcaster;
        public StateBroadcast stateBroadcast;

        void Start()
        {
            _oldPlayer = GetComponent<global::Gameplay.OldInput.OldPlayer> ();
            player       = ReInput.players.GetPlayer(playerId);
            //_rewiredMouse        = ReInput.controllers.Mouse;
            //_controller2D = GetComponent<Controller2D>();

            //_character = GetComponent<Player>();
            _oldInputBroadcaster = GetComponent<OldInputBroadcaster>();
            //playerStateBroadcast = GetComponent<PlayerStateBroadcast>();
        }

        void Update()
        {
            GetInput();
            MoveVectorToDirection();
            ProcessMovementInput();
            BroadcastInput();
        }

        void GetInput()
        {
            _moveVector    = player.GetAxis2DRaw(XAxisActionName, YAxisActionName);
            
            JumpButtonDown = player.GetButtonDown(JumpActionName);
            JumpButtonUp = player.GetButtonUp(JumpActionName);
            MeleeButtonDown = player.GetButtonDown(MeleeActionName);

            _moveVector = new Vector3(Mathf.Clamp(_moveVector.x, -1, 1), Mathf.Clamp(_moveVector.y, -1,1));
        }
        
        public bool HasAnyInput()
        {
            return (_moveVector.x != 0 || _moveVector.y != 0) || JumpButtonDown;
        }

        void BroadcastInput()
        {
           _oldInputBroadcaster.BroadcastInput(_moveVector);
        }
 
        void MoveVectorToDirection()
        {
            _oldInputBroadcaster.MoveVectorToDirection(_moveVector);
        }

        public void ProcessMovementInput()
        {
            _oldPlayer.SetDirectionalInput (_moveVector);

            //TODO wrong! input controller is not responsible for check states
            if (JumpButtonDown && stateBroadcast.state != States.Roofed)
            {
                _oldPlayer.OnJumpInputDown();
            }
            
            //TODO wrong! input controller is not responsible for check states
            if (JumpButtonDown && stateBroadcast.state == States.Roofed)
            {
                //detach from roof?
            }

            if (JumpButtonUp)
            {
                _oldPlayer.OnJumpInputUp();
            }

            if (MeleeButtonDown)
            {
                _oldPlayer.OnMeleeButtonDown();
            }
        }
    }
}
