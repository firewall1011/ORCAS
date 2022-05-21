using UnityEngine;

namespace ORCAS
{
    public abstract class ScriptableObjectAdvertiser : ScriptableObject, IAdvertiser
    {
        public virtual string GetTag() => "";
        public abstract TaskSequence[] AdvertiseTasksFor(Agent agent);

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
