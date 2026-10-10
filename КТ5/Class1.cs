using System;

namespace ConsoleApp4
{
    class Heap
    {
        public static void BuildHeap(int[] a)
        {
            int n = a.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
                SiftDown(a, i, n);
        }

        static void SiftDown(int[] a, int i, int n)
        {
            while (true)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                int largest = i;
                if (left < n && a[left] > a[largest]) largest = left;
                if (right < n && a[right] > a[largest]) largest = right;
                if (largest == i) break;
                Swap(a, i, largest);
                i = largest;
            }
        }

        public static void HeapSort(int[] a)
        {
            int n = a.Length;
            BuildHeap(a);
            for (int end = n - 1; end > 0; end--)
            {
                Swap(a, 0, end);
                SiftDown(a, 0, end);
            }
        }

        static void Swap(int[] a, int i, int j)
        {
            int t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        public static void Print(int[] a)
        {
            foreach (int x in a) Console.Write(x + " ");
            Console.WriteLine();
        }
    }
}