using System;
using Gameplay.Decks;
using Gameplay.Wanimator;
using Gameplay.Wanimator.WanimationClips.Player;
using UnityEngine;

namespace Gameplay.NewInput.Decks
{
    [CreateAssetMenu(fileName = "AnimationDeck", menuName = "BonecoDeck/AnimationDeck")]
    public class AnimationDeck : BonecoDeck
    {
        public const String ANIMATION_BASE_PATH = "Animation/";
        public const String PLAYER = "Player";
        
        [SerializeField] 
        private Animator _animator;
        
        [SerializeField] 
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField] 
        private AnimationClipQueue _animationClips; 
        
        [SerializeField]
        private RunningClip _runningClip;
        
        [SerializeField]
        private JumpingClip _jumpingClip;
        
        [SerializeField]
        private IdleClip _idleClip;
        
        [SerializeField]
        private CrouchClip _crouchClip;

        public Animator Animator(GameObject gameObject)
        {
            if (_animator == null)
            {
                _animator = gameObject.AddComponent(typeof(Animator)) as Animator;
                
                try
                {
                    _animator.runtimeAnimatorController = Resources.Load($"{ANIMATION_BASE_PATH}{PLAYER}/Player") as RuntimeAnimatorController;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }
            return _animator;
        }
        
        public SpriteRenderer SpriteRenderer(GameObject gameObject)
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = gameObject.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            }
            return _spriteRenderer;
        }

        public AnimationClipQueue AnimationClipQueue(GameObject gameObject)
        {
            if (_animationClips == null)
            {
                _animationClips = gameObject.AddComponent(typeof(AnimationClipQueue)) as AnimationClipQueue;
            }
            return _animationClips;
        }
        
        public RunningClip RunningClip(GameObject gameObject)
        {
            if (_runningClip == null)
            {
                _runningClip = gameObject.AddComponent(typeof(RunningClip)) as RunningClip;
            }
            return _runningClip;
        }
        
        public JumpingClip JumpingClip(GameObject gameObject)
        {
            if (_jumpingClip == null)
            {
                _jumpingClip = gameObject.AddComponent(typeof(JumpingClip)) as JumpingClip;
            }
            return _jumpingClip;
        }
        
        public IdleClip IdleClip(GameObject gameObject)
        {
            if (_idleClip == null)
            {
                _idleClip = gameObject.AddComponent(typeof(IdleClip)) as IdleClip;
            }
            return _idleClip;
        }
        
        public CrouchClip CrouchClip(GameObject gameObject)
        {
            if (_crouchClip == null)
            {
                _crouchClip = gameObject.AddComponent(typeof(CrouchClip)) as CrouchClip;
            }
            return _crouchClip;
        }
    }
}
