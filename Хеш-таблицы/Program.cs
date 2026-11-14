using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace ConsoleApp5
{
    public class Tovar
    {
        public string Artikul { get; set; }
        public string Nazvanie { get; set; }
        public decimal Cena { get; set; }
        public int Kolichestvo { get; set; }

        public Tovar(string artikul, string nazvanie, decimal cena, int kolichestvo)
        {
            Artikul = artikul;
            Nazvanie = nazvanie;
            Cena = cena;
            Kolichestvo = kolichestvo;
        }
    }

    public class OshibkaSklada : Exception
    {
        public OshibkaSklada(string soobshenie) : base(soobshenie)
        {
        }
    }

    class Program
    {
        static Dictionary<string, Tovar> sklad = new Dictionary<string, Tovar>();

        static void Zagruzit()
        {
            string[] stroki = File.ReadAllLines("product.csv");

            foreach (string stroka in stroki)
            {
                if (string.IsNullOrWhiteSpace(stroka))
                {
                    continue;
                }

                string[] chasti = stroka.Split(',');
                if (chasti.Length < 4)
                {
                    continue;
                }

                if (decimal.TryParse(chasti[2], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal cena)
                    && int.TryParse(chasti[3], out int kolichestvo))
                {
                    sklad[chasti[0]] = new Tovar(chasti[0], chasti[1], cena, kolichestvo);
                }
            }

            Console.WriteLine($"Склад загружен, товаров: {sklad.Count}");
        }

        static Tovar NaytiTovar()
        {
            Console.Write("Введите артикул: ");
            string artikul = (Console.ReadLine() ?? "").Trim();

            if (!sklad.TryGetValue(artikul, out Tovar? tovar))
            {
                throw new OshibkaSklada("Товар с таким артикулом не найден");
            }

            return tovar;
        }

        static int ChitatKolichestvo(string podskazka)
        {
            Console.Write(podskazka);
            string vvod = Console.ReadLine() ?? "";

            if (!int.TryParse(vvod, out int kolichestvo) || kolichestvo <= 0)
            {
                throw new OshibkaSklada("Количество должно быть больше нуля");
            }

            return kolichestvo;
        }

        static void Poisk()
        {
            try
            {
                Tovar tovar = NaytiTovar();
                Console.WriteLine($"Артикул: {tovar.Artikul}");
                Console.WriteLine($"Название: {tovar.Nazvanie}");
                Console.WriteLine($"Цена: {tovar.Cena}");
                Console.WriteLine($"Остаток: {tovar.Kolichestvo}");
            }
            catch (OshibkaSklada oshibka)
            {
                Console.WriteLine(oshibka.Message);
            }
        }

        static void Prihod()
        {
            try
            {
                Tovar tovar = NaytiTovar();
                int kolichestvo = ChitatKolichestvo("Сколько пришло: ");
                tovar.Kolichestvo += kolichestvo;
                Console.WriteLine($"Приход оформлен, остаток: {tovar.Kolichestvo}");
            }
            catch (OshibkaSklada oshibka)
            {
                Console.WriteLine(oshibka.Message);
            }
        }

        static void Prodazha()
        {
            try
            {
                Tovar tovar = NaytiTovar();
                int kolichestvo = ChitatKolichestvo("Сколько продано: ");

                if (kolichestvo > tovar.Kolichestvo)
                {
                    throw new OshibkaSklada($"Не хватает товара, остаток: {tovar.Kolichestvo}");
                }

                tovar.Kolichestvo -= kolichestvo;
                Console.WriteLine($"Продажа оформлена, остаток: {tovar.Kolichestvo}");
            }
            catch (OshibkaSklada oshibka)
            {
                Console.WriteLine(oshibka.Message);
            }
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                Zagruzit();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл product.csv не найден");
                return;
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - найти товар");
                Console.WriteLine("2 - приход товара");
                Console.WriteLine("3 - продажа товара");
                Console.WriteLine("0 - выход");
                Console.Write("Выбор: ");
                string vybor = Console.ReadLine() ?? "";

                switch (vybor)
                {
                    case "1":
                        Poisk();
                        break;

                    case "2":
                        Prihod();
                        break;

                    case "3":
                        Prodazha();
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