using System.Collections.Generic;
using Gameplay.Decks;
using Gameplay.Managers;
using Gameplay.NewInput.Decks;
using UnityEngine;

namespace Gameplay.Melee
{
    public class MeleePoolManager : ObjectPool
    {
        public DeckManager deckManager;
        
        public Queue<GameObject> queue;
        
        Vector3 objectTempPosition = new Vector3(-1500, -1500, 0);

        //public GameObject objectToPool;
        public GameObject cachedGameObject;

        private GameObject _dequeueCachedGameObject;

        private void Awake()
        {
            deckManager = GetComponent<DeckManager>();
        }

        private void Start()
        {
            QueueSetup(); 
        }

        public override void QueueSetup()
        {
            queue = new Queue<GameObject>();
            var obj = deckManager.GetNewPlayerMeleeA();
            
            //TODO extract to a method. make poolable objects generics and set the owner for any kind of them 
            obj.GetComponent<BaseMelee>().SetOwner(gameObject.transform, gameObject.GetComponent<DeckManager>());
            print(obj.name);
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
                var obj = deckManager.GetNewPlayerMeleeA();
                Add(obj);
            }

            _dequeueCachedGameObject = queue.Dequeue();
            return _dequeueCachedGameObject;
        }

        public override GameObject ResetObjectToTempPosition(GameObject otherGameObject)
        {
            //tempZRotation = otherGameObject.transform.rotation.z;
            //otherGameObject.transform.Rotate(0,0, -tempZRotation);
            otherGameObject.transform.position = objectTempPosition;
            return otherGameObject;
        }
    }
}
