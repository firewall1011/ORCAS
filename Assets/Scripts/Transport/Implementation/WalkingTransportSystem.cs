using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ORCAS.Transport
{
    public class WalkingTransportSystem : ITransportSystem
    {
        public IEnumerable<Transportation> GetTransportationOptions(Agent agent, Vector3 position, Vector3 destination)
        {
            var speed = agent.GetComponent<NavMeshAgent>().speed;
            float timeCost = Vector3.Distance(position, destination) * 100f / speed;
            var walkToDestination = new Transportation()
            {
                StartPosition = position,
                Destination = destination,
                TimeCost = timeCost,

            };
            return new Transportation[] { walkToDestination };
        }
        
        public IEnumerator Transport(Vector3 start, Vector3 destination, float time)
        {
            Debug.Log($"Walking from {start} to {destination}");
            yield return new WaitForSeconds(time);
            Debug.Log("Arrived by walking");
        }

    }
}
