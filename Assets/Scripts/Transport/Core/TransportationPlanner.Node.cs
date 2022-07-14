using System;
using System.Collections.Generic;
using UnityEngine;

namespace ORCAS.Transport
{
    public partial class PathCalculator
    {
        public struct Node
        {
            public readonly Transform Current;
            public readonly Vector3 Position;

            public Node(Transform current)
            {
                Current = current;
                Position = current.position;
            }

            public override bool Equals(object obj)
            {
                return obj is Node node &&
                       EqualityComparer<Transform>.Default.Equals(Current, node.Current);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Current);
            }

            public static bool operator ==(Node left, Node right)
            {
                return left.Position == right.Position;
            }

            public static bool operator !=(Node left, Node right)
            {
                return left.Position != right.Position;
            }

            public override string ToString()
            {
                return $"{Current.name} in position {Position}";
            }
        }
    }
}