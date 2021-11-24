using System;
using UnityEngine;

namespace Gameplay.Utils.Debug.DebugPrinting
{
    public class DebugLogPrinterController : MonoBehaviour
    {
        public static DebugLogPrinterController current;

        public bool states          = true;
        public bool moveControllers = true;

        private void Awake()
        {
            current = this;
        }

        public void PrintStatesLog(String message)
        {
            if (states == false) return;
            
            UnityEngine.Debug.Log(message);
        }
        
        public void PrintMoveControllersLog(String message)
        {
            if (moveControllers == false) return;
            
            UnityEngine.Debug.Log(message);
        }
    }
}
