using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ORCAS
{
    public class SerializedList<T> : ScriptableObject, IEnumerable<T>
    {
        public List<T> List;

        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }

        public static implicit operator List<T>(SerializedList<T> serializedList)
        {
            return serializedList.List;
        }
    }
}
