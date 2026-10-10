using System;

namespace ConsoleApp4
{
    class Heap
    {
        public static void Postroenie(int[] a)
        {
            int n = a.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
                Pogruzit(a, i, n);
        }

        static void Pogruzit(int[] a, int i, int n)
        {
            while (true)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                int largest = i;
                if (left < n && a[left] > a[largest]) largest = left;
                if (right < n && a[right] > a[largest]) largest = right;
                if (largest == i) break;
                Obmen(a, i, largest);
                i = largest;
            }
        }

        public static void Sortirovka(int[] a)
        {
            int n = a.Length;
            Postroenie(a);
            for (int end = n - 1; end > 0; end--)
            {
                Obmen(a, 0, end);
                Pogruzit(a, 0, end);
            }
        }

        static void Obmen(int[] a, int i, int j)
        {
            int t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        public static void Vyvod(int[] a)
        {
            foreach (int x in a) Console.Write(x + " ");
            Console.WriteLine();
        }
    }
}
