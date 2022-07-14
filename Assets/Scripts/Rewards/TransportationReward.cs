using System;
using UnityEngine;

namespace ORCAS
{
    public class TransportationReward : IRewardable
    {
        public readonly Transform Destination;

        public TransportationReward(Transform destination)
        {
            Destination = destination;
        }

        public void ApplyReward(Agent agent)
        {
            return;
        }

        public float GetAppliedValue(Agent agent)
        {
            var travelTime = agent.TripPlanner.CalculateTripCost(agent, Destination);
            return travelTime;
        }

        public float GetCurrentValue(Agent agent)
        {
            return 0f;
        }

        public float GetScore(Agent agent, Func<float, float> atenuationFunc)
        {
            float newValue = GetAppliedValue(agent);

            return atenuationFunc(newValue) * agent.Profile.TransportScoreFactor;
        }
    }
}
