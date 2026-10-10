using System;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PriorityQueue pq = new PriorityQueue();
            pq.Vstavka("съесть хлеб", 5);
            pq.Vstavka("съесть котлету", 4);
            pq.Vstavka("съесть сыр", 3);
            pq.Vstavka("съесть помидор", 2);

            Console.WriteLine("Самый приоритетный: " + pq.Posmotret());
            Console.WriteLine("Дальше по приоритету:");
            while (!pq.Pusto())
                Console.WriteLine("  " + pq.DostatMax());
        }
    }
}