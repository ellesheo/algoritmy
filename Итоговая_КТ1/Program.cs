using System;

namespace ConsoleApp5
{
    internal class Program
    {
        static void Main()
        {
            SmartDispatcher d = new SmartDispatcher(55.7558, 37.6173, 0.4, 1.0, 0.5);

            Order o1 = new Order("o1", 55.77, 37.65, 500);
            Console.WriteLine("Заказ o1, цена 500");
            d.Dobavit(new Driver("d1", 55.80, 37.70), o1);
            d.Dobavit(new Driver("d2", 55.76, 37.64), o1);
            Console.WriteLine("добавили d1 и d2, лучший: " + d.Top());
            d.Obnovit("d1", 55.771, 37.651, o1);
            Console.WriteLine("d1 подъехал ближе, лучший: " + d.Top());
            d.Otmena("d1");
            Console.WriteLine("d1 отказался");
            Console.WriteLine("на заказ o1 едет: " + d.Naznachit());

            Console.WriteLine();
            d.Sbros();
            Order o2 = new Order("o2", 55.80, 37.70, 150);
            Console.WriteLine("Заказ o2, цена 150");
            d.Dobavit(new Driver("d1", 55.771, 37.651), o2);
            Console.WriteLine("на заказ o2 едет: " + d.Naznachit());
        }
    }
}