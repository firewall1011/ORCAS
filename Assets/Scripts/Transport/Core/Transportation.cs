using System.Collections;
using UnityEngine;

namespace ORCAS.Transport
{
    public abstract class Transportation
    {
        public bool Succeded => _success;
        public Transform Start;
        public Transform Destination;
        public float Cost;

        protected bool _success = false;

        protected Transportation(Transform start, Transform destination, float cost)
        {
            Start = start;
            Destination = destination;
            Cost = cost;
        }

        public abstract IEnumerator Transport(Agent agent);
    }
}