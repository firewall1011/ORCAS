using UnityEngine;

namespace ORCAS
{
    public class SimulationTimeController : MonoBehaviour
    {
        [SerializeField] private float _secondsPerSimulatedDay = 60f;
        private float _time = 0f;
        private const float _secondsPerRealDay = 1440;

        private float ScaleFactor => _secondsPerRealDay / _secondsPerSimulatedDay;

        public float GetSimulationTimeInSeconds() => _time * ScaleFactor;
        public float GetSimulationTimeInHours() => GetSimulationTimeInSeconds() / 60f;

        void Update()
        {
            _time += Time.deltaTime;
        }
    }
}
