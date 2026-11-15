using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace ConsoleApp5
{
    public class Fayl
    {
        public string Put { get; set; }
        public string Imya { get; set; }
        public string Ispolnitel { get; set; }
        public string Nazvanie { get; set; }
        public int Dlitelnost { get; set; }
        public long Razmer { get; set; }
        public string Hesh { get; set; } = "";
        public int NomerGruppy { get; set; }

        public Fayl(string put, string ispolnitel, string nazvanie, int dlitelnost, long razmer)
        {
            Put = put;
            Imya = Path.GetFileName(put);
            Ispolnitel = ispolnitel;
            Nazvanie = nazvanie;
            Dlitelnost = dlitelnost;
            Razmer = razmer;
        }
    }

    public class Pohozhij
    {
        public string Fayl { get; set; } = "";
        public string Prichina { get; set; } = "";
    }

    public class Gruppa
    {
        public string Kanonicheskij { get; set; } = "";
        public List<string> Lishnie { get; set; } = new List<string>();
        public List<Pohozhij> Pohozhie { get; set; } = new List<Pohozhij>();
        public long OsvobodyatsyaBajt { get; set; }
    }

    class Program
    {
        const int chisloHeshej = 100;
        const int dlinaShingla = 3;
        const long prostoe = 2147483647;

        static long[] mnozhiteli = new long[chisloHeshej];
        static long[] sdvigi = new long[chisloHeshej];

        static void NastroitMinHash()
        {
            Random sluchajnoe = new Random(12345);
            for (int i = 0; i < chisloHeshej; i++)
            {
                mnozhiteli[i] = sluchajnoe.Next(1, int.MaxValue);
                sdvigi[i] = sluchajnoe.Next(0, int.MaxValue);
            }
        }

        static string Normalizovat(string tekst)
        {
            StringBuilder rezultat = new StringBuilder();
            bool probel = false;

            foreach (char simvol in tekst.ToLower())
            {
                if (char.IsLetterOrDigit(simvol))
                {
                    rezultat.Append(simvol);
                    probel = false;
                }
                else if (char.IsWhiteSpace(simvol) && rezultat.Length > 0 && !probel)
                {
                    rezultat.Append(' ');
                    probel = true;
                }
            }

            return rezultat.ToString().Trim();
        }

        static Fayl? Razobrat(string put)
        {
            string imya = Path.GetFileNameWithoutExtension(put);
            string[] chasti = imya.Split(" - ");

            if (chasti.Length != 4)
            {
                return null;
            }

            string[] vremya = chasti[3].Split('.');
            if (vremya.Length != 2 || !int.TryParse(vremya[0], out int minuty) || !int.TryParse(vremya[1], out int sekundy))
            {
                return null;
            }

            return new Fayl(put, chasti[0], chasti[1], minuty * 60 + sekundy, new FileInfo(put).Length);
        }

        static string KlyuchGruppy(Fayl fayl)
        {
            string tekst = $"{Normalizovat(fayl.Ispolnitel)}|{Normalizovat(fayl.Nazvanie)}|{fayl.Dlitelnost}";
            return Convert.ToHexString(SHA1.HashData(Encoding.UTF8.GetBytes(tekst)));
        }

        static string HeshFayla(string put)
        {
            using IncrementalHash hesh = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
            using FileStream potok = File.OpenRead(put);
            byte[] bufer = new byte[81920];
            int prochitano;

            while ((prochitano = potok.Read(bufer, 0, bufer.Length)) > 0)
            {
                hesh.AppendData(bufer, 0, prochitano);
            }

            return Convert.ToHexString(hesh.GetHashAndReset());
        }

        static long HeshShingla(string shingl)
        {
            uint hesh = 2166136261;
            foreach (char simvol in shingl)
            {
                hesh ^= simvol;
                hesh *= 16777619;
            }
            return hesh % prostoe;
        }

        static long[] Signatura(string tekst)
        {
            HashSet<string> shingly = new HashSet<string>();

            if (tekst.Length < dlinaShingla)
            {
                shingly.Add(tekst);
            }
            else
            {
                for (int i = 0; i <= tekst.Length - dlinaShingla; i++)
                {
                    shingly.Add(tekst.Substring(i, dlinaShingla));
                }
            }

            long[] signatura = new long[chisloHeshej];
            Array.Fill(signatura, long.MaxValue);

            foreach (string shingl in shingly)
            {
                long osnova = HeshShingla(shingl);
                for (int i = 0; i < chisloHeshej; i++)
                {
                    long znachenie = (mnozhiteli[i] * osnova + sdvigi[i]) % prostoe;
                    if (znachenie < signatura[i])
                    {
                        signatura[i] = znachenie;
                    }
                }
            }

            return signatura;
        }

        static double Shozhest(long[] pervaya, long[] vtoraya)
        {
            int sovpalo = 0;
            for (int i = 0; i < chisloHeshej; i++)
            {
                if (pervaya[i] == vtoraya[i])
                {
                    sovpalo++;
                }
            }
            return sovpalo * 100.0 / chisloHeshej;
        }

        static int Koren(int[] roditel, int x)
        {
            while (roditel[x] != x)
            {
                roditel[x] = roditel[roditel[x]];
                x = roditel[x];
            }
            return x;
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            NastroitMinHash();

            Console.WriteLine("Формат имени файла: Исполнитель - Название - Альбом - 3.15.mp3");
            Console.Write("Папка с музыкой: ");
            string papka = (Console.ReadLine() ?? "").Trim().Trim('"');

            if (!Directory.Exists(papka))
            {
                Console.WriteLine("Папка не найдена");
                return;
            }

            Console.Write("Порог схожести в процентах, Enter = 85: ");
            string vvod = Console.ReadLine() ?? "";
            double porog = 85;
            if (vvod != "" && (!double.TryParse(vvod, out porog) || porog < 0 || porog > 100))
            {
                Console.WriteLine("Неверный порог, беру 85");
                porog = 85;
            }

            List<Fayl> fajly = new List<Fayl>();
            foreach (string put in Directory.GetFiles(papka, "*.mp3", SearchOption.AllDirectories))
            {
                Fayl? fayl = Razobrat(put);
                if (fayl == null)
                {
                    Console.WriteLine($"Пропущен файл с неверным именем: {Path.GetFileName(put)}");
                }
                else
                {
                    fajly.Add(fayl);
                }
            }

            Console.WriteLine($"Файлов прочитано: {fajly.Count}");

            List<List<Fayl>> gruppy = fajly
                .GroupBy(KlyuchGruppy)
                .Select(g => g.ToList())
                .ToList();

            for (int i = 0; i < gruppy.Count; i++)
            {
                foreach (Fayl fayl in gruppy[i])
                {
                    fayl.NomerGruppy = i;
                }
            }

            long[][] signatury = new long[gruppy.Count][];
            for (int i = 0; i < gruppy.Count; i++)
            {
                Fayl pervyj = gruppy[i][0];
                signatury[i] = Signatura(Normalizovat(pervyj.Ispolnitel) + " " + Normalizovat(pervyj.Nazvanie));
            }

            int[] roditel = new int[gruppy.Count];
            double[] luchshaya = new double[gruppy.Count];
            for (int i = 0; i < gruppy.Count; i++)
            {
                roditel[i] = i;
            }

            for (int i = 0; i < gruppy.Count; i++)
            {
                for (int j = i + 1; j < gruppy.Count; j++)
                {
                    if (gruppy[i][0].Dlitelnost != gruppy[j][0].Dlitelnost)
                    {
                        continue;
                    }

                    double shozhest = Shozhest(signatury[i], signatury[j]);
                    if (shozhest >= porog)
                    {
                        roditel[Koren(roditel, i)] = Koren(roditel, j);
                        luchshaya[i] = Math.Max(luchshaya[i], shozhest);
                        luchshaya[j] = Math.Max(luchshaya[j], shozhest);
                    }
                }
            }

            Dictionary<int, List<Fayl>> itogovye = new Dictionary<int, List<Fayl>>();
            for (int i = 0; i < gruppy.Count; i++)
            {
                int koren = Koren(roditel, i);
                if (!itogovye.ContainsKey(koren))
                {
                    itogovye[koren] = new List<Fayl>();
                }
                itogovye[koren].AddRange(gruppy[i]);
            }

            List<Gruppa> otchet = new List<Gruppa>();
            long vsegoOsvobodyatsya = 0;

            foreach (List<Fayl> vse in itogovye.Values)
            {
                if (vse.Count < 2)
                {
                    continue;
                }

                foreach (Fayl fayl in vse)
                {
                    fayl.Hesh = HeshFayla(fayl.Put);
                }

                Fayl kanon = vse.OrderBy(f => f.Imya.Length).ThenBy(f => f.Imya).First();
                Gruppa gruppa = new Gruppa { Kanonicheskij = kanon.Imya };

                foreach (var podgruppa in vse.GroupBy(f => f.Hesh))
                {
                    List<Fayl> spisok = podgruppa.OrderBy(f => f.Imya.Length).ThenBy(f => f.Imya).ToList();
                    Fayl predstavitel = spisok.Contains(kanon) ? kanon : spisok[0];

                    foreach (Fayl fayl in spisok)
                    {
                        if (fayl != predstavitel)
                        {
                            gruppa.Lishnie.Add(fayl.Imya);
                            gruppa.OsvobodyatsyaBajt += fayl.Razmer;
                        }
                    }

                    if (predstavitel != kanon)
                    {
                        string prichina = predstavitel.NomerGruppy == kanon.NomerGruppy
                            ? "другая запись с теми же тегами"
                            : $"вероятный дубликат с опечаткой, схожесть {luchshaya[predstavitel.NomerGruppy]:F0}%";
                        gruppa.Pohozhie.Add(new Pohozhij { Fayl = predstavitel.Imya, Prichina = prichina });
                    }
                }

                vsegoOsvobodyatsya += gruppa.OsvobodyatsyaBajt;
                otchet.Add(gruppa);
            }

            if (otchet.Count == 0)
            {
                Console.WriteLine("Дубликатов и похожих треков нет");
                return;
            }

            for (int i = 0; i < otchet.Count; i++)
            {
                Console.WriteLine($"Группа {i + 1}: канонический {otchet[i].Kanonicheskij}, лишних {otchet[i].Lishnie.Count}, похожих {otchet[i].Pohozhie.Count}");
            }

            Console.WriteLine($"Освободится места: {vsegoOsvobodyatsya} байт");

            JsonSerializerOptions nastrojki = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string putOtcheta = Path.Combine(Directory.GetCurrentDirectory(), "otchet.json");
            File.WriteAllText(putOtcheta, JsonSerializer.Serialize(otchet, nastrojki), Encoding.UTF8);
            Console.WriteLine($"Отчёт сохранён: {putOtcheta}");
        }
    }
}