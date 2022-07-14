using System;
using System.Collections.Generic;
using ORCAS.Collections;
using UnityEngine;

namespace ORCAS.Transport
{
    public partial class PathCalculator
    {
        private readonly List<ITransportSystem> _transportSystems;
        private readonly Agent _agent;
        private readonly Transform _destination;

        public PathCalculator(Agent agent, Transform destination)
        {
            _transportSystems = agent.TransportSystems;
            _agent = agent;
            _destination = destination;
        }

        public IEnumerable<Transportation> CalculatePath()
        {
            return A_Star(new(_agent.transform), new(_destination), LinearDistance);
            float LinearDistance(Node node) => Vector3.Distance(node.Current.position, _destination.position);
        }

        public float CalculateCost(IEnumerable<Transportation> transportations)
        {
            float totalCost = 0f;
            
            foreach(var transport in transportations)
            {
                totalCost += transport.Cost;
            }

            return totalCost;
        }

        private IEnumerable<Transportation> A_Star(Node start, Node destination, Func<Node, float> heuristicFunc)
        {
            var openSet = new PriorityQueue<Node, float>();

            Dictionary<Node, Node> cameFromDict = new();
            Dictionary<Node, float> gScoreDict = new();
            Dictionary<Node, Transportation> transportations = new();
            gScoreDict[start] = 0f;

            openSet.Enqueue(start, heuristicFunc(start));

            while ( !openSet.IsEmpty() )
            {
                var current = openSet.First;

                if (current == destination)
                {
                    return ReconstructPath(cameFromDict, transportations, current);
                }

                openSet.Remove(current);
                List<Transportation> neighbors = GetNeighbors(current);
                foreach(var neighbor in neighbors)
                {
                    var neighborNode = new Node(neighbor.Destination);
                    float tentative_gScore = GetGScore(current) + neighbor.Cost;
                    
                    if (tentative_gScore < GetGScore(neighborNode))
                    {
                        cameFromDict[neighborNode] = current;
                        gScoreDict[neighborNode] = tentative_gScore;
                        float fScore = tentative_gScore + heuristicFunc(neighborNode);

                        if (!openSet.Contains(neighborNode))
                        {
                            openSet.Enqueue(neighborNode, fScore);
                        }
                        else
                        {
                            openSet.UpdatePriority(neighborNode, fScore);
                        }
                        transportations[neighborNode] = neighbor;
                    }
                }
            }

            return new Transportation[0];

            float GetGScore(Node node)
            {
                if (gScoreDict.ContainsKey(node))
                {
                    return gScoreDict[node];
                }
                else return float.PositiveInfinity;
            }
        }

        private List<Transportation> GetNeighbors(Node current)
        {
            List<Transportation> neighbors = new List<Transportation>();
            foreach (var system in _transportSystems)
            {
                neighbors.AddRange(system.GetTransportationOptions(_agent, current.Current, _destination));
            }
            return neighbors;
        }

        private IEnumerable<Transportation> ReconstructPath(Dictionary<Node, Node> cameFrom, Dictionary<Node, Transportation> transportations, Node current)
        {
            Stack<Transportation> path = new();

            while (cameFrom.ContainsKey(current))
            {
                if (transportations.TryGetValue(current, out var transportation))
                {
                    path.Push(transportation);
                }
                
                current = cameFrom[current];
            }

            return path;
        }
    }
}