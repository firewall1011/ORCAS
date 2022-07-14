using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ORCAS.Transport
{
    public class WalkingTransportSystem : MonoBehaviour, ITransportSystem
    {
        [SerializeField, Min(1f)] private float _walkSpeed;

        public IEnumerable<Transportation> GetTransportationOptions(Agent agent, Transform current, Transform destination)
        {
            var speed = agent.GetComponent<NavMeshAgent>().speed;
            float timeCost = Vector3.Distance(current.position, destination.position) * 100f / speed;
            var walkToDestination = new WalkingTransportation(current, destination, timeCost, _walkSpeed);
            return new Transportation[] { walkToDestination };
        }
    }
}
