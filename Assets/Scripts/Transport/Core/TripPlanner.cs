using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace ORCAS.Transport
{
    public class TripPlanner : MonoBehaviour
    {
        public class Node
        {
            public float Cost; // G
            public float DistanceToDestination; // H
            public float FinalCost => Cost + DistanceToDestination; // F
            public Node LastNode;
            public Transportation Transportation;
        }

        public Node Plan(Agent agent, Vector3 destination, DateTime startingTime)
        {
            List<Node> openNodes = new List<Node>();
            bool reachedDest = false;

            ExploreNode(agent, agent.transform.position, destination, null, openNodes);

            Node lastNode = null;
            while (reachedDest is false)
            {
                if (openNodes.Count > 100) break;

                var orderedOpenNodes = openNodes.OrderBy(node => node.FinalCost);

                Node openNode = orderedOpenNodes.FirstOrDefault();

                if (openNode is null)
                {
                    break;
                }

                openNodes.Remove(openNode);
                openNode.LastNode = lastNode;

                if (openNode.Transportation.Destination == destination)
                {
                    reachedDest = true;
                }
                else
                {
                    ExploreNode(agent, openNode.Transportation.Destination, destination, lastNode, openNodes);
                }
                lastNode = openNode;
            }

            return lastNode;
        }

        public float CalculateTripCost(Agent agent, Vector3 destination, DateTime startingTime)
        {
            Node path = Plan(agent, destination, startingTime);
            bool reachedDest = path != null;

            if (reachedDest)
            {
                return GetTripCost(path);
            }
            else
            {
                return Mathf.Infinity;
            }
        }

        private float GetTripCost(Node finalNode)
        {
            Node currentNode = finalNode;
            float sum = 0f;
            while(currentNode != null)
            {
                sum += currentNode.Cost;
                currentNode = currentNode.LastNode;
            }
            return sum;
        }

        private static void ExploreNode(Agent agent, Vector3 position, Vector3 destination, Node lastNode, List<Node> openNodes)
        {
            openNodes.AddRange(from system in agent.TransportSystems
                               let options = system.GetTransportationOptions(agent, position, destination)
                               from option in options
                               select CreateNodeFromTransportation(destination, option, lastNode));
        }

        private static Node GetWalkToDestinationNode(Agent agent, Vector3 position, Vector3 destination)
        {
            var transport = agent.WalkingSystem.GetTransportationOptions(agent, position, destination).ToArray()[0];

            var walkToDestNode = CreateNodeFromTransportation(destination, transport);
            
            return walkToDestNode;
        }
        
        private static Node CreateNodeFromTransportation(Vector3 destination, Transportation transportation, Node lastNode = null)
        {
            float lastCost = 0f;
            if (lastNode != null)
            {
                lastCost = lastNode.Cost;
            }

            return new Node()
            {
                Cost = transportation.TimeCost + lastCost,
                DistanceToDestination = Vector3.Distance(transportation.Destination, destination),
                LastNode = lastNode,
                Transportation = transportation
            };
        }
    }
}