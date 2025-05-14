using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManagement;

namespace ProductManagementApp.Tests
{
    [TestClass]
    public class ProductManagerTests
    {
        private ProductManager manager;

        private Dictionary<string, bool> testResults;

        [TestInitialize]
        public void TestInitialize()
        {
            manager = ProductManager.Instance;

            testResults = new Dictionary<string, bool>();
        }

        [TestMethod]
        public void TestAddProduct()
        {
            // Arrange
            Product product = new Product("TestProdus", 3.99);

            // Act
            manager.AddProduct(product);

            // Assert
            double actualPrice = manager.GetProductPrice("TestProdus");
            bool addProductTestResult = (product.Price == actualPrice);

            testResults.Add("TestAddProduct", addProductTestResult);
            Assert.AreEqual(product.Price, actualPrice);
        }

        [TestMethod]
        public void TestRemoveProduct()
        {
            /// Arrange
            Product product = new Product("EliminareProdus", 3.99);
            manager.AddProduct(product);

            // Act
            manager.RemoveProduct(product);

            // Assert
            double actualPrice = manager.GetProductPrice("EliminareProdus");
            bool removeProductTestResult = (actualPrice == 0.0);
            
            testResults.Add("TestRemoveProduct", removeProductTestResult);
            Assert.AreEqual(0.0, actualPrice);
        }

        [TestMethod]
        public void TestUpdateProduct()
        {
            // Arrange
            Product initialProduct = new Product("ProdusInitial", 5.99);
            manager.AddProduct(initialProduct);

            // Act
            Product updatedProduct = new Product("ProdusActualizat", 2.99);
            manager.UpdateProduct("ProdusInitial", updatedProduct);

            // Assert
            double actualPrice = manager.GetProductPrice("ProdusActualizat");
            bool updateProductTestResult = (updatedProduct.Price == actualPrice);
            
            testResults.Add("TestUpdateProduct", updateProductTestResult);
            Assert.AreEqual(updatedProduct.Price, actualPrice);
        }

        [TestMethod]
        public void TestApplyDiscountToProduct()
        {
            // Arrange
            Product product = new Product("TestProdus", 100); // Pret original: 100
            IPriceStrategy discountStrategy = new DiscountPriceStrategy(10); // Reducere de 10%

            // Act
            product.ApplyPriceStrategy(discountStrategy);

            // Assert
            double finalPrice = product.GetFinalPrice();
            bool applyDiscountTestResult = (finalPrice == 90);
            testResults.Add("TestApplyDiscountToProduct", applyDiscountTestResult);
            Assert.AreEqual(90, finalPrice);
        }

        [TestMethod]
        public void TestApplyTaxToProduct()
        {
            // Arrange
            Product product = new Product("TestProdus", 100); // Pret original: 100
            IPriceStrategy taxStrategy = new TaxPriceStrategy(20); // Taxa de 20%

            // Act
            product.ApplyPriceStrategy(taxStrategy);

            // Assert
            double finalPrice = product.GetFinalPrice();
            bool applyTaxTestResult = (finalPrice == 120);
            testResults.Add("TestApplyTaxToProduct", applyTaxTestResult);
            Assert.AreEqual(120, finalPrice);
        }


        public Dictionary<string, bool> GetTestResults()
        {
            return testResults;
        }
    }
}
