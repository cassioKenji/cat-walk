using UnityEngine;

namespace Gameplay.Managers
{
    public interface IObjectPool
    {
        public void QueueSetup();
        public void Add(GameObject otherGameObject);
        public void ReturnToPool(GameObject otherGameObject);
        public GameObject Get();
        GameObject ResetObjectToTempPosition(GameObject otherGameObject);
    }
}
