using System;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 3, 9, 2, 1, 7, 5, 8, 4, 6 };

            Console.Write("Исходный массив: ");
            Heap.Print(arr);

            int[] heap = (int[])arr.Clone();
            Heap.BuildHeap(heap);
            Console.Write("Убывающая куча: ");
            Heap.Print(heap);

            int[] sorted = (int[])arr.Clone();
            Heap.HeapSort(sorted);
            Console.Write("После сортировки кучей ");
            Heap.Print(sorted);
        }
    }
}