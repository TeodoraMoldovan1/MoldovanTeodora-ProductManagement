using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement
{
    // Implementare a unei strategii pentru aplicarea reducerii
    public class DiscountPriceStrategy : IPriceStrategy
    {
        private double discountPercentage;

        public DiscountPriceStrategy(double discountPercentage)
        {
            this.discountPercentage = discountPercentage;
        }

        public double ApplyPrice(double originalPrice)
        {
            double discountAmount = originalPrice * (discountPercentage / 100);
            return originalPrice - discountAmount;
        }
    }
}
