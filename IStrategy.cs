using ProductManagementApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement
{
    public interface IStrategy
    {
        void ExecuteStrategy(List<Product> products);
    }
}
