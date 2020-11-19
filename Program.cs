using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8
{
    class NotNumberException : Exception
    {
        public NotNumberException(string msg) : base(msg)
        { }
    }
    interface IOperations<T>
    {
        void Add(T value);
        bool Delete(T value);
        void View();
    }
    public class MyType
    { dynamic X;
        public dynamic x
        { get { return X; }
            set
            { var test = value;
                if (test is string || test is char)
                { throw new NotNumberException("Число, а не символы!"); }
                else
                { X = value; }
            }
        }
    }
    public class Node<T>
    {
        public Node(T data)
        { Data = data; }
        public T Data { get; set; }
        public Node<T> Next { get; set; }
    }
    public class List<T> : IEnumerable<T>, IOperations<T> where T : struct
    {
        Node<T> head;
        Node<T> tail;
        int count;
        IEnumerator IEnumerable.GetEnumerator()
        { return ((IEnumerable)this).GetEnumerator(); }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        { Node<T> current = head;
            while (current != null)
            { yield return current.Data;
                current = current.Next;
            }
        }
        public void Add(T data)
        { Node<T> node = new Node<T>(data);
            if (head == null)
            { head = node; }
            else
            { tail.Next = node; }
            tail = node;
            count++;
        }
        public bool Delete(T data)
        { Node<T> current = head;
            Node<T> previous = null;
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                        if (current.Next == null)
                        { tail = previous; }
                    }
                    else
                    {
                        head = head.Next;

                        if (head == null)
                        { tail = null; }
                    }
                    count--;
                    return true;
                }
                previous = current;
                current = current.Next;
            }
            return false;
        }
        public void View()
        {
            List<int> v_List = new List<int>();
            v_List.Add(6);
            v_List.Add(0);
            v_List.Add(4);
            foreach (var item in v_List)
            {
                Console.WriteLine(item);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<char> List_3 = new List<char>();
                List_3.Add('c');
                List_3.Add('!');
                List_3.Add('y');

                foreach (var item in List_3)
                { Console.WriteLine(item); }

                List<int> IntList = new List<int>();
                List<int> IntList_2 = new List<int>();

                for (int i = 0; i < 5; i++)
                {
                    MyType temp = new MyType();
                    temp.x = i;
                    IntList.Add(temp.x);
                    IntList_2.Add(temp.x);
                    if (temp.x == 2) { IntList_2.Delete(temp.x); }

                }
                Console.WriteLine(IntList != IntList_2);
                Console.WriteLine("Элементы IntList: ");
                foreach (var item in IntList)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("Элементы IntList_2: ");
                foreach (var item in IntList_2)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("The end");
            }
        }
        }
    } 
