using System;
using System.Collections.Generic;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text = "Алина бежала по дорожке, но Алина споткнулась и упала. Блин что же делать. Какая-то скользкая дорожка. Алина расстроилась";

            text = text.ToLower();
            string cleaned = "";
            foreach (char c in text)
            {
                if (char.IsLetter(c) || c == ' ')
                    cleaned += c;
                else
                    cleaned += ' ';
            }

            string[] words = cleaned.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> counts = new Dictionary<string, int>();
            foreach (string w in words)
            {
                if (counts.ContainsKey(w))
                    counts[w]++;
                else
                    counts[w] = 1;
            }

            MaxHeap heap = new MaxHeap();
            foreach (var pair in counts)
                heap.Insert(new WordCount(pair.Key, pair.Value));

            WordCount top = heap.ExtractMax();
            Console.WriteLine($"Самое частое слово: \"{top.Word}\" ({top.Count} раз)");
        }
    }
}