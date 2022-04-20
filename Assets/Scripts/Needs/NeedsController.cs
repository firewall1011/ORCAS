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

        private void DecayNeeds()
        {
            for (int i = 0; i < CurrentNeeds.Count; i++)
            {
                var need = CurrentNeeds[i];
                need.Amount = Mathf.Max(need.Amount - _profile.DecayAmount, 0f);
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