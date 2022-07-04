using UnityEngine;

namespace ORCAS
{
    public class NeedsVisualizerHelper : MonoBehaviour
    {
        private NeedsVisualizer _visualizer;

        private void Awake()
        {
            _visualizer = GetComponent<NeedsVisualizer>();
        }

        public void ViewAgentNeeds(Agent agent)
        {
            _visualizer.SetNeedsController(agent.GetComponent<NeedsController>());
        }
    }
}
