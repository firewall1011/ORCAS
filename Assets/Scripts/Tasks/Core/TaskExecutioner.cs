using System.Collections.Generic;
using UnityEngine;

namespace ORCAS.Tasks
{
    public class TaskExecutioner : MonoBehaviour
    {
        public Queue<Task> TaskQueue = new Queue<Task>();

        private Task _currentTaskInExecution = null;

        public bool IsPerformingTask() => _currentTaskInExecution != null;

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