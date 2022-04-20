using System.Collections;
using System;
using UnityEngine;

namespace ORCAS
{
    public abstract class Task
    {
        protected CancellationToken _cancellationToken;
        
        public abstract IEnumerator Perform(GameObject agent, Action<Task> OnSuccess, Action<Task> OnFailure);
        public void Abort()
        {
            _cancellationToken.RequestCancellation();
        }
    }
}