using UnityEngine;

namespace ORCAS
{
    public class SetNeedsControllerToFirstFound : MonoBehaviour
    {
        [SerializeField] private NeedsVisualizer _needsVisualizer;

        private void Start()
        {
            _needsVisualizer.SetNeedsController(FindObjectOfType<NeedsController>());
        }
    }
}
