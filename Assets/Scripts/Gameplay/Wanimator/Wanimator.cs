using System;
using Gameplay.Decks;
using Gameplay.Input;
using Gameplay.NewInput;
using Gameplay.NewInput.Decks;
using Gameplay.OldInput;
using Gameplay.StateMachine;
using Gameplay.Wanimator.WanimationClips.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Wanimator
{
    public class Wanimator : MonoBehaviour
    {
        public DeckManager deckManager;
        
        public StateBroadcast stateBroadcast;
        public ActionBroadcast actionBroadcast;
        
        [SerializeField]
        public AnimationClipQueue animationClips;
        public SpriteRenderer spriteRenderer;

        public InputBroadcaster inputBroadcaster;

        [FormerlySerializedAs("inputBroadCaster")] public OldInputBroadCasterScriptableObject oldInputBroadCasterScriptableObject;
        
        WanimationClip currentClip;
        private RunningClip _runningClip;
        private IdleClip _idleClip;
        private JumpingClip _jumpingClip;
        private MeleeClip _meleeClip;
        private CrouchClip _crouchClip;
        private RoofedClip _roofedClip;
        private RoofRuniingClip _roofRuniingClip;
        private WallSlideClip _wallSlideClip;
        
        private void Awake()
        {
            deckManager = GetComponentInParent<DeckManager>();
            
            //animationClips = GetComponent<AnimationClipQueue>();
            
        }
        
        private void Start()
        {
            _idleClip      = deckManager.GetIdleClip(gameObject);
            _runningClip   = deckManager.GetRunningClip(gameObject);
            _jumpingClip   = deckManager.GetJumpingClip(gameObject);
            _crouchClip = deckManager.GetCrouchClip(gameObject);
            animationClips = deckManager.GetAnimationClipQueue(gameObject);
            spriteRenderer = deckManager.GetSpriteRenderer(gameObject);

            actionBroadcast = deckManager.GetBonecoActionBroadcast();
            stateBroadcast  = deckManager.GetStateBroadcast();

            inputBroadcaster = deckManager.GetNewInputBroadcaster();

            //_newIdleClip = GetComponent<IdleClip>();
            //_jumpingClip = GetComponent<JumpingClip>();
            //_meleeClip = GetComponent<MeleeClip>();
            //_crouchClip = GetComponent<CrouchClip>();
            //_roofedClip = GetComponent<RoofedClip>();
            //_roofRuniingClip = GetComponent<RoofRuniingClip>();
            //_wallSlideClip = GetComponent<WallSlideClip>();
        }

        void Update()
        {
            SetFacingSprite();
            
            //TODO add events instead a bunch of ifs
            
            if (actionBroadcast.action == Actions.None && stateBroadcast.state == States.Grounded)
            {
                SetAnimationClip(_runningClip);
                return;
            }

            if (actionBroadcast.action == Actions.None && stateBroadcast.state == States.Running )
            {
                SetAnimationClip(_runningClip);
                return;
            }
            
            if (actionBroadcast.action == Actions.None && stateBroadcast.state == States.Jumping)
            {
                SetAnimationClip(_jumpingClip);
                return;
            }
            
            if (actionBroadcast.action == Actions.None && stateBroadcast.state == States.Idle)
            {
                SetAnimationClip(_idleClip);
                return;
            }
            
            if (actionBroadcast.action == Actions.None && stateBroadcast.state == States.Crouch)
            {
                SetAnimationClip(_crouchClip);
                return;
            }
            
            /*

            if (playerActionBroadcast.action == Actions.Melee && playerStateBroadcast.state == States.Jumping)
            {
                SetAnimationClip(_meleeClip);
                return;
            }
            
            if (playerActionBroadcast.action == Actions.Melee && playerStateBroadcast.state == States.Grounded)
            {
                SetAnimationClip(_meleeClip);
            }
            
            if (playerActionBroadcast.action == Actions.None && playerStateBroadcast.state == States.Crouch)
            {
                SetAnimationClip(_crouchClip);
            }
            
            if (playerActionBroadcast.action == Actions.None && playerStateBroadcast.state == States.Roofed)
            {
                SetAnimationClip(_roofedClip);
            }
            
            if (playerActionBroadcast.action == Actions.None && playerStateBroadcast.state == States.RoofRunning)
            {
                SetAnimationClip(_roofRuniingClip);
            }
            
            if (playerActionBroadcast.action == Actions.None && playerStateBroadcast.state == States.WallSlide)
            {
                SetAnimationClip(_wallSlideClip);
            } */
        }

        public void SetAnimationClip(WanimationClip clip)
        {
            if (IsCurrentClipEqualsTo(clip)) return;
            DequeueClip()?.OnAnimationExit();
            
            clip.OnAnimationEnter();
            
            currentClip = clip;
            EnqueueClip();
        }

        public bool IsCurrentClipEqualsTo(WanimationClip clip)
        {
            return clip == currentClip;
        }
        
        public WanimationClip DequeueClip()
        {
            if (animationClips.queue.Count < 1) return null;
            return animationClips.queue.Dequeue();
        }

        public void EnqueueClip()
        {
            animationClips.queue.Enqueue(currentClip);
        }
        
        void SetFacingSprite()
        {
            if (inputBroadcaster.NewInputDirection.x < 0)
            {
                if (stateBroadcast.state != States.WallSlide)
                {
                    spriteRenderer.flipX = true;
                    return;
                }
            }

            if (stateBroadcast.state != States.WallSlide)
            {
                if (inputBroadcaster.NewInputDirection.x > 0)
                {
                    spriteRenderer.flipX = false;
                } 
            }
        }
    }
}
