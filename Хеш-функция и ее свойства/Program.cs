using System;
using System.Text;

namespace ConsoleApp5
{
    class Program
    {
        const string rus = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        static string Zamena(string tekst)
        {
            StringBuilder rezultat = new StringBuilder();
            foreach (char simvol in tekst.ToUpper())
            {
                if (simvol >= 'A' && simvol <= 'Z')
                {
                    int nomer = simvol - 'A' + 1;
                    rezultat.Append(rus[nomer + 1]);
                }
            }
            return rezultat.ToString();
        }

        static int Heshirovat(string zamenennyj)
        {
            int hesh = 0;
            foreach (char simvol in zamenennyj)
            {
                int nomer = rus.IndexOf(simvol) + 1;
                hesh = (hesh * 15 + nomer) % 1000003;
            }
            return hesh;
        }

        static void PokazatRaschet(string tekst)
        {
            int hesh = 0;
            foreach (char simvol in tekst.ToUpper())
            {
                if (simvol >= 'A' && simvol <= 'Z')
                {
                    int nomer = simvol - 'A' + 1;
                    char novaya = rus[nomer + 1];
                    int nomerRus = nomer + 2;
                    hesh = (hesh * 15 + nomerRus) % 1000003;
                    Console.WriteLine($"{simvol} -> {novaya} ({nomerRus}), hesh = {hesh}");
                }
            }
            Console.WriteLine($"Итоговый хеш: {hesh}");
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - получить хеш текста");
                Console.WriteLine("2 - показать расчёт");
                Console.WriteLine("3 - сравнить два текста");
                Console.WriteLine("0 - выход");
                Console.Write("Выбор: ");
                string vybor = Console.ReadLine() ?? "";

                switch (vybor)
                {
                    case "1":
                        Console.Write("Текст на английском: ");
                        string tekst = Console.ReadLine() ?? "";
                        string zamenennyj = Zamena(tekst);
                        Console.WriteLine($"После замены: {zamenennyj}");
                        Console.WriteLine($"Хеш: {Heshirovat(zamenennyj)}");
                        break;

                    case "2":
                        Console.Write("Текст на английском: ");
                        string tekstRaschet = Console.ReadLine() ?? "";
                        PokazatRaschet(tekstRaschet);
                        break;

                    case "3":
                        Console.Write("Первый текст: ");
                        string tekst1 = Console.ReadLine() ?? "";
                        Console.Write("Второй текст: ");
                        string tekst2 = Console.ReadLine() ?? "";
                        string zamena1 = Zamena(tekst1);
                        string zamena2 = Zamena(tekst2);
                        int hesh1 = Heshirovat(zamena1);
                        int hesh2 = Heshirovat(zamena2);
                        Console.WriteLine($"Текст 1: {zamena1}, хеш {hesh1}");
                        Console.WriteLine($"Текст 2: {zamena2}, хеш {hesh2}");
                        Console.WriteLine(hesh1 == hesh2 ? "Хеши совпадают" : "Хеши отличаются");
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