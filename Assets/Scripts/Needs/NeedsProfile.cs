using UnityEngine;

namespace ORCAS
{
    [CreateAssetMenu(menuName = "ORCAS/Profiles/Needs")]
    public class NeedsProfile : ScriptableObject 
    {
        public SerializedList<NeedType> NeedTypes;

        [SerializeField] private float _maximumNeedAmount;
        [SerializeField, HideInInspector] private float[] _decayAmounts;
        [SerializeField, HideInInspector] private float[] _scoringMultipliers;
        
        public float MaximumNeedAmount => _maximumNeedAmount;
        
        public float GetDecayAmount(NeedType type)
        {
            int index = NeedTypes.List.FindIndex((t) => t == type);
            return _decayAmounts[index];
        }

        public float GetScoreFactor(NeedType type)
        {
            int index = NeedTypes.List.FindIndex((t) => t == type);
            return _scoringMultipliers[index];
        }
    }
}