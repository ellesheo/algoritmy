using System;
using System.Collections.Generic;

namespace ConsoleApp5
{
    internal class Program
    {
        static string Opis(Zadacha z)
        {
            if (z == null) return "нет";
            return z.Name + " (P=" + z.P() + ")";
        }

        static void Main()
        {
            Console.WriteLine("Часть 1: модификация кучи");
            Kucha k = new Kucha();
            k.Dobavit(new Zadacha("Матан", 1, 9, 1));
            k.Dobavit(new Zadacha("Философия", 2, 4, 5));
            k.Dobavit(new Zadacha("Английский", 5, 6, 4));
            k.Dobavit(new Zadacha("Лабораторная", 1, 8, 0));
            k.Dobavit(new Zadacha("История", 3, 5, 2));
            Console.WriteLine("вершина: " + Opis(k.Top()));

            k.IzmenitD("Матан", -1);
            Console.WriteLine("прокрастинация (Матан D-1): вершина " + Opis(k.Top()));

            k.Udalit("Матан");
            Console.WriteLine("автомат (убрали Матан): вершина " + Opis(k.Top()));

            k.SmenaE("Английский", 5);
            Console.WriteLine("настроение (Английский E=5, lazy): вершина " + Opis(k.Top()));

            Console.WriteLine("порядок извлечения:");
            while (!k.Pusto())
                Console.WriteLine("  " + Opis(k.DostatMax()));

            Console.WriteLine();
            Console.WriteLine("Часть 2: распределение по ресурсам");
            Kucha k2 = new Kucha();
            k2.Dobavit(new Zadacha("Матан", 1, 9, 1));
            k2.Dobavit(new Zadacha("Философия", 2, 4, 5));
            k2.Dobavit(new Zadacha("Английский", 5, 6, 4));
            k2.Dobavit(new Zadacha("Лабораторная", 1, 8, 0));
            k2.Dobavit(new Zadacha("История", 3, 5, 2));

            List<string> a = new List<string>();
            List<string> b = new List<string>();
            while (!k2.Pusto())
            {
                Zadacha z = k2.DostatMax();
                if (z.I >= 7) a.Add(z.Name);
                else b.Add(z.Name);
            }
            Console.WriteLine("Ресурс А (кофеин, I>=7): " + string.Join(", ", a));
            Console.WriteLine("Ресурс Б (энергетик, I<7): " + string.Join(", ", b));
        }
    }
}