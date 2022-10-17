/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            // feel free to add your code
            var array = new List<int>();
            var task1 = new Thread(() => Task1(array));
            var task2 = new Thread(() => Task2(array));

            task1.Start();
            task2.Start();

            Console.ReadLine();
        }

        static AutoResetEvent waitHandle = new AutoResetEvent(false);
        static AutoResetEvent waitHandle2 = new AutoResetEvent(false);
        static bool _KeepWorking = true;

        static void Task1(List<int> array)
        {
            for (int i = 0; i < 10; i++)
            {
                array.Add(i);
                waitHandle.Set();
                waitHandle2.WaitOne();
            }
            _KeepWorking = false;
        }

        static void Task2(List<int> array)
        {
            while (_KeepWorking)
            {
                waitHandle.WaitOne();
                Console.Write("[");
                foreach (var item in array)
                {
                    Console.Write($"{item} ");
                }
                Console.Write("]");
                waitHandle2.Set();
            }
        }
    }
}
