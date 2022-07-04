using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ORCAS
{
    [System.Serializable]
    public class AgentUnityEvent : UnityEvent<Agent> { }

    public class AgentPicker : MonoBehaviour
    {
        public AgentUnityEvent OnAgentSelected;
        public Agent CurrentAgent
        {
            get => _currentAgent;
            set
            {
                _currentAgent = value;
                OnAgentSelected?.Invoke(_currentAgent);
            }
        }

        [SerializeField] private Camera _camera;
        
        [Header("Configurations")]
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _maxDistance = 100f;
        
        private RaycastHit[] _agentsRaycastHit = new RaycastHit[1];
        private Agent _currentAgent;

        public void OnSelect(InputValue input)
        {
            if (input.isPressed)
            {
                SelectAgentInView();
            }
            Debug.Log(input);
        }

        public void SelectAgentInView()
        {
            var position = Mouse.current.position.ReadValue();
            var ray = _camera.ScreenPointToRay(position);

            int numHits = Physics.RaycastNonAlloc(ray, _agentsRaycastHit, _maxDistance, _layerMask);

            if(numHits >= 1)
            {
                var agent = _agentsRaycastHit[0].rigidbody.GetComponent<Agent>();
                CurrentAgent = agent;
            }
        }
    }

}
