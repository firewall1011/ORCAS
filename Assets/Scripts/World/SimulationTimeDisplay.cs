using UnityEngine;

namespace ORCAS
{
    public class SimulationTimeDisplay : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _text;
        [SerializeField] private SimulationTimeController _timeController;

        private void Update()
        {
            string time = (_timeController.GetSimulationTimeInHours() % 24).ToString("F2");
            _text.SetText(time);
        }
    }
}
