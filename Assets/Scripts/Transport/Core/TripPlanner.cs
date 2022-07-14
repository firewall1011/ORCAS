using System.Collections.Generic;
using UnityEngine;

namespace ORCAS.Transport
{
    public class TripPlanner : MonoBehaviour
    {
        public IEnumerable<Transportation> CalculatePath(Agent agent, Transform destination)
        {
            var planner = new PathCalculator(agent, destination);
            var path = planner.CalculatePath();
            return path;
        }

        public float CalculateTripCost(Agent agent, Transform destination)
        {
            var planner = new PathCalculator(agent, destination);
            var path = planner.CalculatePath();
            return planner.CalculateCost(path);
        }
    }
}