using UnityEngine;

namespace ORCAS
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
