using System;

namespace ConsoleApp5
{
    internal class Program
    {
        static void Main()
        {
            Kucha heap = new Kucha();
            heap.Dobavit(new Zadacha("Математика", 1, 9, 1));
            heap.Dobavit(new Zadacha("Философия", 2, 4, 5));
            heap.Dobavit(new Zadacha("Английский", 5, 6, 4));
            heap.Dobavit(new Zadacha("Лабораторка", 1, 8, 0));
            heap.Dobavit(new Zadacha("История", 3, 5, 2));

            Console.WriteLine("Куча (min по D):");
            heap.Pokazat();

            while (true)
            {
                Console.WriteLine("d - изменить D, e - изменить E (lazy), del - удалить, exit - выход");
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
                    Console.WriteLine("E изменён, узел помечен dirty, куча не перестроена:");
                    heap.Pokazat();
                }
                else if (cmd == "del")
                {
                    Console.Write("имя: ");
                    string name = Console.ReadLine();
                    heap.Udalit(name);
                    heap.Pokazat();
                }
                else
                {
                    Console.WriteLine("не понял команду");
                }
            }
        }
    }
}