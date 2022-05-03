using System.Collections;
using UnityEngine;

namespace ORCAS
{
    public class MoveWithNavMesh : Task
    {
        public readonly Transform target;

        public MoveWithNavMesh(Transform target)
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
                    yield break;
                }

                while(navAgent.remainingDistance > float.Epsilon)
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