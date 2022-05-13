using UnityEngine;

namespace ORCAS
{
    public class WorkPlace : MonoBehaviourAdvertiser
    {
        [SerializeField] private NeedType _satisfiedNeed;
        [SerializeField] private float _rewardAmount = 20f;
        [SerializeField] private double _workingTime = 2.5f;

#if UNITY_EDITOR || DEBUG
        private void Awake()
        {
            Debug.Assert(_satisfiedNeed != null);
        }
#endif
        public override Advertisement[] AdvertiseTasksFor(Agent agent)
        {
            Reward reward = new Reward(_satisfiedNeed, _rewardAmount);
            
            return new Advertisement[1] 
            {  
                new Advertisement(new Work(_workingTime, reward), reward)
                //new Advertisement(new MoveTo(transform), new Reward(_satisfiedNeed, 0f)),
            };
        }

    }
}
