using System;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 10, 20, 30, 40, 50, 25 };
            AvlTree tree = new AvlTree();

            Console.Write("Исходный массив: ");
            foreach (int x in arr) Console.Write(x + " ");
            Console.WriteLine();

            foreach (int x in arr) tree.Dobavit(x);

            Console.Write("Обход: ");
            tree.Obhod(tree.Root);
            Console.WriteLine();
            Console.WriteLine("Сбалансировано: " + tree.Proverka(tree.Root));

            Console.Write("Введите число для добавления: ");
            int add = int.Parse(Console.ReadLine());
            tree.Dobavit(add);
            Console.Write("После добавления " + add + ": ");
            tree.Obhod(tree.Root);
            Console.WriteLine();
            Console.WriteLine("Сбалансировано: " + tree.Proverka(tree.Root));

            Console.Write("Введите число для удаления: ");
            int del = int.Parse(Console.ReadLine());
            tree.Udalit(del);
            Console.Write("После удаления " + del + ": ");
            tree.Obhod(tree.Root);
            Console.WriteLine();
            Console.WriteLine("Сбалансировано: " + tree.Proverka(tree.Root));
        }
    }
}
