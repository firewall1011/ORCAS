using UnityEngine;

namespace ORCAS
{
    [CreateAssetMenu(menuName = "ORCAS/Needs Profile")]
    public class NeedsProfile : ScriptableObject 
    {
        public SerializedList<NeedType> NeedTypes;
        public float MaximumNeedAmount => _maximumNeedAmount;
        public float DecayAmount => _decayAmount;

        [SerializeField] private float _maximumNeedAmount;
        [SerializeField] private float _decayAmount;
    }
}
