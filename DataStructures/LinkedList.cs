using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    class LinkedList<T> where T : IComparable
    {
        private LinkedListNode<T> _head;
        private LinkedListNode<T> _tail;

        private int _count;

        public int Count
        {
            get { return _count; }
        }

        public bool Empty 
        {
            get { return _count == 0; }
        }

        public LinkedListNode<T> First
        {
            get { return _head; }
        }

        public LinkedList()
        {

        }

        public void AddAfter(LinkedListNode<T> node, T value)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(value,node.Next);
            node.Next = newNode;

            _count++;
        }

        public void RemoveAfter(LinkedListNode<T> node)
        {
            LinkedListNode<T> removedNode = node.Next;
            removedNode.Next = null;

            if (removedNode == _tail)
            {
                _tail = node;
            }

            _count--;
        }

        public void Remove(LinkedListNode<T> node)
        {
            if (node != _head)
            {
                var prevNode = FindPrevNode(node);
                prevNode.Next = node.Next;
                node = null;
            }
            else
            {
                _head = node.Next;
                node = null;
            }
           _count--;
        }

        public LinkedListNode<T> FindPrevNode(LinkedListNode<T> node)
        {
            LinkedListNode<T> prevNode = null;
            LinkedListNode<T> currentNode = _head;

            while (currentNode != node)
            {
                prevNode = currentNode;
                currentNode = currentNode.Next;
            }

            return prevNode;
        }

        public void Add(T value)
        {
            if (_head == null)
            {
                _head = _tail = new LinkedListNode<T>(value, null);
                _count++;
            }
            else
            {
                AddAfter(_tail, value);
                _tail = _tail.Next;
            }
        }

        public LinkedListNode<T> Find(T value)
        {
            LinkedListNode<T> ptr = _head;
            while (ptr != null)
            {
                if (ptr.Value.CompareTo(value) == 0)
                {
                    return ptr;
                }
                ptr = ptr.Next;
            }
            return null;
        }

    }
}
