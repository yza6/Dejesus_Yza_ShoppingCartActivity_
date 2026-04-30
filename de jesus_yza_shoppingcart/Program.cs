using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dejesus
{
    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int RemainingStock;
        public string Category;
        public int ReorderLevel = 5;

        public Product(int id, string name, double price, int stock, string category)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
            RemainingStock = stock;
        }

        public void DisplayProduct()
        {
            Console.WriteLine($"[{Id}] {Name} ({Category}) - PHP {Price:F2} (Stock: {RemainingStock})");
        }

        public bool HasEnoughStock(int qty)
        {
            return qty <= RemainingStock;
        }
        public double GetItemTotal(int qty)
        {
            return Price * qty;
        }
        public void AddStock(int qty)
       {
            RemainingStock += qty;
       }

    }

    class CartItem
    {
        public Product Product;
        public int Quantity;
        public double SubTotal;

        public CartItem(Product product, int qty)
        {
            Product = product;
            Quantity = qty;
            SubTotal = product.Price * qty;
        }

        public void Update(int qty)
        {
            Quantity += qty;
            SubTotal = Product.Price * Quantity;
        }
    }

    class Transaction
    {
        public string ReceiptNo;
        public DateTime Date;
        public double FinalTotal;

        public Transaction(string receiptNo, DateTime date, double finalTotal)
        {
            ReceiptNo = receiptNo;
            Date = date;
            FinalTotal = finalTotal;
        }
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
    Console.WriteLine("     STORE MENU");
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
            foreach (Product item in storemenu)
            {
                item.DisplayProduct();
            }

            Console.Write("\nEnter product number: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            if (productId < 1 || productId > storemenu.Length)
            {
                Console.WriteLine("Invalid product number.");
                continue;
            }

            Product selected = storemenu[productId - 1];

            if (selected.RemainingStock == 0)
            {
                Console.WriteLine("This product is out of stock.");
                continue;
            }

            Console.Write("Enter quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                continue;
            }

            if (!selected.HasEnoughStock(qty))
            {
                Console.WriteLine("Not enough stock available.");
                continue;
            }

            bool found = false;
            for (int i = 0; i < cartCount; i++)
            {
                if (cart[i].Product.Id == selected.Id)
                {
                    cart[i].Update(qty);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                if (cartCount >= cart.Length)
                {
                    Console.WriteLine("Cart is full.");
                    continue;
                }

                cart[cartCount++] = new CartItem(selected, qty);
            }

            selected.DeductStock(qty);

            Console.WriteLine("Item added to cart!");

            bool validChoice = false;
            while (!validChoice)
            {
                Console.Write("\nAdd more items? (Y/N): ");
                string choice = Console.ReadLine().ToUpper();

                if (choice == "Y")
                {
                    validChoice = true;
                }
                else if (choice == "N")
                {
                    validChoice = true;
                    shopping = false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'Y' for Yes or 'N' for No.");
                }

            }
        }
    }
    else if (menuChoice == "2")
    {
    Console.WriteLine("\n--- YOUR CART ---");
    if (cartCount == 0) { Console.WriteLine("Cart is empty."); }
    else
    {
        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine($"{i + 1}. {cart[i].Product.Name} x{cart[i].Quantity} - PHP {cart[i].SubTotal:F2}");
        }
        Console.WriteLine("\n[R] Remove Item | [C] Clear Cart | [B] Back");
        string cartOp = Console.ReadLine().ToUpper();

        if (cartOp == "R")
        {
            Console.Write("Enter item number to remove: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= cartCount)
            {
                cart[idx - 1].Product.AddStock(cart[idx - 1].Quantity); 
                for (int i = idx - 1; i < cartCount - 1; i++) { cart[i] = cart[i + 1]; } 
                cartCount--;
                Console.WriteLine("Item removed.");
            }
        }
        else if (cartOp == "C")
        {
            for (int i = 0; i < cartCount; i++) { cart[i].Product.AddStock(cart[i].Quantity); }
            cartCount = 0;
            Console.WriteLine("Cart cleared.");
        }

    }
}
else if (menuChoice == "3")
                {
                    if (cartCount == 0) { Console.WriteLine("Cart is empty."); continue; }

                    double grandTotal = 0;
                    foreach (var item in cart) 
                    { 
                        if (item != null) grandTotal += item.SubTotal; 
                    }

                    double discount = (grandTotal >= 5000) ? grandTotal * 0.10 : 0;
                    double finalTotal = grandTotal - discount;

                    Console.WriteLine($"\nTotal: PHP {grandTotal:F2}");
                    if (discount > 0) Console.WriteLine($"Discount (10%): -PHP {discount:F2}");
                    Console.WriteLine($"Final Amount Due: PHP {finalTotal:F2}");

                    Console.Write("Enter Payment Amount: ");
                    if (double.TryParse(Console.ReadLine(), out double payment) && payment >= finalTotal)
                    {
                        double change = payment - finalTotal;
                        string recNo = "REC-" + (1001 + historyCount);

                        Console.WriteLine("\n--- OFFICIAL RECEIPT ---");
                        Console.WriteLine($"Receipt No: {recNo}");
                        Console.WriteLine($"Date: {DateTime.Now}");
                        Console.WriteLine($"Total Paid: PHP {payment:F2}");
                        Console.WriteLine($"Change: PHP {change:F2}");

                        orderHistory[historyCount++] = new Transaction(recNo, DateTime.Now, finalTotal);

                        foreach (var p in storemenu)
                        {
                            if (p.RemainingStock <= 5)
                            {
                                Console.WriteLine($"\n[ALERT] {p.Name} stock is low: {p.RemainingStock} left.");
                            }
                        }
                        cartCount = 0; 
                    }
                    else { Console.WriteLine("Invalid payment or insufficient funds."); }
                }
                else if (menuChoice == "4")
                {
                    Console.WriteLine("\n--- TRANSACTION HISTORY ---");
                    if (historyCount == 0) Console.WriteLine("No records yet.");
                    for (int i = 0; i < historyCount; i++)
                    {
                        Console.WriteLine($"{orderHistory[i].ReceiptNo} | {orderHistory[i].Date} | Total: PHP {orderHistory[i].FinalTotal:F2}");
                    }
                }
                else if (menuChoice == "5") { systemRunning = false; }
            }
        }
    }
}
