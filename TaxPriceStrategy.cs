using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement
{
    // Implementare a unei strategii pentru aplicarea taxei
    public class TaxPriceStrategy : IPriceStrategy
    {
        private double taxPercentage;

        public TaxPriceStrategy(double taxPercentage)
        {
            this.taxPercentage = taxPercentage;
        }

        public double ApplyPrice(double originalPrice)
        {
            double taxAmount = originalPrice * (taxPercentage / 100);
            return originalPrice + taxAmount;
        }
    }
}
