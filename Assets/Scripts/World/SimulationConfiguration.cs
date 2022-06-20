using ORCAS.Advertisement;
using UnityEngine;

namespace ORCAS
{
    [RequireComponent(typeof(DateTimeManager), typeof(GlobalAdvertiserQuerySystem))]
    public class SimulationConfiguration : MonoBehaviour
    {
        public static DateTimeManager DateTimeManager { get; private set; }
        public static GlobalAdvertiserQuerySystem AdvertiserSystem { get; private set; }

        private void Awake()
        {
            DateTimeManager = GetComponent<DateTimeManager>();
            AdvertiserSystem = GetComponent<GlobalAdvertiserQuerySystem>();
        }
    }
}
