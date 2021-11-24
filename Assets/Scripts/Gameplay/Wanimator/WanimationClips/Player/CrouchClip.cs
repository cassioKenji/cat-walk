using UnityEngine;

namespace Gameplay.Wanimator.WanimationClips.Player
{
    public class CrouchClip : WanimationClip
    {
        public override void Start()
        {
            clip = Resources.Load<AnimationClip>($"{ANIMATION_BASE_PATH}{PLAYER}/Player_Crouch");
            base.Start();
        }
    }
}
