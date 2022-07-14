using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace ORCAS.Transport
{
    public class BusTransportation : Transportation
    {
        private readonly float _speed;

        public BusTransportation(Transform start, Transform destination, float cost, float speed)
            : base(start, destination, cost)
        {
            _speed = speed;
        }

        public override IEnumerator Transport(Agent agent)
        {
            _success = false;
            Debug.Log($"Taking bus from {Start.name} to {Destination.name} will take {Cost}");

            if (!agent.TryGetComponent(out NavMeshAgent navAgent)) yield break;

            navAgent.speed = _speed;
            if (!navAgent.SetDestination(Destination.position))
            {
                Debug.LogError("Cannot reach " + Destination.name);
                Debug.Break();
                yield break;
            }

            while (navAgent.pathPending) yield return null;

            while (navAgent.remainingDistance > navAgent.stoppingDistance)
            {
                yield return null;
            }

            _success = true;
        }
    }
}
