using UnityEngine;

namespace ORCAS
{
    public class MoveHomeWithNavMeshTaskHolder : TaskHolder
    {
        [SerializeField] private Transform _home;

        private void Awake()
        {
            Task = new MoveWithNavMesh(_home);
        }
    }
}
