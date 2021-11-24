using UnityEngine;

namespace Gameplay.OldInput
{
    [CreateAssetMenu(menuName = "PlayerInput/PlayerInputBroadCaster")]
    public class OldInputBroadCasterScriptableObject : ScriptableObject
    {
        public Vector2 playerDirectionalInput;
        public Direction direction = Direction.None;
        
        public bool HasAnyLeftInput()
        {
            return (this.direction == Direction.Left || this.direction == Direction.DownLeft || this.direction == Direction.UpLeft);
        }
        
        public bool HasAnyRightInput()
        {
            return (this.direction == Direction.Right || this.direction == Direction.DownRight || this.direction == Direction.UpRight);
        }
    }

    public enum Direction
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
