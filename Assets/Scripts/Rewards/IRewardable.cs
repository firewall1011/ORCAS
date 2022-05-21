namespace ORCAS
{
    public interface IRewardable
    {
        void ApplyReward(ORCAS.Agent agent);
        float GetCurrentValue(ORCAS.Agent agent);
        float GetAppliedValue(ORCAS.Agent agent);
    }
}