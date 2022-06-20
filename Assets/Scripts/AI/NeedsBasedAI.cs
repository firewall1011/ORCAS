using ORCAS.Advertisement;
using ORCAS.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ORCAS
{
    public class NeedsBasedAI : MonoBehaviour
    {
        /*
         High-level AI loop:
            While there are actions in the queue, pop the next one off, perform it, and maybe get a reward
            If you run out of actions, perform action selection based on current needs, to find more actions
            If you still have nothing to do, do some fallback actions
         */
        private Agent _agent;
        private NeedsController _needsController;
        private IAdvertisementSelector _selectionStrategy;

        private void Awake()
        {
            _selectionStrategy = GetComponent<IAdvertisementSelector>();
            _agent = GetComponent<Agent>();
            _needsController = _agent.GetComponent<NeedsController>();
        }

        private void Update()
        {
            if (_agent.IsPerformingTask())
            {
                return;
            }

            if(_agent.TaskQueue.Count == 0)
            {
                if (!SelectTaskSequence())
                {
                    PerformFallbackTask();
                }
            }
            else
            {
                PerformTaskInQueue();
            }
        }

        public void PerformTaskInQueue()
        {
            _agent.PerformTaskInQueue();
        }

        public bool SelectTaskSequence()
        {
            List<TaskSequence> availableAds = GetAvailableAdvertisements(_agent);

            float[] advsScores = ScoreAdvertisements(availableAds);

            TaskSequence selectedSequence = SelectAdvertisement(advsScores, availableAds);
            
            EnqueueAdvertisements(selectedSequence);

            return selectedSequence.Tasks.Length > 0;
        }

        private void EnqueueAdvertisements(TaskSequence selectedTaskSequence)
        {
            foreach (Task task in selectedTaskSequence.Tasks)
            {
                _agent.TaskQueue.Enqueue(task);
            }
        }

        private float[] ScoreAdvertisements(List<TaskSequence> advertisements)
        {
            float[] scores = new float[advertisements.Count];

            for (int i = 0; i < scores.Length; i++)
            {
                float total = 0;

                foreach (IRewardable reward in advertisements[i].Rewards)
                {
                    float pastValue = reward.GetCurrentValue(_agent);
                    float newValue = reward.GetAppliedValue(_agent);

                    float newScore = Atenuation(pastValue) - Atenuation(newValue);
                    total += newScore;//* _needsController.Profile.GetScoringMultiplier(reward.Type);
                    //TODO: Apply scoring profile
                }

                scores[i] = total;
            }

            return scores;
        }

        private TaskSequence SelectAdvertisement(float[] scores, List<TaskSequence> advertisements)
        {
            return _selectionStrategy.Select(_agent, scores, advertisements);
        }

        private static List<TaskSequence> GetAvailableAdvertisements(Agent agent)
        {
            var advertisers = SimulationConfiguration.AdvertiserSystem.QueryAllAdvertisers();

            List<TaskSequence> advertisements = new List<TaskSequence>(advertisers.Count);
            foreach (var advertiser in advertisers)
            {
                advertisements.AddRange(advertiser.AdvertiseTasksFor(agent));
            }

            return advertisements;
        }

        private float Atenuation(float value)
        {
            return 10f / value;
        }

        public void PerformFallbackTask()
        {
            _agent.PerformFallbackTask();
        }

    }
}
