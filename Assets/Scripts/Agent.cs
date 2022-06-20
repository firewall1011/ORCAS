using System.Collections.Generic;
using UnityEngine;
using ORCAS.Tasks;

namespace ORCAS
{
    public class Agent : MonoBehaviour
    {
        public Queue<Task> TaskQueue = new Queue<Task>();

        [SerializeField] private TaskHolder _fallbackTaskHolder;
        [SerializeField] private TaskExecutioner _taskExecutioner;
        private Task _currentTaskInExecution = null;

        public bool IsPerformingTask() => _currentTaskInExecution != null;

        public void PerformFallbackTask()
        {
            TryPerformTask(_fallbackTaskHolder.Task);
        }

        public void PerformTaskInQueue()
        {
            var task = TaskQueue.Dequeue();

            TryPerformTask(task);
        }

        public bool TryPerformTask(Task task)
        {
            if (IsPerformingTask())
            {
                return false;
            }

            task.OnExecutionEnded += Task_OnExecutionEnded;
            _currentTaskInExecution = task;

            StartCoroutine(task.Perform(gameObject));

            return true;
        }

        public void AbortCurrentTask()
        {
            _currentTaskInExecution.Abort();
        }

        private void Task_OnExecutionEnded(bool success)
        {
            _currentTaskInExecution = null;
        }
    }
}
