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

        int Vysota(Node node)
        {
            if (node == null) return 0;
            return node.Height;
        }

        int Balans(Node node)
        {
            if (node == null) return 0;
            return Vysota(node.Left) - Vysota(node.Right);
        }

        void Obnovit(Node node)
        {
            node.Height = 1 + Math.Max(Vysota(node.Left), Vysota(node.Right));
        }

        Node PravyPovorot(Node y)
        {
            Node x = y.Left;
            Node T2 = x.Right;
            x.Right = y;
            y.Left = T2;
            Obnovit(y);
            Obnovit(x);
            return x;
        }

        Node LevyPovorot(Node x)
        {
            Node y = x.Right;
            Node T2 = y.Left;
            y.Left = x;
            x.Right = T2;
            Obnovit(x);
            Obnovit(y);
            return y;
        }

        Node Vyrovnyat(Node node)
        {
            Obnovit(node);
            int balance = Balans(node);
            if (balance > 1 && Balans(node.Left) >= 0)
                return PravyPovorot(node);
            if (balance > 1 && Balans(node.Left) < 0)
            {
                node.Left = LevyPovorot(node.Left);
                return PravyPovorot(node);
            }
            if (balance < -1 && Balans(node.Right) <= 0)
                return LevyPovorot(node);
            if (balance < -1 && Balans(node.Right) > 0)
            {
                node.Right = PravyPovorot(node.Right);
                return LevyPovorot(node);
            }
            return node;
        }

        public void Dobavit(int key)
        {
            Root = Vstavka(Root, key);
        }

        Node Vstavka(Node node, int key)
        {
            if (node == null) return new Node(key);
            if (key < node.Key) node.Left = Vstavka(node.Left, key);
            else if (key > node.Key) node.Right = Vstavka(node.Right, key);
            else return node;
            return Vyrovnyat(node);
        }

        public void Udalit(int key)
        {
            Root = Udalenie(Root, key);
        }

        Node Udalenie(Node node, int key)
        {
            if (node == null) return null;
            if (key < node.Key) node.Left = Udalenie(node.Left, key);
            else if (key > node.Key) node.Right = Udalenie(node.Right, key);
            else
            {
                if (node.Left == null) return node.Right;
                if (node.Right == null) return node.Left;
                Node min = node.Right;
                while (min.Left != null) min = min.Left;
                node.Key = min.Key;
                node.Right = Udalenie(node.Right, min.Key);
            }
            return Vyrovnyat(node);
        }

        public bool Proverka(Node node)
        {
            if (node == null) return true;
            int balance = Balans(node);
            if (balance > 1 || balance < -1) return false;
            return Proverka(node.Left) && Proverka(node.Right);
        }

        public void Obhod(Node node)
        {
            if (node == null) return;
            Obhod(node.Left);
            Console.Write(node.Key + " ");
            Obhod(node.Right);
        }
    }
}
