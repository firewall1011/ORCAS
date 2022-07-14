using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ORCAS.Transport
{
    public class BusStop : MonoBehaviour
    {
        [System.Serializable]
        public struct BusConnection
        {
            public BusStop Destination;
            public float TravelTime;
        }

        [field: SerializeField] public BusConnection[] Connections { get; private set; }
        [field: SerializeField] public Transform DropTarget { get; private set; }

        [SerializeField, Min(1f)] private float _busSpeed = 42f;

        public IEnumerable<Transportation> GetBusOptions()
        {
            var transportations = new List<Transportation>();
            
            foreach(var connection in Connections)
            {
                var transportation = new BusTransportation(transform, connection.Destination.DropTarget, connection.TravelTime, _busSpeed);
                transportations.Add(transportation);
            }

            return transportations;
        }
    }
}