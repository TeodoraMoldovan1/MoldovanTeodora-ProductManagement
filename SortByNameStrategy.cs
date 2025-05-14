using ProductManagementApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement
{
    public class SortByNameStrategy : IStrategy
    {
        public void ExecuteStrategy(List<Product> products)
        {
            products.Sort((p1, p2) => p1.Name.CompareTo(p2.Name));
        }
    }
}
