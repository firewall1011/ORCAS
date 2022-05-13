using UnityEngine;

namespace ORCAS
{
    public class Location : MonoBehaviourAdvertiser
    {
        [SerializeField] private NeedType _satisfiedNeed;
        [SerializeField] private float _rewardAmount = 20f;

#if UNITY_EDITOR || DEBUG
        private void Awake()
        {
            Debug.Assert(_satisfiedNeed != null);
        }
#endif

        public override Advertisement[] AdvertiseTasksFor(Agent agent)
        {
            return new Advertisement[1] { CreateAdvertisement() };
        }

        private Advertisement CreateAdvertisement()
        {
            return new Advertisement(new MoveWithNavMesh(transform), new Reward(_satisfiedNeed, _rewardAmount));
        }
    }
}
