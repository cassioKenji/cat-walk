using UnityEngine;

namespace Gameplay.OldInput
{
    //TODO choose better name for this class. it breaks the name convention cause broadcaster is always a SO (PlayerStateBroadCaster, PlayerActionBroadCaster, etc)
    public class OldInputBroadcaster : MonoBehaviour
    {
        public OldInputBroadCasterScriptableObject oldInputBroadCasterScriptableObject;
        
        public void BroadcastInput(Vector2 moveVector)
        {
            oldInputBroadCasterScriptableObject.playerDirectionalInput = moveVector;
        }
        
        public void MoveVectorToDirection(Vector2 moveVector)
        {
            switch ((moveVector.x, moveVector.y))
            {
                case(0,0):
                    oldInputBroadCasterScriptableObject.direction = Direction.None;
                    break;
                
                case(0,1):
                    oldInputBroadCasterScriptableObject.direction = Direction.Up;
                    break;
                
                case(-1,1):
                    oldInputBroadCasterScriptableObject.direction = Direction.UpLeft;
                    break;
                
                case(1,1):
                    oldInputBroadCasterScriptableObject.direction = Direction.UpRight;
                    break;
                
                case(0,-1):
                    oldInputBroadCasterScriptableObject.direction = Direction.Down;
                    break;
                
                case(-1,0):
                    oldInputBroadCasterScriptableObject.direction = Direction.Left;
                    break;
                case(1,0):
                    oldInputBroadCasterScriptableObject.direction = Direction.Right;
                    break;
                case(1,-1):
                    oldInputBroadCasterScriptableObject.direction = Direction.DownRight;
                    break;
                case(-1,-1):
                    oldInputBroadCasterScriptableObject.direction = Direction.DownLeft;
                    break;
            }
        }
    }
}
