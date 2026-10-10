using System;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 3, 9, 2, 1, 7, 5, 8, 4, 6 };

            Console.Write("Исходный массив: ");
            Heap.Vyvod(arr);

            int[] heap = (int[])arr.Clone();
            Heap.Postroenie(heap);
            Console.Write("Убывающая куча: ");
            Heap.Vyvod(heap);

            int[] sorted = (int[])arr.Clone();
            Heap.Sortirovka(sorted);
            Console.Write("После сортировки кучей ");
            Heap.Vyvod(sorted);
        }
    }
}
