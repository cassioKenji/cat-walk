using UnityEngine;

namespace Gameplay.Wanimator.WanimationClips.Player
{
    [CreateAssetMenu(menuName = "Wanimator/WanimationClip")]
    public class WanimationClipScriptableObject : ScriptableObject
    {
        public UnityEngine.AnimationClip wanimation;

        public int NameToHashId()
        {
            return this.wanimation.GetHashCode();
        }
    }
}
