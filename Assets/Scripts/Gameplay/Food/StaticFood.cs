using System;
using UnityEngine;

namespace Gameplay.Food
{
    public class StaticFood : Food
    {
        private const bool _isStatic = true;

        private Food _thisFoodComponent;

        public new bool IsStatic {
            get => _isStatic;
        }

        private void Awake()
        {
            _thisFoodComponent = GetComponent<Food>();
        }

        public override void EnableFood()
        {
            _thisFoodComponent.enabled = true;
        }
        
        public override void DisableFood()
        {
            _thisFoodComponent.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D hittedGOCollider2D)
        {
            GameObject foodThatFellAboveMe = hittedGOCollider2D.gameObject;
            Food componentDynamicFoodThatFellAboveMe = foodThatFellAboveMe.GetComponent<Food>();
            
            //TODO in case I'd want to set the food as child
            //var transformFromFoodThaFellAboveMe = hittedGOCollider2D.transform;
            if (foodThatFellAboveMe == null || componentDynamicFoodThatFellAboveMe == null)
            {
                return;
            }
        }
    }
}
