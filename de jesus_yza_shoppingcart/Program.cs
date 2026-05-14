using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dejesus
{
    class Product
    {
        private int id;
        private string name;
        private double price;
        private int remainingStock;
        private string category;
        private int reorderLevel = 5;

        public Product(int id, string name, double price, int stock, string category)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.remainingStock = stock;
            this.category = category;
        }

        public int GetId() { return this.id; }
        public string GetName() { return this.name; }
        public double GetPrice() { return this.price; }
        public int GetRemainingStock() { return this.remainingStock; }
        public string GetCategory() { return this.category; }

        public void SetRemainingStock(int qty) { this.remainingStock = qty; }

        public void DisplayProduct()
        {
            Console.WriteLine($"[{GetId()}] {GetName()} ({GetCategory()}) - PHP {GetPrice():F2} (Stock: {GetRemainingStock()})");
        }

        public bool HasEnoughStock(int qty)
        {
            return qty <= GetRemainingStock();
        }

        public double GetItemTotal(int qty)
        {
            return GetPrice() * qty;
        }

        public void DeductStock(int qty)
        {
            this.remainingStock -= qty;
        }

        public void AddStock(int qty)
        {
            this.remainingStock += qty;
        }
    }

    class CartItem
    {
        private Product product;
        private int quantity;
        private double subTotal;

        public CartItem(Product product, int qty)
        {
            this.product = product;
            this.quantity = qty;
            this.subTotal = product.GetPrice() * qty;
        }

        public Product GetProduct() { return this.product; }
        public int GetQuantity() { return this.quantity; }
        public double GetSubTotal() { return this.subTotal; }

        public void Update(int qty)
        {
            this.quantity += qty;
            this.subTotal = this.product.GetPrice() * this.quantity;
        }
    }

    class Transaction
    {
        private string receiptNo;
        private DateTime date;
        private double finalTotal;

        public Transaction(string receiptNo, DateTime date, double finalTotal)
        {
            this.receiptNo = receiptNo;
            this.date = date;
            this.finalTotal = finalTotal;
        }

        public string GetReceiptNo() { return this.receiptNo; }
        public DateTime GetDate() { return this.date; }
        public double GetFinalTotal() { return this.finalTotal; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Product[] storemenu = new Product[]
            {
                new Product (1, "Lipstick", 350, 15, "Cosmetics"),
                new Product (2, "Foundation", 500, 10, "Cosmetics"),
                new Product (3, "Blush", 267, 5, "Cosmetics"),
                new Product (4, "Eyeliner", 100, 20, "Cosmetics"),
                new Product (5, "Mascara", 300, 10, "Cosmetics")
            };

            CartItem[] cart = new CartItem[10];
            int cartCount = 0;
            Transaction[] orderHistory = new Transaction[100];
            int historyCount = 0;
            bool systemRunning = true;

            while (systemRunning)
            {
                Console.WriteLine("\n==============================");
                Console.WriteLine("      STORE MENU");
                Console.WriteLine("==============================");
                Console.WriteLine("1. View Store ");
                Console.WriteLine("2. View/Manage Cart");
                Console.WriteLine("3. Checkout & Payment");
                Console.WriteLine("4. View Transaction History");
                Console.WriteLine("5. Exit");
                Console.Write("\nSelect an option: ");

                string menuChoice = Console.ReadLine();

                if (menuChoice == "1")
                {
                    bool shopping = true;
                    while (shopping)
                    {
                        Console.WriteLine("\n--- STORE MENU ---");
                        foreach (Product item in storemenu) { item.DisplayProduct(); }

                        Console.Write("\nEnter product number: ");
                        if (!int.TryParse(Console.ReadLine(), out int productId) || productId < 1 || productId > storemenu.Length)
                        {
                            Console.WriteLine("Invalid input.");
                            continue;
                        }

                        Product selected = storemenu[productId - 1];
                        if (selected.GetRemainingStock() == 0)
                        {
                            Console.WriteLine("Out of stock.");
                            continue;
                        }

                        Console.Write("Enter quantity: ");
                        if (int.TryParse(Console.ReadLine(), out int qty) && selected.HasEnoughStock(qty))
                        {
                            bool found = false;
                            for (int i = 0; i < cartCount; i++)
                            {
                                if (cart[i].GetProduct().GetId() == selected.GetId())
                                {
                                    cart[i].Update(qty);
                                    found = true;
                                    break;
                                }
                            }

                            if (!found && cartCount < cart.Length)
                            {
                                cart[cartCount++] = new CartItem(selected, qty);
                            }

                            selected.DeductStock(qty);
                            Console.WriteLine("Added!");
                        }

                        Console.Write("\nAdd more? (Y/N): ");
                        if (Console.ReadLine().ToUpper() != "Y") shopping = false;
                    }
                }
                else if (menuChoice == "2")
                {
                    if (cartCount == 0) Console.WriteLine("Cart empty.");
                    else
                    {
                        for (int i = 0; i < cartCount; i++)
                        {
                            Console.WriteLine($"{i + 1}. {cart[i].GetProduct().GetName()} x{cart[i].GetQuantity()} - PHP {cart[i].GetSubTotal():F2}");
                        }
                    }
                }
                else if (menuChoice == "3")
                {
                    if (cartCount == 0) continue;
                    double grandTotal = 0;
                    for (int i = 0; i < cartCount; i++) { grandTotal += cart[i].GetSubTotal(); }

                    double discount = (grandTotal >= 5000) ? grandTotal * 0.10 : 0;
                    double finalTotal = grandTotal - discount;

                    Console.WriteLine($"Final Amount: PHP {finalTotal:F2}");
                    Console.Write("Enter Payment: ");
                    if (double.TryParse(Console.ReadLine(), out double payment) && payment >= finalTotal)
                    {
                        string recNo = "REC-" + (1001 + historyCount);
                        orderHistory[historyCount++] = new Transaction(recNo, DateTime.Now, finalTotal);
                        cartCount = 0;
                        Console.WriteLine("Payment Successful!");
                    }
                }
                else if (menuChoice == "4")
                {
                    Console.WriteLine("\n--- TRANSACTION HISTORY ---");

                    if (historyCount == 0)
                    {
                        Console.WriteLine("No transactions found yet.");
                    }
                    else
                    {
                        for (int i = 0; i < historyCount; i++)
                        {
                            Console.WriteLine($"{orderHistory[i].GetReceiptNo()} | {orderHistory[i].GetDate()} | Total: PHP {orderHistory[i].GetFinalTotal():F2}");
                        }
                    }

                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }
                else if (menuChoice == "5") systemRunning = false;
            }
        }
    }
}

