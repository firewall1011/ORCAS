using UnityEngine;

namespace ORCAS
{
    public class DateTimeDisplay : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI displayMesh;

        private DateTimeManager _timeManager;

        private void Awake()
        {
            _timeManager = FindObjectOfType<DateTimeManager>();
        }

        private void Update()
        {
            string text = _timeManager.DateTime.ToShortTimeString();

            displayMesh.SetText(text);
        }
    }
}
