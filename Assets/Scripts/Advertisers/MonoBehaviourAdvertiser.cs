using UnityEngine;

namespace ORCAS
{
    public abstract class MonoBehaviourAdvertiser : MonoBehaviour, IAdvertiser
    {
        public abstract Advertisement[] AdvertiseTasksFor(Agent agent);
        public virtual string GetTag() => tag;

        private void OnEnable()
        {
            SimulationConfiguration.AdvertiserSystem?.Register(this);
        }

        private void OnDisable()
        {
            SimulationConfiguration.AdvertiserSystem?.Unregister(this);
        }
    }
}
