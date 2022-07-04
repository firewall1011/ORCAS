using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace ORCAS.Transport
{
    public class BusTransportSystem : MonoBehaviour, ITransportSystem
    {
        [field: SerializeField] public List<BusStop> BusStops { get; private set; } = new List<BusStop>();
        
        [SerializeField] private float minDistanceToCatchBus = 15f;
        [SerializeField] private float minDistanceToWalkToStop = 100f;

        private void Awake()
        {
            BusStops = transform.GetComponentsInChildren<BusStop>().ToList();
        }

        public IEnumerable<Transportation> GetTransportationOptions(Agent agent, Vector3 position, Vector3 destination)
        {
            List<Transportation> transportations = new List<Transportation>();
            
            var nearbyStops = BusStops.Where(t => Vector3.Distance(t.transform.position, position) <= minDistanceToWalkToStop);
            var acessibleStops = BusStops.Where(t => Vector3.Distance(t.transform.position, position) <= minDistanceToCatchBus);

            nearbyStops = nearbyStops.Except(acessibleStops);

            foreach(var stop in nearbyStops)
            {
                transportations.AddRange(agent.WalkingSystem.GetTransportationOptions(agent, position, stop.transform.position));
            }

            foreach(var stop in acessibleStops)
            {
                transportations.AddRange(stop.GetBusOptions());
            }

            return transportations;
        }
    }
}
