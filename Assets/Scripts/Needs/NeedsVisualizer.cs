using System.Collections.Generic;
using UnityEngine;

namespace ORCAS
{
    public class NeedsVisualizer : MonoBehaviour
    {
        private NeedsController _controller;

        [SerializeField] private NeedBarUI _needBarPrefab;
        [SerializeField] private List<NeedBarUI> _needBars;

        public void SetNeedsController(NeedsController controller)
        {
            _controller = controller;
            InitializeVisuals();
        }

        private void InitializeVisuals()
        {
            foreach(var bar in _needBars)
            {
                Destroy(bar.gameObject);
            }
            _needBars.Clear();

            foreach(var need in _controller.CurrentNeeds)
            {
                var needBar = Instantiate(_needBarPrefab, transform);

                needBar.SetLabel(need.Type.name);
                needBar.UpdateFillAmount(need.Amount / _controller.Profile.MaximumNeedAmount);

                _needBars.Add(needBar);
            }
        }

        private void Update()
        {
            if (_controller is null)
                return;

            int i = 0;
            foreach(var need in _controller.CurrentNeeds)
            {
                if (i >= _needBars.Count) return;

                _needBars[i].UpdateFillAmount(need.Amount / _controller.Profile.MaximumNeedAmount);
                i++;
            }
        }
    }
}
