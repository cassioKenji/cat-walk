using UnityEngine;

namespace Gameplay.Food
{
    [CreateAssetMenu(menuName = "FoodTypes/FoodType")]
    public class FoodType : ScriptableObject
    {
        public FoodEnum foodType;
        public string foodName = "";
        public Sprite foodSprite;
    }
}