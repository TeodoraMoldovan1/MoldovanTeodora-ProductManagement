using ProductManagementApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement
{
    public class PriceFilterStrategy : IStrategy
    {
        private double minPrice;
        private double maxPrice;

        public PriceFilterStrategy(double minPrice, double maxPrice)
        {
            this.minPrice = minPrice;
            this.maxPrice = maxPrice;
        }

        public void ExecuteStrategy(List<Product> products)
        {
            throw new NotImplementedException();
        }

        public List<Product> Filter(List<Product> products)
        {
            List<Product> filteredProducts = new List<Product>();
            foreach (Product product in products)
            {
                if (product.Price >= minPrice && product.Price <= maxPrice)
                {
                    filteredProducts.Add(product);
                }
            }
            return filteredProducts;
        }
    }
}
