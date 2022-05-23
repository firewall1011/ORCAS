using System.Collections.Generic;
using UnityEngine;

namespace ORCAS
{
    public class NeedsController : MonoBehaviour
    {
        public List<Need> CurrentNeeds { get; private set; }
        public NeedsProfile Profile => _profile;

        [SerializeField] private NeedsProfile _profile;

        private void Awake()
        {
            InitializeNeeds();
        }

        private void Update()
        {
            DecayNeeds();
        }

        public void ApplyReward(NeedReward reward)
        {
            int index = GetNeedIndex(reward.Type);

            Need need = CurrentNeeds[index];
            need.Amount = GetResultingNeedAmount(reward);

            CurrentNeeds[index] = need;
        }

        public void ApplyReward(NeedReward[] rewards)
        {
            foreach(var reward in rewards)
            {
                ApplyReward(reward);
            }
        }

        public float GetResultingNeedAmount(NeedReward reward)
        {
            var need = GetNeed(reward.Type);
            return Mathf.Min(need.Amount + reward.Delta, Profile.MaximumNeedAmount);
        }

        public Need GetNeed(NeedType type) => CurrentNeeds.Find((need) => need.Type == type);
        public int GetNeedIndex(NeedType type) => CurrentNeeds.FindIndex((need) => need.Type == type);

        private void DecayNeeds()
        {
            for (int i = 0; i < CurrentNeeds.Count; i++)
            {
                var need = CurrentNeeds[i];
                need.Amount = Mathf.Max(need.Amount - _profile.GetDecayAmount(need.Type), 1f);
                CurrentNeeds[i] = need;
            }
        }

        private void InitializeNeeds()
        {
            CurrentNeeds = new List<Need>();

            foreach (var needType in _profile.NeedTypes)
            {
                CurrentNeeds.Add(new Need(needType, _profile.MaximumNeedAmount));
            }
        }
    }
}