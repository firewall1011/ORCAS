using UnityEngine;

namespace ORCAS.Transport
{
    internal class RegisterBusSystemToAgents : MonoBehaviour
    {
        [SerializeField] private BusTransportSystem _transportSystem;
        
        private void Awake()
        {
            var agents = FindObjectsOfType<Agent>();
            
            foreach(var agent in agents)
            {
                agent.TransportSystems.Add(_transportSystem);
            }
        }
    }
}