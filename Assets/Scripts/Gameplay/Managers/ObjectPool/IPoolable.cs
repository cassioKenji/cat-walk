using Gameplay.Decks;
using UnityEngine;

namespace Gameplay.Managers
{
    public interface IPoolable
    {
        public abstract void SetOwner(Transform transform, DeckManager ownerDeck);
    }
}
