using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    class LinkedListNode<T>
    {
        // Значение, хранящееся в узле списка
        public T Value;
        // Следующий узел
        public LinkedListNode<T> Next;

        public LinkedListNode(T value, LinkedListNode<T> next = null)
        {
            Value = value;
            Next = next;
        }
    }
}
