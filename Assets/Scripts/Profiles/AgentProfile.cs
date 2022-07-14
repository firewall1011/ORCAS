using UnityEngine;

namespace ORCAS
{
    [CreateAssetMenu(menuName = "ORCAS/Profiles/Agent")]
    public class AgentProfile : ScriptableObject
    {
        [field: SerializeField] public NeedsProfile NeedsProfile { get; private set; }
        [field: SerializeField] public ResourcesProfile ResourcesProfile { get; private set; }
        [field: SerializeField] public float TransportScoreFactor { get; private set; }
        
    }
}