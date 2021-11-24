using System.Collections.Generic;
using Gameplay.Decks;
using Gameplay.Melee;
using Gameplay.NewInput.NewMelee;
using UnityEngine;

namespace Gameplay.Managers
{
    public class MeleePoolManager : ObjectPool
    {
       public static ObjectPool Instance { get; private set; }

        [SerializeField]
        public Queue<GameObject> queue;
        
        Vector3 objectTempPosition = new Vector3(-1500, -1500, 0);

        public GameObject objectToPool;
        public GameObject cachedGameObject;

        private GameObject _dequeueCachedGameObject;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            QueueSetup(); 
        }

        public override void QueueSetup()
        {
            queue = new Queue<GameObject>();
            var obj = Instantiate(objectToPool);
            
            //TODO extract to a method. make poolable objects generics and set the owner for any kind of them 
            obj.GetComponent<BaseMelee>().SetOwner(gameObject.transform, gameObject.GetComponent<DeckManager>());
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
                var obj = Instantiate(objectToPool);
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
