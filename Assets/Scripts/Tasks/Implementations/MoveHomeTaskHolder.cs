using UnityEngine;

namespace ORCAS.Tasks
{
    public class MoveHomeTaskHolder : TaskHolder
    {
        [SerializeField] private Transform _home;

        private void Awake()
        {
            Task = new MoveTo(_home);
        }
    }
}
