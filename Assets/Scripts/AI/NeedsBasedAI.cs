using System;
using System.Collections.Generic;
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
            List<Advertisement[]> availableAds = GetAvailableAdvertisements(_agent);

            float[] advsScores = ScoreAdvertisements(availableAds);

            Advertisement[] selectedAdvs = SelectAdvertisement(advsScores, availableAds);
            
            EnqueueAdvertisements(selectedAdvs);

            return selectedAdvs.Length > 0;
        }

        private void EnqueueAdvertisements(Advertisement[] selectedAdvs)
        {
            foreach ((Task task, Reward reward) in selectedAdvs)
            {
                task.OnExecutionEnded += GiveReward(reward);

                _agent.TaskQueue.Enqueue(task);
            }
        }

        private float[] ScoreAdvertisements(List<Advertisement[]> advertisements)
        {
            float[] scores = new float[advertisements.Count];

            for (int i = 0; i < scores.Length; i++)
            {
                float total = 0;

                foreach ((Task task, Reward reward) in advertisements[i])
                {
                    float pastValue = _needsController.GetNeed(reward.NeedType).Amount;
                    float newValue = _needsController.GetResultingNeedAmount(reward);

                    float newScore = Atenuation(pastValue) - Atenuation(newValue);
                    total += newScore * _needsController.Profile.GetScoringMultiplier(reward.NeedType);
                }

                scores[i] = total;
            }

            return scores;
        }

        private Advertisement[] SelectAdvertisement(float[] scores, List<Advertisement[]> advertisements)
        {
            return _selectionStrategy.Select(_agent, scores, advertisements);
        }

        private static List<Advertisement[]> GetAvailableAdvertisements(Agent agent)
        {
            var advertisers = SimulationConfiguration.AdvertiserSystem.QueryAllAdvertisers();

            List<Advertisement[]> advertisements = new List<Advertisement[]>(advertisers.Count);
            foreach (var advertiser in advertisers)
            {
                advertisements.Add(advertiser.AdvertiseTasksFor(agent));
            }

            return advertisements;
        }

        private Action<bool> GiveReward(Reward reward)
        {
            return 
                (success) =>
                {
                    if (success) _needsController.ApplyReward(reward);
                };
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
