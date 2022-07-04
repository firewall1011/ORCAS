using System.Collections;
using UnityEngine;

namespace ORCAS.Transport
{
    public class Transportation
    {
        public Vector3 StartPosition;
        public Vector3 Destination;
        public float TimeCost;
        public IEnumerator Transport;
    }
}