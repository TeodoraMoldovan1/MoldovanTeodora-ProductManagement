using ProductManagement;
using System;

namespace ProductManagementApp
{
    // Clasa pentru produse
    public class Product
    {
        public int ProductId { get; set; } // Adauga proprietatea ProductId
        public string Name { get; set; }
        public double Price { get; set; }

        public Product() { } // Constructor implicit necesar pentru EF
        private IPriceStrategy priceStrategy;

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public void ApplyPriceStrategy(IPriceStrategy strategy)
        {
            priceStrategy = strategy;
        }

        public double GetFinalPrice()
        {
            return priceStrategy.ApplyPrice(Price);
        }
    }
}
