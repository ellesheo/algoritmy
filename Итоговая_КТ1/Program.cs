using System;
using System.Collections.Generic;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main()
        {
            TreeNode root = null;
            root = Tree.Insert(root, new Request(50, "Ошибка оплаты", 9));
            root = Tree.Insert(root, new Request(30, "Сброс пароля", 4));
            root = Tree.Insert(root, new Request(70, "Интеграция API", 13));
            root = Tree.Insert(root, new Request(20, "Изменение профиля", 2));
            root = Tree.Insert(root, new Request(40, "Ошибка отчёта", 7));
            root = Tree.Insert(root, new Request(60, "Настройка уведомлений", 11));
            root = Tree.Insert(root, new Request(80, "Сбой сервера", 15));
            root = Tree.Insert(root, new Request(10, "Удаление аккаунта", 1));
            root = Tree.Insert(root, new Request(25, "Проблема входа", 3));
            root = Tree.Insert(root, new Request(35, "Экспорт данных", 5));
            root = Tree.Insert(root, new Request(45, "Возврат платежа", 8));
            root = Tree.Insert(root, new Request(55, "Подключение тарифа", 10));
            root = Tree.Insert(root, new Request(65, "Ошибка синхронизации", 12));
            root = Tree.Insert(root, new Request(75, "Недоступна база данных", 14));
            root = Tree.Insert(root, new Request(90, "Обновление реквизитов", 6));

            Console.WriteLine("Дерево по уровням:");
            PrintP(Tree.LevelOrder(root));

            Console.WriteLine();
            Console.WriteLine("Обходы:");
            Console.Write("прямой: "); PrintId(Tree.PreOrder(root));
            Console.Write("симметричный: "); PrintId(Tree.InOrder(root));
            Console.Write("обратный: "); PrintId(Tree.PostOrder(root));
            Console.Write("по уровням: "); PrintId(Tree.LevelOrder(root));

            Console.WriteLine();
            Console.WriteLine("Поиск и характеристики:");
            List<int> path = new List<int>();
            Request found = Tree.Search(root, 65, path);
            Console.Write("поиск 65: ");
            if (found != null) Console.Write("есть (" + found.Title + ")");
            else Console.Write("нет");
            Console.Write(", путь: ");
            foreach (int x in path) Console.Write(x + " ");
            Console.WriteLine();

            List<int> path2 = new List<int>();
            Request found2 = Tree.Search(root, 99, path2);
            Console.Write("поиск 99: ");
            if (found2 != null) Console.Write("есть");
            else Console.Write("нет");
            Console.Write(", путь: ");
            foreach (int x in path2) Console.Write(x + " ");
            Console.WriteLine();

            Console.WriteLine("узлов: " + Tree.CountNodes(root));
            Console.WriteLine("листьев: " + Tree.CountLeaves(root));
            Console.WriteLine("высота: " + Tree.Height(root));
            Console.WriteLine("мин id: " + Tree.FindMin(root).Id);
            Console.WriteLine("макс id: " + Tree.FindMax(root).Id);
            Console.WriteLine("глубина 65: " + Tree.GetDepth(root, 65));
            Console.WriteLine("bst: " + Tree.ValidateBst(root));

            Console.WriteLine();
            Console.WriteLine("Изменение дерева:");
            root = Tree.Insert(root, new Request(37, "Утечка данных", 16));
            PrintState(root, "добавили 37");

            int before = Tree.CountNodes(root);
            root = Tree.Insert(root, new Request(50, "Дубликат", 0));
            if (Tree.CountNodes(root) == before) Console.WriteLine("дубликат 50 отклонён");
            else Console.WriteLine("дубликат добавлен");

            root = Tree.Delete(root, 10);
            PrintState(root, "удалили 10 (лист)");
            root = Tree.Delete(root, 35);
            PrintState(root, "удалили 35 (один потомок)");
            root = Tree.Delete(root, 70);
            PrintState(root, "удалили 70 (два потомка)");

            before = Tree.CountNodes(root);
            root = Tree.Delete(root, 999);
            if (Tree.CountNodes(root) == before) Console.WriteLine("узел 999 не найден, без изменений");

            Console.WriteLine();
            Console.WriteLine("Построение кучи:");
            Request[] data = Tree.InOrder(root).ToArray();
            Console.Write("массив: "); PrintP2(data);
            Heap.BuildMaxHeap(data);
            Console.Write("куча: "); PrintP2(data);
            Console.WriteLine("по уровням:");
            PrintLevels(data);
            Console.WriteLine("is max heap: " + Heap.IsMaxHeap(data));

            Console.WriteLine();
            Console.WriteLine("Сортировка кучей:");
            Request[] report = Tree.InOrder(root).ToArray();
            Heap.HeapSort(report);
            Console.Write("отчёт: "); PrintP2(report);
        }

        static void PrintState(TreeNode root, string op)
        {
            Console.WriteLine(op);
            Console.Write("обход: "); PrintId(Tree.InOrder(root));
            Console.WriteLine("узлов: " + Tree.CountNodes(root) + ", высота: " + Tree.Height(root) + ", bst: " + Tree.ValidateBst(root));
        }

        static void PrintP(List<Request> list)
        {
            foreach (Request r in list) Console.Write(r.Id + "(" + r.Priority + ") ");
            Console.WriteLine();
        }

        static void PrintP2(Request[] data)
        {
            foreach (Request r in data) Console.Write(r.Id + "(" + r.Priority + ") ");
            Console.WriteLine();
        }

        static void PrintId(List<Request> list)
        {
            foreach (Request r in list) Console.Write(r.Id + " ");
            Console.WriteLine();
        }

        static void PrintLevels(Request[] data)
        {
            int count = 1;
            int idx = 0;
            while (idx < data.Length)
            {
                for (int k = 0; k < count && idx < data.Length; k++)
                {
                    Console.Write(data[idx].Id + "(" + data[idx].Priority + ") ");
                    idx++;
                }
                Console.WriteLine();
                count = count * 2;
            }
        }
    }
}