using System.Collections.Generic;
using Gameplay.Decks;
using Gameplay.Managers;
using Gameplay.Melee;
using UnityEngine;

namespace Gameplay.Food
{
    public class FoodPoolManager : ObjectPool
    {
        
        public static FoodPoolManager Instance { get; private set; }
        
        public FoodDeck foodDeck;
        
        public Queue<GameObject> queue;
        
        Vector3 objectTempPosition = new Vector3(-1500, -1500, 0);

        //public GameObject objectToPool;
        public GameObject cachedGameObject;

        private GameObject _dequeueCachedGameObject;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
            
            DontDestroyOnLoad(gameObject);

            foodDeck = ScriptableObject.CreateInstance<FoodDeck>();
        }

        private void Start()
        {
            QueueSetup(); 
        }

        public override void QueueSetup()
        {
            queue = new Queue<GameObject>();
            var obj = foodDeck.NewStaticFood();
            
            //TODO extract to a method. make poolable objects generics and set the owner for any kind of them 
            //obj.GetComponent<BaseMelee>().SetOwner(gameObject.transform, gameObject.GetComponent<DeckManager>());
            //print(obj.name);
            Add(obj);
        }

        public override void Add(GameObject otherGameObject)
        {
            otherGameObject.SetActive(false);
            ResetObjectToTempPosition(otherGameObject);
            queue.Enqueue(otherGameObject);
        }

        public override void ReturnToPool(GameObject otherGameObject)
        {
            cachedGameObject = ResetObjectToTempPosition(otherGameObject);
            Add(cachedGameObject);
        }

        public override GameObject Get()
        {
            if (queue.Count <= 0)
            {
                var obj = foodDeck.NewStaticFood();
                Add(obj);
            }

            _dequeueCachedGameObject = queue.Dequeue();
            return _dequeueCachedGameObject;
        }

        public override GameObject ResetObjectToTempPosition(GameObject otherGameObject)
        {
            otherGameObject.transform.position = objectTempPosition;
            return otherGameObject;
        }
    }
}
