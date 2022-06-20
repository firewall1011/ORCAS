using UnityEngine;

namespace ORCAS.Utils
{
    public class TimeManipulator : MonoBehaviour
    {
        [SerializeField] private float deltaAmount = 0.1f;
        
        [SerializeField] private float _curTimeScale = 1f;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Minus))
            {
                AddTimeScale(-deltaAmount);
            }
            if (Input.GetKeyDown(KeyCode.Equals))
            {
                AddTimeScale(deltaAmount);
            }
            
            
            Time.timeScale = _curTimeScale;
        }

        public void AddTimeScale(float amount)
        {
            _curTimeScale += amount;
            _curTimeScale = Mathf.Clamp(_curTimeScale, 0f, 10f);
        }
    }
}
