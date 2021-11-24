using System;
using UnityEngine;

namespace Gameplay.Wanimator.WanimationClips.Player
{
    public class IdleClip : WanimationClip
    {
        public override void Start()
        {
            clip = Resources.Load<AnimationClip>($"{ANIMATION_BASE_PATH}{PLAYER}/Player_Idle");
            base.Start();
        }
    }
}
