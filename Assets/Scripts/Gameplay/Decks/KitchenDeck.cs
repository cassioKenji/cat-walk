using System;
using Gameplay.Food;
using Gameplay.Food.Plate;
using Gameplay.Melee;
using UnityEngine;

namespace Gameplay.Decks
{
    public class KitchenDeck : BonecoDeck
    {
        [SerializeField] 
        private FoodPoolManager _hamburgerBunBottom;
        
        [SerializeField] 
        private FoodPoolManager _staticHamburgerBunBottom;

        public FoodPoolManager HamburgerBunBottom(GameObject gameObject)
        {
            if (_hamburgerBunBottom == null)
            {
                _hamburgerBunBottom = gameObject.AddComponent<FoodPoolManager>();
            }

            return _hamburgerBunBottom;
        }
        
        public FoodPoolManager StaticHamburgerBunBottom(GameObject gameObject)
        {
            if (_staticHamburgerBunBottom == null)
            {
                _staticHamburgerBunBottom = gameObject.AddComponent<FoodPoolManager>();
            }

            return _staticHamburgerBunBottom;
        }
    }
}    