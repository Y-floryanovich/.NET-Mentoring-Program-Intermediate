/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            // feel free to add your code
            //OptionOne();
            //OptionTwo();
            //OptionThree();
            OptionFour();

            Console.ReadLine();
        }

        static void OptionOne()
        {
            var taskA = Task.Run(() => DateTime.Today.DayOfWeek);
            var taskb = taskA.ContinueWith(antecedent => Console.WriteLine($"Today is {antecedent.Result}."));
            taskb.Wait();
        }

        static void OptionTwo()
        {
            var taskA = Task.Run(() => throw new Exception());
            var taskb = taskA.ContinueWith(antecedent => Console.WriteLine($"Today is"), TaskContinuationOptions.OnlyOnFaulted);
            taskb.Wait();
        }

        static void OptionThree()
        {
            Task taskA = Task.Run(() => { Console.WriteLine($"thread {Thread.CurrentThread.ManagedThreadId} qweqwe "); });
            var taskb = taskA.ContinueWith(antecedent => Console.WriteLine($" thread {Thread.CurrentThread.ManagedThreadId} Today is"), TaskContinuationOptions.ExecuteSynchronously);
            taskb.Wait();
        }

        static void OptionFour()
        {
            var source = new CancellationTokenSource();
            var token = source.Token;
            var taskA = Task.Factory.StartNew(() => { Console.WriteLine($"thread {Thread.CurrentThread.ManagedThreadId} qweqwe "); }, token);
            source.Cancel();
            var taskb = taskA.ContinueWith((antecedent) => Console.WriteLine($" thread {Thread.CurrentThread.ManagedThreadId} Today is"), TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning);
            taskb.Wait();
        }
    }
}
