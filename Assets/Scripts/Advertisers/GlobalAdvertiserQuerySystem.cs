using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ORCAS
{
    public class GlobalAdvertiserQuerySystem : MonoBehaviour
    {
        public static GlobalAdvertiserQuerySystem Instance = null;

        private List<IAdvertiser> _advertisers = new List<IAdvertiser>();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }

            Instance = this;
        }

        public List<IAdvertiser> QueryAllAdvertisers() => _advertisers;
        public List<IAdvertiser> QueryAdvertisers(Func<IAdvertiser, bool> filter)
        {
            return _advertisers.Where(filter).ToList();
        }

        public void Register(IAdvertiser advertiser)
        {
            _advertisers.Add(advertiser);
        }

        public void Unregister(IAdvertiser advertiser)
        {
            _advertisers.Remove(advertiser);
        }

    }
}