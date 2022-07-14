using ORCAS.Tasks;
using UnityEngine;

namespace ORCAS.Advertisement
{

    public class Location : MonoBehaviourAdvertiser
    {
        [SerializeField] private string ActivityName;
        [SerializeField] private NeedType _satisfiedNeed;
        [SerializeField] private float _rewardAmount = 20f;

#if UNITY_EDITOR || DEBUG
        private void Awake()
        {
            Debug.Assert(_satisfiedNeed != null);
        }
#endif

        public override TaskSequence[] AdvertiseTasksFor(Agent agent)
        {
            return new TaskSequence[1] { CreateAdvertisement() };
        }

        private TaskSequence CreateAdvertisement()
        {
            NeedReward rewardPerHour = new NeedReward(_satisfiedNeed, _rewardAmount);
            TransportationReward transportCost = new TransportationReward(transform);
            
            var tasks = new Task[] { new MoveTo(transform), new Work(1, rewardPerHour) };
            var rewards = new IRewardable[] { rewardPerHour, transportCost };
            return new TaskSequence(tasks, rewards, ActivityName);
        }
    }
}
