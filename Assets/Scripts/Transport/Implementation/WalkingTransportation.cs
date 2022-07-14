using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace ORCAS.Transport
{
    public class WalkingTransportation : Transportation
    {
        private readonly float _walkSpeed;

        public WalkingTransportation(Transform start, Transform destination, float cost, float walkSpeed) 
            : base(start, destination, cost)
        {
            _walkSpeed = walkSpeed;
        }

        public override IEnumerator Transport(Agent agent)
        {
            _success = false;
            Debug.Log($"Walking from {Start.name} to {Destination.name} will take {Cost}");

            if (!agent.TryGetComponent(out NavMeshAgent navAgent)) yield break;

            navAgent.speed = _walkSpeed;
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