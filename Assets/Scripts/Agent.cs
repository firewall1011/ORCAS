using UnityEngine;
using ORCAS.Tasks;

namespace ORCAS
{
    [RequireComponent(typeof(TaskExecutioner))]
    public class Agent : MonoBehaviour
    {
        public TaskExecutioner TaskExecutioner { get; private set; }
        
        [SerializeField] private TaskHolder _fallbackTaskHolder;
        
        private IAgentAI _ai;

        private void Awake()
        {
            _ai = GetComponent<IAgentAI>();
            TaskExecutioner = GetComponent<TaskExecutioner>();
        }

        private void Update()
        {
            if (TaskExecutioner.IsPerformingTask())
            {
                return;
            }

            if (TaskExecutioner.TaskQueue.Count == 0)
            {
                if (!_ai.SelectTaskSequence(this))
                {
                    PerformFallbackTask();
                }
            }
            else
            {
                TaskExecutioner.PerformTaskInQueue();
            }
        }

        private void PerformFallbackTask()
        {
            TaskExecutioner.TryPerformTask(_fallbackTaskHolder.Task);
        }
    }
}
