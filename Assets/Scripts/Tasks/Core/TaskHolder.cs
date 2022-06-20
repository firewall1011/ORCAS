using UnityEngine;

namespace ORCAS.Tasks
{
    public class TaskHolder : MonoBehaviour
    {
        public Task Task { protected set; get; } = null;
    }
}
