using System;
using Gameplay.Decks;
using Gameplay.NewInput.Decks;
using Gameplay.StateMachine;
using Gameplay.Wanimator.WanimationClips.Player;
using UnityEngine;

namespace Gameplay.Wanimator
{
    public class WanimationClip : MonoBehaviour
    {
        public const String ANIMATION_BASE_PATH = "Animation/";
        public const String PLAYER = "Player";
        
        public DeckManager deckManager;
        
        private Animator animator;
        public AnimationClip clip;
        public string clipName;
        public int id;

        private void Awake()
        {
            deckManager = GetComponentInParent<DeckManager>();
        }

        public virtual void Start()
        {
            animator = deckManager.GetAnimator(gameObject);
            
            clipName = clip.name;
            id = Animator.StringToHash(clipName);
        }

        public virtual void OnAnimationEnter()
        {
            Play();
        }

        public void OnAnimationExit()
        {
           //maybe some logic to detect if there is a need to play a transtion 
        }

        public void Play()
        {
            animator.Play(clipName);
        }
    }
}

