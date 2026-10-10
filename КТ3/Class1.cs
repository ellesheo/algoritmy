using System;

namespace ConsoleApp4
{
    class Node
    {
        public int Key;
        public Node Left;
        public Node Right;
        public int Height;
        public Node(int key)
        {
            Key = key;
            Height = 1;
        }
    }

    class AvlTree
    {
        public Node Root;

        int GetHeight(Node node)
        {
            if (node == null) return 0;
            return node.Height;
        }

        int GetBalance(Node node)
        {
            if (node == null) return 0;
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        void UpdateHeight(Node node)
        {
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        }

        Node RotateRight(Node y)
        {
            Node x = y.Left;
            Node T2 = x.Right;
            x.Right = y;
            y.Left = T2;
            UpdateHeight(y);
            UpdateHeight(x);
            return x;
        }

        Node RotateLeft(Node x)
        {
            Node y = x.Right;
            Node T2 = y.Left;
            y.Left = x;
            x.Right = T2;
            UpdateHeight(x);
            UpdateHeight(y);
            return y;
        }

        Node Balance(Node node)
        {
            UpdateHeight(node);
            int balance = GetBalance(node);
            if (balance > 1 && GetBalance(node.Left) >= 0)
                return RotateRight(node);
            if (balance > 1 && GetBalance(node.Left) < 0)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            if (balance < -1 && GetBalance(node.Right) <= 0)
                return RotateLeft(node);
            if (balance < -1 && GetBalance(node.Right) > 0)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }
            return node;
        }

        public void Add(int key)
        {
            Root = Insert(Root, key);
        }

        Node Insert(Node node, int key)
        {
            if (node == null) return new Node(key);
            if (key < node.Key) node.Left = Insert(node.Left, key);
            else if (key > node.Key) node.Right = Insert(node.Right, key);
            else return node;
            return Balance(node);
        }

        public void Remove(int key)
        {
            Root = Delete(Root, key);
        }

        Node Delete(Node node, int key)
        {
            if (node == null) return null;
            if (key < node.Key) node.Left = Delete(node.Left, key);
            else if (key > node.Key) node.Right = Delete(node.Right, key);
            else
            {
                if (node.Left == null) return node.Right;
                if (node.Right == null) return node.Left;
                Node min = node.Right;
                while (min.Left != null) min = min.Left;
                node.Key = min.Key;
                node.Right = Delete(node.Right, min.Key);
            }
            return Balance(node);
        }

        public bool IsBalanced(Node node)
        {
            if (node == null) return true;
            int balance = GetBalance(node);
            if (balance > 1 || balance < -1) return false;
            return IsBalanced(node.Left) && IsBalanced(node.Right);
        }

        public void InOrder(Node node)
        {
            if (node == null) return;
            InOrder(node.Left);
            Console.Write(node.Key + " ");
            InOrder(node.Right);
        }
    }
}