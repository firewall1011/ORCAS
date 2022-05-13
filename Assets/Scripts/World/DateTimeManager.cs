using System;
using UnityEngine;

namespace ORCAS
{
    public class DateTimeManager : MonoBehaviour
    {
        public int Hour => _dateTime.Hour;
        public int Minute => _dateTime.Minute;
        public DateTime DateTime => _dateTime;

        [SerializeField, Min(0.01f), Tooltip("How many real life seconds are there in a simulation minute")] 
        private float _secondsPerMinute = 0.5f;
        private DateTime _dateTime;
        private float _timer = 0f;

        private void Start()
        {
            _dateTime = DateTime.UtcNow;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _secondsPerMinute)
            {
                _dateTime = _dateTime.AddMinutes(1);
                _timer -= _secondsPerMinute;
            }    
        }
    }
}
