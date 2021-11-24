using System;
using Gameplay.Decks;
using Gameplay.Kitchen;
using UnityEngine;

namespace Gameplay.Food.Plate
{
    public class PlateController : MonoBehaviour
    {
        [SerializeField] 
        private DeckManager _deckManager;

        [SerializeField]
        private KitchenManager _kitchenManager;

        [SerializeField]
        private FoodStack _foodStack;

        [SerializeField]
        private FoodPoolManager _hamburgerBunBottomPoolManager;
        
        [SerializeField]
        private FoodPoolManager _staticHamburgerBunBottomPoolManager;

        private void Awake()
        {
            _deckManager    = GetComponentInParent<DeckManager>();
            _kitchenManager = GetComponentInParent<KitchenManager>();
        }

        private void Start()
        {
            _foodStack = _deckManager.GetFoodStack(gameObject);
            KitchenRegistry();

            _hamburgerBunBottomPoolManager       = _deckManager.GetHamburgerBunBottomPoolManager(gameObject);
            _staticHamburgerBunBottomPoolManager = _deckManager.GetStaticHamburgerBunBottomPoolManager(gameObject);
        }

        private void KitchenRegistry()
        {
            _kitchenManager.PlateController = this;
        }

        private void AddCollider()
        {
            var boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = true;
            boxCollider2D.size = new Vector2(1.5f, 1f);
            boxCollider2D.offset = new Vector2(1.24f, 1.26f);
        }

        private void OnTriggerEnter2D(Collider2D hittedGOCollider2D)
        {
            //TODO aqui a comida que tocou o objeto abaixo vai ter de se substituir  ao detectar se tocou uma comida estatica ou se tocou o Plate.
            //o bloco abaixo não é elegante pois tem diversos ifs. só os nomes das variáveis já são um smell. estudar uma abordagem mais OOP.
            //é provavel que este bloco nao seja atualizado num futuro proximo, entao se lembre sempre que comentarios mentem.
            
            GameObject thingThatIFell = hittedGOCollider2D.gameObject;
            Debug.Log(thingThatIFell.name);
            var componentFromThingThatIFell = thingThatIFell.GetComponent<Food>();
            
            if (thingThatIFell == null)
            {
                return;
            }

            //if (_foodController.collisionMask == 8)
            //{
            var cachedStaticFood = _staticHamburgerBunBottomPoolManager.Get();
            cachedStaticFood.transform.position = thingThatIFell.transform.position;
            //cachedStaticFood.transform.parent = gameObject.transform;
            cachedStaticFood.SetActive(true);

            componentFromThingThatIFell.EnableFood();
            _hamburgerBunBottomPoolManager.ReturnToPool(thingThatIFell);
            AddCollider();
            //}
            //transformFromFoodThaFellAboveMe.parent = gameObject.transform;
        }
    }
}
