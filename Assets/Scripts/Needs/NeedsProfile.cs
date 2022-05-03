using UnityEngine;

namespace ORCAS
{
    [CreateAssetMenu(menuName = "ORCAS/Needs Profile")]
    public class NeedsProfile : ScriptableObject 
    {
        public SerializedList<NeedType> NeedTypes;

        [SerializeField] private float _maximumNeedAmount;
        [SerializeField] private float[] _decayAmounts;
        [SerializeField] private float[] _scoringMultipliers;
        
        public float MaximumNeedAmount => _maximumNeedAmount;
        
        public float GetDecayAmount(NeedType type)
        {
            int index = NeedTypes.List.FindIndex((t) => t == type);
            return _decayAmounts[index];
        }

        public float GetScoringMultiplier(NeedType type)
        {
            int index = NeedTypes.List.FindIndex((t) => t == type);
            return _scoringMultipliers[index];
        }
    }
}