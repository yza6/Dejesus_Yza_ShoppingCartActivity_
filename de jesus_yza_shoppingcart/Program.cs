using System;

namespace dejesus
{

    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int RemainingStock;

        public Product(int id, string name, double price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            RemainingStock = stock;
        }

        public void DisplayProduct()
        {
            Console.WriteLine($"[{Id}] {Name} - PHP {Price:F2} (Stock: {RemainingStock})");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {


            Product[] storemenu = new Product[]
            {
                new Product(1, "Lipstick", 350, 15),
                new Product(2, "Foundation", 500, 10),
                new Product(3, "Blush", 250, 5),
                new Product(4, "Eyeliner", 100, 20),
                new Product(5, "Mascara", 300, 10)
            };

            Console.WriteLine("Welcome to my Shop!");

            foreach (Product item in storemenu)
            {
                item.DisplayProduct();
            }

            Console.WriteLine("\nPress any key to test the run...");
            Console.ReadKey();

        }
    }
}

