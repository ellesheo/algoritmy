using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace ConsoleApp5
{
    internal class Program
    {
        static Dictionary<string, UserCredentials> polzovateli = new Dictionary<string, UserCredentials>();

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("аутентификация");
                Console.WriteLine("1 - регистрация");
                Console.WriteLine("2 - вход");
                Console.WriteLine("0 - выход");
                Console.Write("выберите: ");
                string vybor = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    if (vybor == "1") Registraciya();
                    else if (vybor == "2") Vhod();
                    else if (vybor == "0") break;
                    else Console.WriteLine("нет такого пункта");
                }
                catch (OshibkaAuth ex)
                {
                    Console.WriteLine("ошибка: " + ex.Message);
                }
                Console.WriteLine();
            }
        }

        static void Registraciya()
        {
            Console.Write("имя пользователя: ");
            string imya = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(imya))
                throw new OshibkaAuth("имя не может быть пустым");
            if (polzovateli.ContainsKey(imya))
                throw new OshibkaAuth("такой пользователь уже есть");

            Console.Write("пароль: ");
            string parol = ChitatParol();
            Console.Write("повторите пароль: ");
            string parol2 = ChitatParol();
            if (parol != parol2)
                throw new OshibkaAuth("пароли не совпадают");

            byte[] sol = SgenerirovatSol();
            byte[] hesh = Heshirovat(parol, sol);
            polzovateli[imya] = new UserCredentials(sol, hesh);
            Console.WriteLine("регистрация прошла успешно");
        }

        static void Vhod()
        {
            Console.Write("имя пользователя: ");
            string imya = Console.ReadLine();
            if (!polzovateli.ContainsKey(imya))
                throw new OshibkaAuth("пользователь не найден");

            Console.Write("пароль: ");
            string parol = ChitatParol();

            UserCredentials u = polzovateli[imya];
            byte[] hesh = Heshirovat(parol, u.Salt);
            if (CryptographicOperations.FixedTimeEquals(hesh, u.PasswordHash))
                Console.WriteLine("доступ разрешен, добро пожаловать, " + imya);
            else
                Console.WriteLine("неверный пароль");
        }

        static byte[] SgenerirovatSol()
        {
            byte[] sol = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                rng.GetBytes(sol);
            return sol;
        }

        static byte[] Heshirovat(string parol, byte[] sol)
        {
            byte[] baytyParolya = Encoding.UTF8.GetBytes(parol);
            byte[] vmeste = new byte[baytyParolya.Length + sol.Length];
            Array.Copy(baytyParolya, 0, vmeste, 0, baytyParolya.Length);
            Array.Copy(sol, 0, vmeste, baytyParolya.Length, sol.Length);
            return SHA256.HashData(vmeste);
        }

        static string ChitatParol()
        {
            try
            {
                string parol = "";
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        break;
                    }
                    if (key.Key == ConsoleKey.Backspace)
                    {
                        if (parol.Length > 0)
                        {
                            parol = parol.Substring(0, parol.Length - 1);
                            Console.Write("\b \b");
                        }
                    }
                    else if (!char.IsControl(key.KeyChar))
                    {
                        parol += key.KeyChar;
                        Console.Write("*");
                    }
                }
                return parol;
            }
            catch (InvalidOperationException)
            {
                return Console.ReadLine();
            }
        }
    }
}