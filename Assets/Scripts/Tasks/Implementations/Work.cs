using System;
using System.Collections;
using UnityEngine;

namespace ORCAS
{
    public class Work : Task
    {
        public readonly double workingTime;
        public readonly Reward[] rewards;

        public Work(double workingTime, Reward[] rewards)
        {
            this.workingTime = workingTime;
            this.rewards = rewards;
        }

        public Work(double workingTime, Reward reward)
        {
            this.workingTime = workingTime;
            this.rewards = new Reward[1] { reward };
        }

        public override IEnumerator Perform(GameObject agent)
        {
            DateTime endingTime = SimulationConfiguration.DateTimeManager.DateTime.AddHours(workingTime);

            while (SimulationConfiguration.DateTimeManager.DateTime < endingTime)
            {
                yield return null;
            }

            InvokeOnExecutionEnded(true);
        }
    }
}