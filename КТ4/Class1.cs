using System;
using System.Collections.Generic;

namespace ConsoleApp4
{
    class WordCount
    {
        public string Word;
        public int Count;
        public WordCount(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }

    class MaxHeap
    {
        List<WordCount> data = new List<WordCount>();

        public int Size => data.Count;

        public void Insert(WordCount item)
        {
            data.Add(item);
            int i = data.Count - 1;
            while (i > 0 && data[i].Count > data[(i - 1) / 2].Count)
            {
                int parent = (i - 1) / 2;
                Swap(i, parent);
                i = parent;
            }
        }

        public WordCount ExtractMax()
        {
            WordCount max = data[0];
            int last = data.Count - 1;
            data[0] = data[last];
            data.RemoveAt(last);

            int i = 0;
            int n = data.Count;
            while (true)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                int largest = i;
                if (left < n && data[left].Count > data[largest].Count) largest = left;
                if (right < n && data[right].Count > data[largest].Count) largest = right;
                if (largest == i) break;
                Swap(i, largest);
                i = largest;
            }
            return max;
        }

        void Swap(int a, int b)
        {
            WordCount temp = data[a];
            data[a] = data[b];
            data[b] = temp;
        }
    }
}