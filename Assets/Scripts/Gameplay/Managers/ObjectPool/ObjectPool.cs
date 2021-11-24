using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Managers
{
    public abstract class ObjectPool : MonoBehaviour, IObjectPool
    {
        public abstract void QueueSetup();
        public abstract void Add(GameObject otherGameObject);
        public abstract void ReturnToPool(GameObject otherGameObject);
        public abstract GameObject Get();
        public abstract GameObject ResetObjectToTempPosition(GameObject otherGameObject);
    }
}
