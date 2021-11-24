using UnityEngine;

namespace Gameplay.Input
{
    [CreateAssetMenu(menuName = "NewInput/NewInputBroadcaster")]
    public class InputBroadcaster : ScriptableObject
    {
        [SerializeField]
        private Vector2 _newInputDirection;
        public Vector2 NewInputDirection
        {
            get
            {
                return _newInputDirection;
            }
        }

        [SerializeField]
        private float xInput;
        public float XInput
        {
            get
            {
                return xInput;
            }
            set { xInput = value; }
        }

        [SerializeField]
        private NewDirection _newDirection = NewDirection.None;

        [SerializeField]
        private bool _hasAnyLeftInput;
        public bool HasAnyLeftInput { get; } = false;

        [SerializeField]
        private bool _hasAnyRighttInput;
        public bool HasAnyRightInput { get; } = false;
        
        public NewDirection NewDirection
        {
            get
            {
                return _newDirection;
            }
        }
        
        void SetHasAnyLeftInput()
        {
            _hasAnyLeftInput = _newDirection == NewDirection.Left || _newDirection == NewDirection.DownLeft || _newDirection == NewDirection.UpLeft;
        }
        
        void SetHasAnyRightInput()
        {
            _hasAnyRighttInput =_newDirection == NewDirection.Right || _newDirection == NewDirection.DownRight || _newDirection == NewDirection.UpRight;
        }

        public void UpdateInputs(Vector2 inputDirection, float xInputDirecion)
        {
            _newInputDirection = inputDirection;
            XInput = Mathf.Clamp(xInputDirecion, -1, 1);
            SetHasAnyLeftInput();
            SetHasAnyRightInput();
            _newDirection = Vector2ToNewDirection(inputDirection);
        }

        public static NewDirection Vector2ToNewDirection(Vector2 input) 
            => (input.x, input.y) switch
            {
                ( 0,   0)  => NewDirection.None,
                ( 0,   1)  => NewDirection.Up,
                (-1,  1 )  => NewDirection.UpLeft,
                ( 1,   1)  => NewDirection.UpRight,
                ( 0,  -1)  => NewDirection.Down,
                (-1,  0 )  => NewDirection.Left,
                ( 1,   0)  => NewDirection.Right,
                ( 1,  -1)  => NewDirection.DownRight,
                (-1, -1 )  => NewDirection.DownLeft
            };
        }

    public enum NewDirection
    {
        None,
        Up,
        UpRight,
        UpLeft,
        Down,
        Left,
        Right,
        DownRight,
        DownLeft
    }
}
