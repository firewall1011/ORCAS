using UnityEngine;
using ORCAS.Utils;

namespace ORCAS
{
    public class WorkPlace : MonoBehaviourAdvertiser
    {
        [SerializeField] private NeedType _satisfiedNeed;
        [SerializeField] private float _rewardAmount = 20f;
        [SerializeField] private double _workingTime = 2.5f;
        
        [SerializeField, Range(1, 24)] private int openingHour;
        [SerializeField, Range(1, 24)] private int closingHour;

#if UNITY_EDITOR || DEBUG
        private void Awake()
        {
            Debug.Assert(_satisfiedNeed != null);
        }
#endif
        public override TaskSequence[] AdvertiseTasksFor(Agent agent)
        {
            if (!IsWorkOpen())
            {
                return new TaskSequence[0];
            }

            NeedReward rewardPerHour = new NeedReward(_satisfiedNeed, _rewardAmount);
            NeedReward totalReward = new NeedReward(_satisfiedNeed, (int) (_rewardAmount * _workingTime));

            Task[] workTasks = new Task[] { new MoveTo(transform), new Work(_workingTime, rewardPerHour) };
            IRewardable[] rewards = new IRewardable[] { totalReward };
            return new TaskSequence[]
            {  
                new TaskSequence(workTasks, rewards)
            };
        }

        private bool IsWorkOpen()
        {
            return DateTimeHelper.IsBetweenHours(SimulationConfiguration.DateTimeManager.DateTime, openingHour, closingHour);
        }
    }
}
