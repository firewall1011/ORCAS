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
        
        public IEnumerable<Transportation> GetBusOptions()
        {
            var transportations = new List<Transportation>();
            
            foreach(var connection in Connections)
            {
                transportations.Add(new Transportation()
                {
                    StartPosition = transform.position,
                    Destination = connection.Destination.transform.position,
                    TimeCost = connection.TravelTime,
                    Transport = Transport(connection)
                });
            }

            return transportations;
        }

        public IEnumerator Transport(BusConnection connection)
        {
            Debug.Log($"Taking bus from {name} to {connection.Destination.name}");
            yield return new WaitForSeconds(connection.TravelTime);
            Debug.Log("Arrived at " + connection.Destination.name);
        }
    }
}