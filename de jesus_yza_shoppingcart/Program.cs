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

        public void DeductStock(int qty)
        {
            RemainingStock -= qty;
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
