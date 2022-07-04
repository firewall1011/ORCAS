using UnityEngine;

namespace ORCAS
{
    public class TransportationReward : IRewardable
    {
        public readonly Vector3 Destination;

        public TransportationReward(Vector3 destination)
        {
            Destination = destination;
        }

        public void ApplyReward(Agent agent)
        {
            return;
        }

        public float GetAppliedValue(Agent agent)
        {
            var travelTime = agent.TripPlanner.CalculateTripCost(agent, Destination, SimulationConfiguration.DateTimeManager.DateTime);
            return 1f / travelTime;
        }

        public float GetCurrentValue(Agent agent)
        {
            return 0f;
        }
    }
}
