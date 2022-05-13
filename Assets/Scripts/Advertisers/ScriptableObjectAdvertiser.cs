using UnityEngine;

namespace ORCAS
{
    public abstract class ScriptableObjectAdvertiser : ScriptableObject, IAdvertiser
    {
        public virtual string GetTag() => "";
        public abstract Advertisement[] AdvertiseTasksFor(Agent agent);

        private void OnEnable()
        {
            GlobalAdvertiserQuerySystem.Instance?.Register(this);
        }

        private void OnDisable()
        {
            GlobalAdvertiserQuerySystem.Instance?.Unregister(this);
        }

    }
}
