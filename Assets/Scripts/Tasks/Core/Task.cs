using System.Collections;
using System;
using UnityEngine;

namespace ORCAS
{
    public abstract class Task
    {
        public event Action<bool> OnExecutionEnded;
        protected CancellationToken _cancellationToken;

        public abstract IEnumerator Perform(GameObject agent);
        public void Abort()
        {
            _cancellationToken.RequestCancellation();
        }

        protected void InvokeOnExecutionEnded(bool success)
        {
            OnExecutionEnded?.Invoke(success);
        }
    }
}