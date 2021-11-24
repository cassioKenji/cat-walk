using System;
using Gameplay.Capabilities.CapabilityProps;
using Gameplay.Food;
using UnityEngine;

namespace Gameplay.Decks
{
    [CreateAssetMenu(fileName = "FoodsDeck", menuName = "BonecoDeck/FoodDeck")]
    public class FoodDeck : BonecoDeck
    {
        [SerializeField]
        private FoodMovementProps foodMovementMovementCapabilityProps;
        
        [SerializeField]
        private BounceOnWallCapabilityProps _bounceOnWallCapabilityProps;
        
        [SerializeField]
        private FoodMotor _foodMotor;

        [SerializeField]
        private FoodController _foodController;

        [SerializeField]
        private StateMachine.StateMachine _foodStateMachine;
        
        [SerializeField]
        private StateMachine.StateStack _stateStack;
        
        [SerializeField]
        private StateMachine.ActionStack _actionStack;

        public FoodMovementProps FoodProps()
        {
            if (foodMovementMovementCapabilityProps == null)
            {
                foodMovementMovementCapabilityProps = ScriptableObject.CreateInstance<FoodMovementProps>();
            }

            return foodMovementMovementCapabilityProps;
        }
        
        public BounceOnWallCapabilityProps BounceOnWallCapabilityProps()
        {
            if (_bounceOnWallCapabilityProps == null)
            {
                _bounceOnWallCapabilityProps = ScriptableObject.CreateInstance<BounceOnWallCapabilityProps>();
            }

            return _bounceOnWallCapabilityProps;
        }

        public FoodMotor FoodMotor(Transform foodTransform, FoodMovementProps foodMovementMovCapabilityProps)
        {
            if (_foodMotor == null)
            {
                _foodMotor = new FoodMotor(foodTransform, foodMovementMovCapabilityProps);
            }
            return _foodMotor;
        }

        public FoodController FoodController(GameObject go)
        {
            if (_foodController == null)
            {
                _foodController = go.AddComponent(typeof(FoodController)) as FoodController;
            }

            return _foodController;
        }
        
        public StateMachine.StateMachine StateMachine(GameObject go)
        {
            if (_foodStateMachine == null)
            {
                _foodStateMachine = go.AddComponent(typeof(StateMachine.StateMachine)) as StateMachine.StateMachine;
            }

            return _foodStateMachine;
        }
        
        public StateMachine.StateStack StateStack(GameObject go)
        {
            if (_stateStack == null)
            {
                _stateStack = go.AddComponent(typeof(StateMachine.StateStack)) as StateMachine.StateStack;
            }

            return _stateStack;
        }
        
        public StateMachine.ActionStack ActionStack(GameObject go)
        {
            if (_actionStack == null)
            {
                _actionStack = go.AddComponent(typeof(StateMachine.ActionStack)) as StateMachine.ActionStack;
            }

            return _actionStack;
        }
    }
}
