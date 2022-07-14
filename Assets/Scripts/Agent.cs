using System.Collections.Generic;
using UnityEngine;
using ORCAS.Tasks;
using ORCAS.Advertisement;
using ORCAS.Transport;

namespace ORCAS
{
    [RequireComponent(typeof(TaskExecutioner))]
    public class Agent : MonoBehaviour
    {
        public TaskExecutioner TaskExecutioner { get; private set; }
        public TripPlanner TripPlanner { get; private set; }
        public WalkingTransportSystem WalkingSystem { get; private set; }
        public List<ITransportSystem> TransportSystems { get; private set; } = new List<ITransportSystem>();
        
        [field: SerializeField] public AgentProfile Profile { get; private set; }
        [SerializeField] private TaskHolder _fallbackTaskHolder;
        
        private IAgentAI _ai;

        private void Awake()
        {
            _ai = GetComponent<IAgentAI>();
            TaskExecutioner = GetComponent<TaskExecutioner>();
            TripPlanner = GetComponent<TripPlanner>();
            WalkingSystem = GetComponent<WalkingTransportSystem>();
            TransportSystems.Add(WalkingSystem);
        }

        private void Update()
        {
            if (TaskExecutioner.IsPerformingTask())
            {
                return;
            }

            if (TaskExecutioner.TaskQueue.Count == 0)
            {
                if (!_ai.SelectTaskSequence(this, GetAvailableAdvertisements()))
                {
                    PerformFallbackTask();
                }
            }
            else
            {
                TaskExecutioner.PerformTaskInQueue();
            }
        }

        private void PerformFallbackTask()
        {
            TaskExecutioner.TryPerformTask(_fallbackTaskHolder.Task);
        }

        private List<TaskSequence> GetAvailableAdvertisements()
        {
            var advertisers = SimulationConfiguration.AdvertiserSystem.QueryAllAdvertisers();

            List<TaskSequence> advertisements = new List<TaskSequence>(advertisers.Count);
            foreach (var advertiser in advertisers)
            {
                advertisements.AddRange(advertiser.AdvertiseTasksFor(this));
            }

            return advertisements;
        }
    }
}
