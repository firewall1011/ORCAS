namespace ORCAS
{
    public class NeedReward : IRewardable
    {
        public readonly NeedType Type;
        public readonly float Delta;

        public NeedReward(NeedType type, float delta)
        {
            Type = type;
            Delta = delta;
        }

        public void ApplyReward(Agent agent)
        {
            var controller = agent.GetComponent<NeedsController>();
            controller.ApplyReward(this);
        }

        public float GetAppliedValue(Agent agent)
        {
            return agent.GetComponent<NeedsController>().GetResultingNeedAmount(this);
        }

        public float GetCurrentValue(Agent agent)
        {
            return agent.GetComponent<NeedsController>().GetNeed(Type).Amount;
        }

        public void Deconstruct(out NeedType type, out float amount)
        {
            type = Type;
            amount = Delta;
        }
    }
}