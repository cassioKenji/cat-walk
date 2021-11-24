using Gameplay.Melee;
using UnityEngine;

namespace Gameplay.Decks
{
    [CreateAssetMenu(fileName = "MeleeDeck", menuName = "MeleeDeck/MeleeDeck")]
    public class MeleeDeck : BonecoDeck
    {
        public GameObject newPlayerMeleeA;
        public MeleePoolManager meleePoolManager;

        public GameObject NewPlayerMeleeA()
        {
            return Instantiate(newPlayerMeleeA);
        }
        
        public MeleePoolManager NewMeleePoolManager(GameObject gameObject)
        {
            if (meleePoolManager == null)
            {
                meleePoolManager = gameObject.AddComponent(typeof(MeleePoolManager)) as MeleePoolManager;
            }
            return meleePoolManager;
        }
    }
}
