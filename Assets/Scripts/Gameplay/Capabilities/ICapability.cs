using System.Collections;

namespace Gameplay.Capabilities
{
    public interface ICapability
    {
        public abstract void StartCapability();

        public IEnumerator EnterCapability();

        public abstract void ExitCapability();
    }
}
