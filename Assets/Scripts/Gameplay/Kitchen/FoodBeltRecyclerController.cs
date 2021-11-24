using Gameplay.Decks;
using Gameplay.Food;
using UnityEngine;

namespace Gameplay.Kitchen
{
    public class FoodBeltRecyclerController : MonoBehaviour
    {
        [SerializeField] 
        private DeckManager _deckManager;

        [SerializeField]
        private KitchenManager _kitchenManager;
        
        private void Awake()
        {
            _deckManager    = GetComponentInParent<DeckManager>();
            _kitchenManager = GetComponentInParent<KitchenManager>();
        }

        private void Start()
        {
            KitchenRegistry();
        }

        private void KitchenRegistry()
        {
            _kitchenManager.FoodBeltRecyclerController = this;
        }
    }
}
