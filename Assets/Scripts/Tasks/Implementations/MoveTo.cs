using ORCAS.Transport;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace ORCAS.Tasks
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
            var agentComp = agent.GetComponent<Agent>();
            var path = agentComp.TripPlanner.CalculatePath(agentComp, target);

            foreach(var node in path)
            {
                yield return node.Transport(agentComp);
                if (!node.Succeded)
                {
                    InvokeOnExecutionEnded(false);
                    yield break;
                }
            }

            InvokeOnExecutionEnded(true);
        }
    }
}