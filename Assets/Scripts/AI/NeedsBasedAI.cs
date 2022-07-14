using System.Collections.Generic;
using UnityEngine;
using ORCAS.Tasks;
using ORCAS.Advertisement;

namespace ORCAS
{
    public class NeedsBasedAI : MonoBehaviour, IAgentAI
    {
        /*
         High-level AI loop:
            While there are actions in the queue, pop the next one off, perform it, and maybe get a reward
            If you run out of actions, perform action selection based on current needs, to find more actions
            If you still have nothing to do, do some fallback actions
         */
        private IAdvertisementSelector _selectionStrategy;

        private void Awake()
        {
            _selectionStrategy = GetComponent<IAdvertisementSelector>();
        }

        public bool SelectTaskSequence(Agent agent, List<TaskSequence> availableAdvertisements)
        {
            float[] advsScores = ScoreAdvertisements(agent, availableAdvertisements);

            TaskSequence selectedSequence = SelectAdvertisement(agent, advsScores, availableAdvertisements);

            EnqueueAdvertisements(agent.TaskExecutioner, selectedSequence);

            return selectedSequence.Tasks.Length > 0;
        }

        private void EnqueueAdvertisements(TaskExecutioner executioner, TaskSequence selectedTaskSequence)
        {
            Debug.Log($"{executioner.name} is {selectedTaskSequence.TaskName}");
            foreach (Task task in selectedTaskSequence.Tasks)
            {
                executioner.TaskQueue.Enqueue(task);
            }
        }

        private float[] ScoreAdvertisements(Agent agent, List<TaskSequence> advertisements)
        {
            float[] scores = new float[advertisements.Count];

            for (int i = 0; i < scores.Length; i++)
            {
                float total = 0;

                foreach (IRewardable reward in advertisements[i].Rewards)
                {
                    float newScore = reward.GetScore(agent, Atenuation);
                    total += newScore;
                }

                scores[i] = total;
            }

            return scores;
        }

        private TaskSequence SelectAdvertisement(Agent agent, float[] scores, List<TaskSequence> advertisements)
        {
            return _selectionStrategy.Select(agent, scores, advertisements);
        }

        private float Atenuation(float value)
        {
            return 10f / value;
        }
    }
}
