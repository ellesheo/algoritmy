using System;
using System.Collections.Generic;

namespace ConsoleApp4
{
    class Request
    {
        public int Id;
        public string Title;
        public int Priority;
        public Request(int id, string title, int priority)
        {
            Id = id;
            Title = title;
            Priority = priority;
        }
    }

    class TreeNode
    {
        public Request Request;
        public TreeNode Left;
        public TreeNode Right;
        public TreeNode(Request request)
        {
            Request = request;
        }
    }

    static class Tree
    {
        public static TreeNode Insert(TreeNode root, Request request)
        {
            if (root == null) return new TreeNode(request);
            if (request.Id < root.Request.Id) root.Left = Insert(root.Left, request);
            else if (request.Id > root.Request.Id) root.Right = Insert(root.Right, request);
            return root;
        }

        public static List<Request> PreOrder(TreeNode root)
        {
            List<Request> res = new List<Request>();
            PreOrderGo(root, res);
            return res;
        }

        static void PreOrderGo(TreeNode node, List<Request> res)
        {
            if (node == null) return;
            res.Add(node.Request);
            PreOrderGo(node.Left, res);
            PreOrderGo(node.Right, res);
        }

        public static List<Request> InOrder(TreeNode root)
        {
            List<Request> res = new List<Request>();
            InOrderGo(root, res);
            return res;
        }

        static void InOrderGo(TreeNode node, List<Request> res)
        {
            if (node == null) return;
            InOrderGo(node.Left, res);
            res.Add(node.Request);
            InOrderGo(node.Right, res);
        }

        public static List<Request> PostOrder(TreeNode root)
        {
            List<Request> res = new List<Request>();
            PostOrderGo(root, res);
            return res;
        }

        static void PostOrderGo(TreeNode node, List<Request> res)
        {
            if (node == null) return;
            PostOrderGo(node.Left, res);
            PostOrderGo(node.Right, res);
            res.Add(node.Request);
        }

        public static List<Request> LevelOrder(TreeNode root)
        {
            List<Request> res = new List<Request>();
            if (root == null) return res;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            while (q.Count > 0)
            {
                TreeNode n = q.Dequeue();
                res.Add(n.Request);
                if (n.Left != null) q.Enqueue(n.Left);
                if (n.Right != null) q.Enqueue(n.Right);
            }
            return res;
        }

        public static Request Search(TreeNode root, int id, List<int> path)
        {
            TreeNode cur = root;
            while (cur != null)
            {
                path.Add(cur.Request.Id);
                if (id == cur.Request.Id) return cur.Request;
                if (id < cur.Request.Id) cur = cur.Left;
                else cur = cur.Right;
            }
            return null;
        }

        public static Request FindMin(TreeNode root)
        {
            while (root.Left != null) root = root.Left;
            return root.Request;
        }

        public static Request FindMax(TreeNode root)
        {
            while (root.Right != null) root = root.Right;
            return root.Request;
        }

        public static int CountNodes(TreeNode root)
        {
            if (root == null) return 0;
            return 1 + CountNodes(root.Left) + CountNodes(root.Right);
        }

        public static int CountLeaves(TreeNode root)
        {
            if (root == null) return 0;
            if (root.Left == null && root.Right == null) return 1;
            return CountLeaves(root.Left) + CountLeaves(root.Right);
        }

        public static int Height(TreeNode root)
        {
            if (root == null) return 0;
            return 1 + Math.Max(Height(root.Left), Height(root.Right));
        }

        public static int GetDepth(TreeNode root, int id)
        {
            TreeNode cur = root;
            int depth = 0;
            while (cur != null)
            {
                if (id == cur.Request.Id) return depth;
                if (id < cur.Request.Id) cur = cur.Left;
                else cur = cur.Right;
                depth++;
            }
            return -1;
        }

        public static bool ValidateBst(TreeNode root)
        {
            return ValidateBst(root, long.MinValue, long.MaxValue);
        }

        static bool ValidateBst(TreeNode node, long min, long max)
        {
            if (node == null) return true;
            if (node.Request.Id <= min || node.Request.Id >= max) return false;
            return ValidateBst(node.Left, min, node.Request.Id) && ValidateBst(node.Right, node.Request.Id, max);
        }

        public static TreeNode Delete(TreeNode root, int id)
        {
            if (root == null) return null;
            if (id < root.Request.Id) root.Left = Delete(root.Left, id);
            else if (id > root.Request.Id) root.Right = Delete(root.Right, id);
            else
            {
                if (root.Left == null) return root.Right;
                if (root.Right == null) return root.Left;
                TreeNode succ = root.Right;
                while (succ.Left != null) succ = succ.Left;
                root.Request = succ.Request;
                root.Right = Delete(root.Right, succ.Request.Id);
            }
            return root;
        }
    }

    static class Heap
    {
        static bool Bolshe(Request a, Request b)
        {
            if (a.Priority != b.Priority) return a.Priority > b.Priority;
            return a.Id > b.Id;
        }

        public static void SiftDown(Request[] data, int heapSize, int index)
        {
            while (true)
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int largest = index;
                if (left < heapSize && Bolshe(data[left], data[largest])) largest = left;
                if (right < heapSize && Bolshe(data[right], data[largest])) largest = right;
                if (largest == index) break;
                Obmen(data, index, largest);
                index = largest;
            }
        }

        public static void BuildMaxHeap(Request[] data)
        {
            for (int i = data.Length / 2 - 1; i >= 0; i--)
                SiftDown(data, data.Length, i);
        }

        public static bool IsMaxHeap(Request[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                if (left < data.Length && Bolshe(data[left], data[i])) return false;
                if (right < data.Length && Bolshe(data[right], data[i])) return false;
            }
            return true;
        }

        public static void HeapSort(Request[] data)
        {
            BuildMaxHeap(data);
            for (int end = data.Length - 1; end > 0; end--)
            {
                Obmen(data, 0, end);
                SiftDown(data, end, 0);
            }
        }

        static void Obmen(Request[] data, int i, int j)
        {
            Request temp = data[i];
            data[i] = data[j];
            data[j] = temp;
        }
    }
}