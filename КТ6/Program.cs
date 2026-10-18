using System;
using System.Collections.Generic;

namespace ConsoleApp5
{
    internal class Program
    {
        static KuchaD heap = new KuchaD();
        static List<Zadacha> sdelano = new List<Zadacha>();

        static string CvetImya(int c)
        {
            if (c == 0) return "красный";
            if (c == 1) return "синий";
            return "жёлтый";
        }

        static void Main()
        {
            heap.Dobavit(new Zadacha("Математика", 1, 9, 1));
            heap.Dobavit(new Zadacha("Философия", 2, 4, 5));
            heap.Dobavit(new Zadacha("Английский", 5, 6, 4));
            heap.Dobavit(new Zadacha("Лабораторка", 1, 8, 0));
            heap.Dobavit(new Zadacha("История", 3, 5, 2));

            Console.WriteLine("Куча (min по D):");
            heap.Pokazat();

            while (true)
            {
                Console.WriteLine("d - изменить D, e - изменить E (lazy), del - удалить");
                Console.WriteLine("plan - планирование, group - группировка по цвету, check - проверка, exit - выход");
                Console.Write("> ");
                string cmd = Console.ReadLine();
                if (cmd == "exit") break;

                if (cmd == "d")
                {
                    Console.Write("имя: ");
                    string name = Console.ReadLine();
                    Console.Write("новое D: ");
                    int d = int.Parse(Console.ReadLine());
                    heap.IzmenitD(name, d);
                    heap.Pokazat();
                }
                else if (cmd == "e")
                {
                    Console.Write("имя: ");
                    string name = Console.ReadLine();
                    Console.Write("новое E: ");
                    int e = int.Parse(Console.ReadLine());
                    heap.IzmenitE(name, e);
                    Console.WriteLine("E изменён, узел dirty, куча не перестроена:");
                    heap.Pokazat();
                }
                else if (cmd == "del")
                {
                    Console.Write("имя: ");
                    string name = Console.ReadLine();
                    heap.Udalit(name);
                    heap.Pokazat();
                }
                else if (cmd == "plan")
                {
                    Planirovanie();
                }
                else if (cmd == "group")
                {
                    Gruppirovka();
                }
                else if (cmd == "check")
                {
                    Console.WriteLine("куча по D корректна: " + heap.Proverka());
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("не понял команду");
                }
            }
        }

        static void Planirovanie()
        {
            KuchaP plan = new KuchaP();
            foreach (Zadacha z in heap.Vse())
                plan.Dobavit(new Zadacha(z.Name, z.D, z.I, z.E));

            if (plan.Pusto())
            {
                Console.WriteLine("куча пуста");
                Console.WriteLine();
                return;
            }

            Console.Write("сколько часов? ");
            int hours = int.Parse(Console.ReadLine());

            sdelano = new List<Zadacha>();
            int summa = 0;
            int vsego = hours;

            Console.WriteLine("Ресурс А (I>=7): 1 задача в час; Ресурс Б (I<7): до 2 задач в час");
            while (hours > 0 && !plan.Pusto())
            {
                Zadacha top = plan.DostatMax();
                int chas = vsego - hours + 1;
                if (top.I >= 7)
                {
                    Console.WriteLine("час " + chas + " [А]: " + top.Name + " (P=" + top.P() + ")");
                    summa += top.P();
                    sdelano.Add(top);
                }
                else
                {
                    Console.WriteLine("час " + chas + " [Б]: " + top.Name + " (P=" + top.P() + ")");
                    summa += top.P();
                    sdelano.Add(top);
                    if (!plan.Pusto() && plan.Verhushka().I < 7)
                    {
                        Zadacha vtoraya = plan.DostatMax();
                        Console.WriteLine("час " + chas + " [Б]: " + vtoraya.Name + " (P=" + vtoraya.P() + ")");
                        summa += vtoraya.P();
                        sdelano.Add(vtoraya);
                    }
                }
                hours--;
            }
            Console.WriteLine("суммарный приоритет: " + summa);
            Console.WriteLine();
        }

        static void Gruppirovka()
        {
            if (sdelano.Count == 0)
            {
                Console.WriteLine("сначала выполни plan");
                Console.WriteLine();
                return;
            }
            KuchaColor cvet = new KuchaColor();
            foreach (Zadacha z in sdelano)
                cvet.Dobavit(z);

            Console.WriteLine("порядок по цвету:");
            while (!cvet.Pusto())
            {
                Zadacha z = cvet.DostatMin();
                Console.WriteLine("  " + z.Name + " - " + CvetImya(z.Cvet()));
            }
            Console.WriteLine();
        }
    }
}