namespace ORCAS
{
    public struct Reward
    {
        public NeedType NeedType;
        public float Amount;

        public Reward(NeedType needType, float amount)
        {
            NeedType = needType;
            Amount = amount;
        }

        public void Deconstruct(out NeedType rewardedNeedType, out float rewardAmount)
        {
            rewardedNeedType = NeedType;
            rewardAmount = Amount;
        }
    }
}