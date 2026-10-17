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

        public bool Pusto()
        {
            return data.Count == 0;
        }

        public Zadacha Top()
        {
            if (data.Count == 0) return null;
            return data[0];
        }

        public void Dobavit(Zadacha z)
        {
            data.Add(z);
            Vverh(data.Count - 1);
        }

        public Zadacha DostatMax()
        {
            if (data.Count == 0) return null;
            while (data[0].Dirty)
            {
                data[0].Dirty = false;
                Vniz(0);
            }
            Zadacha max = data[0];
            int last = data.Count - 1;
            Obmen(0, last);
            data.RemoveAt(last);
            if (data.Count > 0) Vniz(0);
            return max;
        }

        public void Udalit(string name)
        {
            int idx = Naiti(name);
            if (idx < 0) return;
            int last = data.Count - 1;
            Obmen(idx, last);
            data.RemoveAt(last);
            if (idx < data.Count)
            {
                Vverh(idx);
                Vniz(idx);
            }
        }

        public void IzmenitD(string name, int delta)
        {
            int idx = Naiti(name);
            if (idx < 0) return;
            data[idx].D = data[idx].D + delta;
            Vverh(idx);
            Vniz(idx);
        }

        public void SmenaE(string name, int newE)
        {
            int idx = Naiti(name);
            if (idx < 0) return;
            data[idx].E = newE;
            data[idx].Dirty = true;
        }

        int Naiti(string name)
        {
            for (int i = 0; i < data.Count; i++)
                if (data[i].Name == name) return i;
            return -1;
        }

        bool Bolshe(Zadacha a, Zadacha b)
        {
            return a.P() > b.P();
        }

        void Vverh(int i)
        {
            while (i > 0)
            {
                int p = (i - 1) / 2;
                if (Bolshe(data[i], data[p]))
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
                int big = i;
                if (l < n && Bolshe(data[l], data[big])) big = l;
                if (r < n && Bolshe(data[r], data[big])) big = r;
                if (big == i) break;
                Obmen(i, big);
                i = big;
            }
        }

        void Obmen(int i, int j)
        {
            Zadacha t = data[i];
            data[i] = data[j];
            data[j] = t;
        }
    }
}