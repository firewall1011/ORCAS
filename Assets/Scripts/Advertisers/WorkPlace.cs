using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using ORCAS.Utils;

namespace ORCAS
{
    public class WorkPlace : MonoBehaviourAdvertiser
    {
        [SerializeField] private ResourceReward[] _resourceRewardsPerHour;
        [SerializeField] private NeedReward[] _needRewardsPerHour;
        [SerializeField] private int _workingTime = 2;
        
        [SerializeField, Range(1, 24)] private int openingHour;
        [SerializeField, Range(1, 24)] private int closingHour;

        private IRewardable[] _rewardsPerHour;
        private IRewardable[] _rewardsTotal;

        private void Awake()
        {
            var rewardsPerHour = new List<IRewardable>();
            rewardsPerHour.AddRange(_resourceRewardsPerHour);
            rewardsPerHour.AddRange(_needRewardsPerHour);
            
            var rewardsTotal = new List<IRewardable>();
            rewardsTotal.AddRange(_resourceRewardsPerHour.Select(
                    reward => new ResourceReward(reward.Resource, (float)(reward.Delta * _workingTime)))
            );
            rewardsTotal.AddRange(_needRewardsPerHour.Select(
                    reward => new NeedReward(reward.Type, (float)(reward.Delta * _workingTime)))
            );

            _rewardsPerHour = rewardsPerHour.ToArray();
            _rewardsTotal = rewardsTotal.ToArray();
        }

        public override TaskSequence[] AdvertiseTasksFor(Agent agent)
        {
            if (!IsWorkOpen())
            {
                return new TaskSequence[0];
            }

            Task[] workTasks = new Task[] { new MoveTo(transform), new Work(_workingTime, _rewardsPerHour) };
            
            return new TaskSequence[]
            {  
                new TaskSequence(workTasks, _rewardsTotal)
            };
        }

        private bool IsWorkOpen()
        {
            return DateTimeHelper.IsBetweenHours(SimulationConfiguration.DateTimeManager.DateTime, openingHour, closingHour);
        }
    }
}
