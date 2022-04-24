using System.Collections;
using UnityEngine;

namespace ORCAS
{
    public class MoveToTask : Task
    {
        private readonly Transform _target;

        public MoveToTask(Transform target)
        {
            _target = target;
        }

        public override IEnumerator Perform(GameObject agent)
        {
            float deltaTime = 0f;
            float reachTime = 10f;

            while (!HasReachedTarget(agent))
            {
                if (_cancellationToken.IsCancellationRequested)
                {
                    InvokeOnExecutionEnded(false);
                    break;
                }

                agent.transform.position = Vector3.Lerp(agent.transform.position, _target.position, deltaTime / reachTime);
                deltaTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            InvokeOnExecutionEnded(true);
        }

        private bool HasReachedTarget(GameObject agent)
        {
            return Vector3.Distance(_target.position, agent.transform.position) < 1f;
        }
    }
}
