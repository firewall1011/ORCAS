using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ORCAS.Advertisement
{
    public class GlobalAdvertiserQuerySystem : MonoBehaviour
    {
        private List<IAdvertiser> _advertisers = new List<IAdvertiser>();
        
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