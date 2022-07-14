using System;
using System.Collections;
using System.Collections.Generic;
using Priority_Queue;

namespace ORCAS.Collections
{
    public class PriorityQueue<TItem, TPriority> : IEnumerable<TItem>
	{
        public TItem First => _queue.First;
        public int Count => _queue.Count;
        
        private readonly SimplePriorityQueue<TItem, TPriority> _queue;

        public PriorityQueue() => _queue = new SimplePriorityQueue<TItem, TPriority>();
        public PriorityQueue(IComparer<TPriority> priorityComparer)
            => _queue = new SimplePriorityQueue<TItem, TPriority>(priorityComparer);

        public PriorityQueue(Comparison<TPriority> priorityComparer) 
            => _queue = new SimplePriorityQueue<TItem, TPriority>(priorityComparer);

        public PriorityQueue(IEqualityComparer<TItem> itemEquality)
            => _queue = new SimplePriorityQueue<TItem, TPriority>(itemEquality);

        public PriorityQueue(IComparer<TPriority> priorityComparer, IEqualityComparer<TItem> itemEquality)
            => _queue = new SimplePriorityQueue<TItem, TPriority>(priorityComparer, itemEquality);

        public PriorityQueue(Comparison<TPriority> priorityComparer, IEqualityComparer<TItem> itemEquality)
            => _queue = new SimplePriorityQueue<TItem, TPriority>(priorityComparer, itemEquality);

        public void Enqueue(TItem node, TPriority priority) => _queue.Enqueue(node, priority);
        public TItem Dequeue() => _queue.Dequeue();
        public void Clear() => _queue.Clear();

        public bool Contains(TItem node) => _queue.Contains(node);

        public void Remove(TItem node) => _queue.Remove(node);

        public void UpdatePriority(TItem node, TPriority priority) => _queue.UpdatePriority(node, priority);

        public bool IsEmpty() => _queue.Count == 0;

        public IEnumerator<TItem> GetEnumerator() => _queue.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _queue.GetEnumerator();
	}
}
