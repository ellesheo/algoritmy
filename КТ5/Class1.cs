using System;
using System.Collections.Generic;

namespace ConsoleApp5
{
    class Zadacha
    {
        public string Name;
        public int D;
        public int I;
        public int E;
        public bool Dirty;
        public Zadacha(string name, int d, int i, int e)
        {
            Name = name;
            D = d;
            I = i;
            E = e;
        }
        public int P()
        {
            return D * 10 + I - E;
        }
    }

    class Kucha
    {
        List<Zadacha> data = new List<Zadacha>();

        public void Dobavit(Zadacha z)
        {
            data.Add(z);
            Vverh(data.Count - 1);
        }

        public int Naiti(string name)
        {
            for (int i = 0; i < data.Count; i++)
                if (data[i].Name == name) return i;
            return -1;
        }

        public void IzmenitD(string name, int newD)
        {
            int i = Naiti(name);
            if (i < 0) return;
            data[i].D = newD;
            Vverh(i);
            Vniz(i);
        }

        public void IzmenitE(string name, int newE)
        {
            int i = Naiti(name);
            if (i < 0) return;
            data[i].E = newE;
            data[i].Dirty = true;
        }

        public void Udalit(string name)
        {
            int i = Naiti(name);
            if (i < 0) return;
            data[i] = data[data.Count - 1];
            data.RemoveAt(data.Count - 1);
            if (i < data.Count)
            {
                Vverh(i);
                Vniz(i);
            }
        }

        public void Pokazat()
        {
            for (int i = 0; i < data.Count; i++)
            {
                Zadacha z = data[i];
                string d = z.Dirty ? " (dirty)" : "";
                Console.WriteLine("[" + i + "] " + z.Name + " D=" + z.D + " P=" + z.P() + d);
            }
            Console.WriteLine();
        }

        void Vverh(int i)
        {
            while (i > 0)
            {
                int p = (i - 1) / 2;
                if (data[i].D < data[p].D)
                {
                    Obmen(i, p);
                    i = p;
                }
                else break;
            }
        }

        void Vniz(int i)
        {
            int n = data.Count;
            while (true)
            {
                int l = 2 * i + 1;
                int r = 2 * i + 2;
                int m = i;
                if (l < n && data[l].D < data[m].D) m = l;
                if (r < n && data[r].D < data[m].D) m = r;
                if (m == i) break;
                Obmen(i, m);
                i = m;
            }
        }

        void Obmen(int a, int b)
        {
            Zadacha t = data[a];
            data[a] = data[b];
            data[b] = t;
        }
    }
}