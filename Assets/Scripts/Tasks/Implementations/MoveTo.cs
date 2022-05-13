using System.Collections;
using UnityEngine;

namespace ORCAS
{
    public class MoveTo : Task
    {
        public readonly Transform target;

        public MoveTo(Transform target)
        {
            this.target = target;
        }

        public override IEnumerator Perform(GameObject agent)
        {
            if (agent.TryGetComponent(out UnityEngine.AI.NavMeshAgent navAgent))
            {
                if (!navAgent.SetDestination(target.position))
                {
                    InvokeOnExecutionEnded(false);
                    Debug.LogError("Cannot reach " + target.name);
                    Debug.Break();
                    yield break;
                }

                while (navAgent.pathPending) yield return null;
                
                while (navAgent.remainingDistance > navAgent.stoppingDistance)
                {
                    yield return null;
                }

                InvokeOnExecutionEnded(true);
            }
            else
            {
                InvokeOnExecutionEnded(false);
            }
        }
    }
}