using Gameplay.Input;
using Gameplay.OldInput;
using UnityEngine;

namespace Gameplay.Decks
{
    [CreateAssetMenu(fileName = "BroadcasterDeck", menuName = "BonecoDeck/BroadcasterDeck")]
    public class BroadcasterDeck : BonecoDeck
    {
        [SerializeField]
        private InputBroadcaster inputBroadcaster;
        
        [SerializeField] 
        private OldInputBroadCasterScriptableObject oldInputBroadcaster;

        public InputBroadcaster NewInputBroadcaster()
        {
            if (inputBroadcaster == null)
            {
                inputBroadcaster = ScriptableObject.CreateInstance<InputBroadcaster>();
            }

            return inputBroadcaster;
        }
        
        public OldInputBroadCasterScriptableObject InputBroadcaster()
        {
            if (oldInputBroadcaster == null)
            {
                oldInputBroadcaster = ScriptableObject.CreateInstance<OldInputBroadCasterScriptableObject>();
            }

            return oldInputBroadcaster;
        }
    }
}
