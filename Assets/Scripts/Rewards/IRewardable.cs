namespace ORCAS
{
    public interface IRewardable
    {
        void ApplyReward(Agent agent);
        float GetCurrentValue(Agent agent);
        float GetAppliedValue(Agent agent);
        float GetScore(Agent agent, System.Func<float, float> atenuation);
    }
}