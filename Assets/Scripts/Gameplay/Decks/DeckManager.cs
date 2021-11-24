using Gameplay.Capabilities;
using Gameplay.Capabilities.CapabilityProps;
using Gameplay.Food;
using Gameplay.Input;
using Gameplay.Melee;
using Gameplay.NewInput.Decks;
using Gameplay.OldInput;
using Gameplay.StateMachine;
using Gameplay.StateMachine.ActionsAndStates;
using Gameplay.Wanimator;
using Gameplay.Wanimator.WanimationClips.Player;
using UnityEngine;

namespace Gameplay.Decks
{
    public class DeckManager : MonoBehaviour, IAnythingDeck
    {
        public BroadcasterDeck  broadcasterDeck;
        public PropsDeck        propsDeck;
        public CapabilitiesDeck capabilitiesDeck;
        public StatesDeck       statesDeck;
        public AnimationDeck    animationDeck;
        public MeleeDeck        playerMeleeDeck;
        public FoodDeck         foodDeck;

        public void Awake()
        {
            broadcasterDeck  = ScriptableObject.CreateInstance<BroadcasterDeck>();
            propsDeck        = ScriptableObject.CreateInstance<PropsDeck>();
            capabilitiesDeck = ScriptableObject.CreateInstance<CapabilitiesDeck>();
            statesDeck       = ScriptableObject.CreateInstance<StatesDeck>();
            animationDeck    = ScriptableObject.CreateInstance<AnimationDeck>();
            playerMeleeDeck  = UnityEngine.Resources.Load<MeleeDeck>($"Melee/PlayerMeleeDeck");
            foodDeck         = ScriptableObject.CreateInstance<FoodDeck>();
        }

        #region Broadcasters

        public InputBroadcaster GetNewInputBroadcaster()
        {
            return broadcasterDeck.NewInputBroadcaster();
        }

        public OldInputBroadCasterScriptableObject GetInputBroadCasterScriptableObject()
        {
            return broadcasterDeck.InputBroadcaster();
        }

        #endregion

        #region Capabilities

        public BonecoMovementCapabilityProps GetPlayerMovementCapabilityProps()
        {
            return propsDeck.BonecoMovementCapabilityProps();
        }
        
        public JumpCapabilityProps GetJumpCapabilityProps()
        {
            return propsDeck.JumpCapabilityProps();
        }
        
        public MeleeCapabilityProps GetMeleeCapabilityProps()
        {
            return propsDeck.MeleeCapabilityProps();
        }

        public RoofWalkCapabilityProps RoofWalkCapabilityProps()
        {
            return propsDeck.RoofWalkCapabilityProps();
        }

        public WallSlideProps WallSlideProps()
        {
            return propsDeck.WallSlideProps();
        }
        
        public JumpCapability GetJumpCapability(GameObject gO)
        {
            return capabilitiesDeck.JumpCapability(gO);
        }
        
        public BounceOnWallCapability GetBounceOnWallCapability(GameObject gO)
        {
            return capabilitiesDeck.BounceOnWallCapability(gO);
        }
        
        public MeleeCapability GetMeleeCapability(GameObject gO)
        {
            return capabilitiesDeck.MeleeCapability(gO);
        }

        #endregion

        #region StateMachine

        //TODO just for testing: 
        public StateMachine.StateMachine GetStateMachine(GameObject go)
        {
            return statesDeck.StateMachine(go);
        }
        
        public StateStack GetStateStack(GameObject go)
        {
            return statesDeck.StateStack(go);
        }
        
        public ActionStack GetActionStack(GameObject go)
        {
            return statesDeck.ActionStack(go);
        }
        
        public StateBroadcast GetStateBroadcast()
        {
            return statesDeck.StateBroadcast();
        }
        
        public ActionBroadcast GetBonecoActionBroadcast()
        {
            return statesDeck.ActionBroadcast();
        }
        
        public JumpingState GetJumpJumpingState(GameObject gO)
        {
            return statesDeck.JumpingState(gO);
        }
        
        public GroundedState GetGroundedState(GameObject gO)
        {
            return statesDeck.GroundedState(gO);
        }
        
        public HitToRightState GetHitToRightState(GameObject gO)
        {
            return statesDeck.HitToRightState(gO);
        }
        
        public HitToLeftState GetHitToLeftState(GameObject gO)
        {
            return statesDeck.HitToLeftState(gO);
        }
        
        public NeverHittedState GetNeverHittedState(GameObject gO)
        {
            return statesDeck.NeverHittedState(gO);
        }
        
        public BouncedState GetBouncedState(GameObject gO)
        {
            return statesDeck.BouncedState(gO);
        }
        
        public PlatedState GetPlatedState(GameObject gO)
        {
            return statesDeck.PlatedState(gO);
        }
        
        public WastedState GetWastedState(GameObject gO)
        {
            return statesDeck.WastedState(gO);
        }
        
        public RecycledState GetRecycledState(GameObject gO)
        {
            return statesDeck.RecycledState(gO);
        }
        
        public NoneState GetNoneState(GameObject gO)
        {
            return statesDeck.NoneState(gO);
        }
        
        public RunningState GetRunningState(GameObject gO)
        {
            return statesDeck.RunningState(gO);
        }
        
        public IdleState GetIdleState(GameObject gO)
        {
            return statesDeck.IdleState(gO);
        }
        
        public CrouchState GetCrouchState(GameObject gO)
        {
            return statesDeck.CrouchState(gO);
        }
        
        public FallingState GetFallingState(GameObject gO)
        {
            return statesDeck.FallingState(gO);
        }
        
        public MeleeAction GetMeleeAction(GameObject gO)
        {
            return statesDeck.MeleeAction(gO);
        }

        #endregion

        #region Wanimator
        
        public Animator GetAnimator(GameObject go)
        {
            return animationDeck.Animator(go);
        }
        
        public SpriteRenderer GetSpriteRenderer(GameObject go)
        {
            return animationDeck.SpriteRenderer(go);
        }

        public AnimationClipQueue GetAnimationClipQueue(GameObject go)
        {
            return animationDeck.AnimationClipQueue(go);
        }
        public RunningClip GetRunningClip(GameObject go)
        {
            return animationDeck.RunningClip(go);
        }
        
        public JumpingClip GetJumpingClip(GameObject go)
        {
            return animationDeck.JumpingClip(go);
        }
        
        public IdleClip GetIdleClip(GameObject go)
        {
            return animationDeck.IdleClip(go);
        }
        
        public CrouchClip GetCrouchClip(GameObject go)
        {
            return animationDeck.CrouchClip(go);
        }

        #endregion

        #region Melee

        public GameObject GetNewPlayerMeleeA()
        {
            return playerMeleeDeck.NewPlayerMeleeA();
        }
        
        public MeleePoolManager GetNewMeleePoolManager(GameObject go)
        {
            return playerMeleeDeck.NewMeleePoolManager(go);
        }

        #endregion

        #region Food

        public FoodMovementProps GetFoodMovementProps()
        {
            return foodDeck.FoodProps();
        }
        
        public BounceOnWallCapabilityProps GetBounceOnWallCapabilityProps()
        {
            return foodDeck.BounceOnWallCapabilityProps();
        }

        public FoodMotor GetFoodMotor(Transform foodTransform, FoodMovementProps foodMovementMovementCapabilityProps)
        {
            return foodDeck.FoodMotor(foodTransform, foodMovementMovementCapabilityProps);
        }

        public FoodController GetFoodController(GameObject go)
        {
            return foodDeck.FoodController(go);
        }
        
        #endregion

    }
}
