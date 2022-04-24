﻿using UnityEngine;

namespace ORCAS
{
    public abstract class MonoBehaviourAdvertiser : MonoBehaviour, IAdvertiser
    {
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
