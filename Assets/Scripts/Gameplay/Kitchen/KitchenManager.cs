using System;
using Gameplay.Decks;
using Gameplay.Food;
using Gameplay.Food.Plate;
using UnityEngine;

namespace Gameplay.Kitchen
{
    public class KitchenManager : MonoBehaviour
    {
        [SerializeField]
        private DeckManager _deckManager;

        public DeckManager DeckManager{
            get => _deckManager;
        }

        [SerializeField]
        private PlateController _plateController;

        [SerializeField] 
        private FoodBeltRecyclerController _foodBeltRecyclerController;

        //TODO implement a proper property to access this guy below 
        public FoodPoolManager _hamburgerBunBottomPoolManager;
        
        //TODO implement a proper property to access this guy below
        public FoodPoolManager _staticHamburgerBunBottomPoolManager;

        public PlateController PlateController
        {
            set => _plateController = value;
        }

        public FoodBeltRecyclerController FoodBeltRecyclerController
        {
            set => _foodBeltRecyclerController = value;
        }

        private void Awake()
        {
            _deckManager = GetComponent<DeckManager>();
        }

        private void Start()
        {
            HamburgerBunBottomPoolManagerSetup();
            Time.timeScale = 0.2f;
            Application.targetFrameRate = 144;
        }

        private void HamburgerBunBottomPoolManagerSetup()
        {
            _hamburgerBunBottomPoolManager       = _deckManager.GetHamburgerBunBottomPoolManager(gameObject);
            _staticHamburgerBunBottomPoolManager = _deckManager.GetStaticHamburgerBunBottomPoolManager(gameObject);
            
            _hamburgerBunBottomPoolManager.ObjectToPool       = (Resources.Load ("Food/HamburgerBunBottom"))       as GameObject;
            _staticHamburgerBunBottomPoolManager.ObjectToPool = (Resources.Load ("Food/StaticHamburgerBunBottom")) as GameObject;
        }
    }
}
