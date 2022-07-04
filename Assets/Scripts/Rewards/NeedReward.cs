using System;

namespace ORCAS
{
    [System.Serializable]
    public class NeedReward : IRewardable
    {
        public NeedType Type;
        public float Delta;

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

        public float GetScore(Agent agent, Func<float, float> atenuationFunc)
        {
            float pastValue = GetCurrentValue(agent);
            float newValue =  GetAppliedValue(agent);

            return atenuationFunc(pastValue) - atenuationFunc(newValue);
        }
    }
}