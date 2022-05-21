using System;
using System.Collections;
using UnityEngine;

namespace ORCAS
{
    public class Work : Task
    {
        public readonly double workingTime;
        public readonly IRewardable[] rewardsPerHour;

        public Work(double workingTime, IRewardable[] rewards)
        {
            this.workingTime = workingTime;
            this.rewardsPerHour = rewards;
        }

        public Work(double workingTime, IRewardable reward)
        {
            this.workingTime = workingTime;
            this.rewardsPerHour = new IRewardable[1] { reward };
        }

        public override IEnumerator Perform(GameObject agent)
        {
            DateTime startingTime = SimulationConfiguration.DateTimeManager.DateTime;
            DateTime endingTime = SimulationConfiguration.DateTimeManager.DateTime.AddHours(workingTime);

            while (!_cancellationToken.IsCancellationRequested
                && SimulationConfiguration.DateTimeManager.DateTime < endingTime)
            {
                yield return null;
            }

            int totalWorkingHours = (int) 
                (SimulationConfiguration.DateTimeManager.DateTime - startingTime).TotalHours;

            ApplyReward(agent, totalWorkingHours);
            InvokeOnExecutionEnded(true);
        }

        private void ApplyReward(GameObject agent, int totalWorkingHours)
        {
            var agentComponent = agent.GetComponent<Agent>();

            foreach(var reward in rewardsPerHour)
            {
                for(int i = 0; i < totalWorkingHours; i++)
                {
                    reward.ApplyReward(agentComponent);
                }
            }
        }
    }
}