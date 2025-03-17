using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeListForEach
{
    internal class Program
    {
        public class Node<T>
        {
            public Node<T> Next { get; set; }
            public T Data { get; set; }
            public Node(T data)
            {
                Next = null;
                this.Data = data;
            }
        }
        public class List<T>
        {
            private Node<T> head;
            private Node<T> tail;
            public T sum, min, max;
            public List()
            {
                head=tail = null;
            }
            public Node<T> Head()
            {
                return this.head;
            }
            public void Add(T t)
            {
                Node<T> node = new Node<T>(t);
                if (tail == null)
                {
                    tail = head = node;
                }
                else
                {
                    tail.Next = node;
                    tail = node;
                }
            }
            public void ForEach(Action<T> action)
            {
                Node<T> node = this.head;
                while (node != null)
                {
                    action(node.Data);
                    node=node.Next;
                }
            }
        }
        static void Main(string[] args)
        {
            List<int> list= new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.max=int.MinValue;
            list.min=int.MaxValue;
            list.sum = 0;
            list.ForEach(n => Console.WriteLine(n));
            list.ForEach(n => list.max = Math.Max(list.max, n));
            list.ForEach(n => list.min = Math.Min(list.min, n));
            list.ForEach(n => list.sum += n);
            Console.WriteLine(list.max);
            Console.WriteLine(list.min);
            Console.WriteLine(list.sum);
        }
    }
}
