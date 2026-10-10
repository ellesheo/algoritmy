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

            tree.Dobavit(1);
            Console.Write("После вставки 1: ");
            tree.Obhod(tree.Root);
            Console.WriteLine();
            Console.WriteLine("Сбалансировано: " + tree.Proverka(tree.Root));

            tree.Udalit(10);
            Console.Write("После удаления 10: ");
            tree.Obhod(tree.Root);
            Console.WriteLine();
            Console.WriteLine("Сбалансировано: " + tree.Proverka(tree.Root));
        }
    }
}
