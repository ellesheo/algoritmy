using System;
using System.Collections.Generic;

namespace ConsoleApp5
{
    class Driver
    {
        public string Id;
        public double Lat;
        public double Lon;
        public Driver(string id, double lat, double lon)
        {
            Id = id;
            Lat = lat;
            Lon = lon;
        }
    }

    class Order
    {
        public string Id;
        public double Lat;
        public double Lon;
        public double Price;
        public Order(string id, double lat, double lon, double price)
        {
            Id = id;
            Lat = lat;
            Lon = lon;
            Price = price;
        }
    }

    class Uzel
    {
        public string Id;
        public double L;
        public long Ts;
        public Uzel(string id, double l, long ts)
        {
            Id = id;
            L = l;
            Ts = ts;
        }
    }

    class SmartDispatcher
    {
        double cLat;
        double cLon;
        double speed;
        double a;
        double b;
        long counter;

        List<Uzel> heap = new List<Uzel>();
        Dictionary<string, int> pos = new Dictionary<string, int>();
        HashSet<string> otkazy = new HashSet<string>();

        public SmartDispatcher(double cLat, double cLon, double speed, double a, double b)
        {
            this.cLat = cLat;
            this.cLon = cLon;
            this.speed = speed;
            this.a = a;
            this.b = b;
        }

        double Dist(double x1, double y1, double x2, double y2)
        {
            double dx = x1 - x2;
            double dy = y1 - y2;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        double Koef(double lat, double lon, Order o)
        {
            double t = Dist(lat, lon, o.Lat, o.Lon) / speed;
            double dc = Dist(lat, lon, cLat, cLon);
            return a * t + b * dc / o.Price;
        }

        public void Dobavit(Driver dr, Order o)
        {
            double l = Koef(dr.Lat, dr.Lon, o);
            Uzel node = new Uzel(dr.Id, l, counter);
            counter++;
            heap.Add(node);
            pos[node.Id] = heap.Count - 1;
            Vverh(heap.Count - 1);
        }

        public void Obnovit(string id, double lat, double lon, Order o)
        {
            double nl = Koef(lat, lon, o);
            if (!pos.ContainsKey(id)) return;
            int i = pos[id];
            if (nl < heap[i].L)
            {
                heap[i].L = nl;
                Vverh(i);
            }
        }

        public void Otmena(string id)
        {
            otkazy.Add(id);
        }

        public string Naznachit()
        {
            while (true)
            {
                Uzel min = Dostat();
                if (min == null) return null;
                if (otkazy.Contains(min.Id)) continue;
                return min.Id;
            }
        }

        public string Top()
        {
            if (heap.Count == 0) return "нет";
            return heap[0].Id;
        }

        public void Sbros()
        {
            heap.Clear();
            pos.Clear();
            otkazy.Clear();
        }

        Uzel Dostat()
        {
            if (heap.Count == 0) return null;
            Uzel min = heap[0];
            int last = heap.Count - 1;
            Obmen(0, last);
            heap.RemoveAt(last);
            pos.Remove(min.Id);
            if (heap.Count > 0) Vniz(0);
            return min;
        }

        bool Menshe(Uzel x, Uzel y)
        {
            if (x.L != y.L) return x.L < y.L;
            return x.Ts < y.Ts;
        }

        void Vverh(int i)
        {
            while (i > 0)
            {
                int p = (i - 1) / 2;
                if (Menshe(heap[i], heap[p]))
                {
                    Obmen(i, p);
                    i = p;
                }
                else break;
            }
        }

        void Vniz(int i)
        {
            int n = heap.Count;
            while (true)
            {
                int l = 2 * i + 1;
                int r = 2 * i + 2;
                int m = i;
                if (l < n && Menshe(heap[l], heap[m])) m = l;
                if (r < n && Menshe(heap[r], heap[m])) m = r;
                if (m == i) break;
                Obmen(i, m);
                i = m;
            }
        }

        void Obmen(int i, int j)
        {
            Uzel t = heap[i];
            heap[i] = heap[j];
            heap[j] = t;
            pos[heap[i].Id] = i;
            pos[heap[j].Id] = j;
        }
    }
}