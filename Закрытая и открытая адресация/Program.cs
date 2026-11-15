using System;
using System.Collections.Generic;

namespace ConsoleApp5
{
    class Program
    {
        const int razmer = 10;
        const int kolichestvo = 6;

        static int[] PoluchitId()
        {
            int[] idKlientov = new int[kolichestvo];
            Console.WriteLine($"Введите ID {kolichestvo} клиентов");

            for (int i = 0; i < kolichestvo; i++)
            {
                while (true)
                {
                    Console.Write($"ID клиента {i + 1}: ");
                    string vvod = Console.ReadLine() ?? "";

                    if (int.TryParse(vvod, out int id) && id >= 0)
                    {
                        idKlientov[i] = id;
                        break;
                    }

                    Console.WriteLine("Введите целое число от 0");
                }
            }

            return idKlientov;
        }

        static void PokazatYacheyki(int[] idKlientov)
        {
            Console.WriteLine("Исходные ячейки");
            foreach (int id in idKlientov)
            {
                Console.WriteLine($"Клиент {id} -> ячейка {id % razmer}");
            }
        }

        static int[] SozdatPustoy()
        {
            int[] massiv = new int[razmer];
            Array.Fill(massiv, -1);
            return massiv;
        }

        static void PokazatMassiv(int[] massiv, string nazvanie)
        {
            Console.WriteLine("Результат");
            for (int i = 0; i < massiv.Length; i++)
            {
                if (massiv[i] == -1)
                {
                    Console.WriteLine($"{nazvanie} {i}: свободно");
                }
                else
                {
                    Console.WriteLine($"{nazvanie} {i}: клиент {massiv[i]}");
                }
            }
        }

        static void Parkovka()
        {
            int[] idKlientov = PoluchitId();
            PokazatYacheyki(idKlientov);

            Console.WriteLine("Парковка: открытая адресация, линейное пробирование");
            int[] parkovka = SozdatPustoy();

            foreach (int id in idKlientov)
            {
                int mesto = id % razmer;
                bool razmeshen = false;

                for (int popytka = 0; popytka < razmer; popytka++)
                {
                    if (parkovka[mesto] == -1)
                    {
                        parkovka[mesto] = id;
                        razmeshen = true;
                        break;
                    }

                    mesto = (mesto + 1) % razmer;
                }

                if (!razmeshen)
                {
                    Console.WriteLine($"Парковка переполнена, клиент {id} не припарковался");
                }
            }

            PokazatMassiv(parkovka, "Место");
        }

        static void Garderob()
        {
            int[] idKlientov = PoluchitId();
            PokazatYacheyki(idKlientov);

            Console.WriteLine("Гардероб: закрытая адресация, цепочки");
            List<int>[] kryuchki = new List<int>[razmer];

            for (int i = 0; i < razmer; i++)
            {
                kryuchki[i] = new List<int>();
            }

            foreach (int id in idKlientov)
            {
                kryuchki[id % razmer].Add(id);
            }

            Console.WriteLine("Результат");
            for (int i = 0; i < razmer; i++)
            {
                if (kryuchki[i].Count == 0)
                {
                    Console.WriteLine($"Крючок {i}: свободен");
                }
                else
                {
                    Console.WriteLine($"Крючок {i}: " + string.Join(" -> ", kryuchki[i]));
                }
            }
        }

        static bool Prostoe(int chislo)
        {
            if (chislo < 2)
            {
                return false;
            }

            for (int i = 2; i * i <= chislo; i++)
            {
                if (chislo % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        static void Shkafchiki()
        {
            int[] idKlientov = PoluchitId();
            PokazatYacheyki(idKlientov);

            int shag;
            while (true)
            {
                Console.Write("Простое число P меньше 10: ");
                string vvod = Console.ReadLine() ?? "";

                if (int.TryParse(vvod, out int znachenie) && znachenie < razmer && Prostoe(znachenie))
                {
                    shag = znachenie;
                    break;
                }

                Console.WriteLine("Введите простое число меньше 10");
            }

            Console.WriteLine("Шкафчики: открытая адресация, двойное хеширование");
            int[] shkafchiki = SozdatPustoy();

            foreach (int id in idKlientov)
            {
                int nachalo = id % razmer;
                bool razmeshen = false;

                for (int popytka = 0; popytka < razmer; popytka++)
                {
                    int mesto = (nachalo + popytka * shag) % razmer;

                    if (shkafchiki[mesto] == -1)
                    {
                        shkafchiki[mesto] = id;
                        razmeshen = true;
                        break;
                    }
                }

                if (!razmeshen)
                {
                    Console.WriteLine($"Шкафчик для клиента {id} не найден");
                }
            }

            PokazatMassiv(shkafchiki, "Шкафчик");
        }

        static void Main()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - парковка");
                Console.WriteLine("2 - гардероб");
                Console.WriteLine("3 - шкафчики");
                Console.WriteLine("0 - выход");
                Console.Write("Выбор: ");
                string vybor = Console.ReadLine() ?? "";

                switch (vybor)
                {
                    case "1":
                        Parkovka();
                        break;

                    case "2":
                        Garderob();
                        break;

                    case "3":
                        Shkafchiki();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
        }
    }
}