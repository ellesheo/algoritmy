using System;
using System.Collections.Generic;

namespace ConsoleApp4
{
    class PriorityQueue
    {
        class Element
        {
            public string Value;
            public int Priority;
            public int Order;
            public Element(string value, int priority, int order)
            {
                Value = value;
                Priority = priority;
                Order = order;
            }
        }

        List<Element> data = new List<Element>();
        int counter = 0;

        public bool Pusto()
        {
            return data.Count == 0;
        }

        bool Prioritetnee(Element a, Element b)
        {
            if (a.Priority != b.Priority) return a.Priority > b.Priority;
            return a.Order < b.Order;
        }

        public void Vstavka(string value, int priority)
        {
            data.Add(new Element(value, priority, counter++));
            int i = data.Count - 1;
            while (i > 0 && Prioritetnee(data[i], data[(i - 1) / 2]))
            {
                Obmen(i, (i - 1) / 2);
                i = (i - 1) / 2;
            }
        }

        public string DostatMax()
        {
            Element max = data[0];
            int last = data.Count - 1;
            data[0] = data[last];
            data.RemoveAt(last);

            int i = 0;
            int n = data.Count;
            while (true)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                int best = i;
                if (left < n && Prioritetnee(data[left], data[best])) best = left;
                if (right < n && Prioritetnee(data[right], data[best])) best = right;
                if (best == i) break;
                Obmen(i, best);
                i = best;
            }
            return max.Value;
        }

        public string Posmotret()
        {
            return data[0].Value;
        }

        void Obmen(int a, int b)
        {
            Element temp = data[a];
            data[a] = data[b];
            data[b] = temp;
        }
    }
}