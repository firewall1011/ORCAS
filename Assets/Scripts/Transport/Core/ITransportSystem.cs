using System.Collections.Generic;
using UnityEngine;

namespace ORCAS.Transport
{
    public interface ITransportSystem
    {
        public IEnumerable<Transportation> GetTransportationOptions(Agent agent, Vector3 position, Vector3 destination);
    }
}