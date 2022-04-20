using UnityEngine;
using UnityEngine.UI;

namespace ORCAS
{
    public class NeedBarUI : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI Label;
        [SerializeField] private Image _fillImage;

        public void SetLabel(string label)
        {
            Label.text = label;
        }

        public void UpdateFillAmount(float amount)
        {
            _fillImage.fillAmount = amount;
            _fillImage.color = Color.Lerp(Color.red, Color.green, amount);
        }
    }
}
