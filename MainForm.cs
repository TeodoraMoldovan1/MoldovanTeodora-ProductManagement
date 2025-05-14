using ProductManagementApp;
using ProductManagementApp.Tests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProductManagement
{
    public partial class MainForm : Form
    {
        readonly private SqlConnection connection; // conexiune la baza de date
        private List<Product> allProducts;
        public MainForm()
        {
            InitializeComponent();
            string connectionString = "Data Source=Teodora;Initial Catalog=ProductManagementDB;Integrated Security=True;TrustServerCertificate=True"; // Definim șirul de conexiune
            connection = new SqlConnection(connectionString); // Initializare conexiune
            allProducts = GetProducts();
            RefreshProductList(); ;
        }

        // Afisare produse in listbox
        private void RefreshProductList(List<Product> productList = null)
        {
            productsListBox.Items.Clear();
            List<Product> productsToDisplay = productList ?? GetProducts(); // Daca nu este furnizata o lista de produse, se obtin toate produsele din baza de date
            foreach (Product product in productsToDisplay)
            {
                productsListBox.Items.Add(product.Name + " - "  + product.Price + " RON");
            }
        }

        // Executare query
        private void ExecuteNonQuery(string query)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        // Obtinere produse din baza de date
        private List<Product> GetProducts(string query = "SELECT * FROM Products")
        {
            List<Product> products = new List<Product>();
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["Name"].ToString(); 
                    double price = Convert.ToDouble(reader["Price"]); 
                    products.Add(new Product(name, price));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close(); 
            }
            return products;
        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            string selectQuery = "SELECT * FROM Products ORDER BY Name";
            List<Product> sortedProducts = GetProducts(selectQuery);
            RefreshProductList(sortedProducts);

        }

        // Adaugare produse noi in baza de date
        private void AddProductButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(productNameTextBox.Text) || string.IsNullOrWhiteSpace(productPriceTextBox.Text))
            {
                MessageBox.Show("Completați toate câmpurile", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string name = productNameTextBox.Text;
                double price = Convert.ToDouble(productPriceTextBox.Text);
                string insertQuery = $"INSERT INTO Products (Name, Price) VALUES ('{name}', {price})";
                ExecuteNonQuery(insertQuery);

                RefreshProductList();

            }
            catch (FormatException)
            {
                MessageBox.Show("Prețul introdus nu este într-un format valid", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Stergere produs selectat din listbox
        private void DeleteProductButton_Click(object sender, EventArgs e)
        {
            string selectedProductString = productsListBox.SelectedItem.ToString();
            string selectedProductName = selectedProductString.Split('-')[0].Trim();
            if (productsListBox.SelectedItem != null)
            {
                string deleteQuery = $"DELETE FROM Products WHERE Name = '{selectedProductName}'";
                ExecuteNonQuery(deleteQuery);

                RefreshProductList();
            }
            else
            {
                MessageBox.Show("Selectați un produs pentru a-l șterge");
            }
        }

        // Modificare produs selectat
        private void ModifyProductButton_Click(object sender, EventArgs e)
        {
            if (productsListBox.SelectedItem != null)
            { 
                if (string.IsNullOrWhiteSpace(productNameTextBox.Text))
                {
                    MessageBox.Show("Introduceți un nume valid pentru produs", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string selectedProductString = productsListBox.SelectedItem.ToString();
                string selectedProductName = selectedProductString.Split('-')[0].Trim();

                if (string.IsNullOrWhiteSpace(productPriceTextBox.Text))
                {
                    MessageBox.Show("Introduceți un preț valid pentru produs", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double newProductPrice;
                if (!double.TryParse(productPriceTextBox.Text, out newProductPrice))
                {
                    MessageBox.Show("Introduceți un preț valid pentru produs", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                productsListBox.Items[productsListBox.SelectedIndex] = $"{productNameTextBox.Text} - {newProductPrice} RON";

                string updateQuery = $"UPDATE Products SET Name = '{productNameTextBox.Text}', Price = {newProductPrice} WHERE Name = '{selectedProductName}'";
                ExecuteNonQuery(updateQuery);

                RefreshProductList();
            }
            else
            {
                MessageBox.Show("Selectați un produs pentru a-l modifica", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void FilterByPriceButton_Click_1(object sender, EventArgs e)
        {
            double minPrice;
            double maxPrice;

            if (!double.TryParse(minPriceTextBox.Text, out minPrice) || !double.TryParse(maxPriceTextBox.Text, out maxPrice))
            {
                
                MessageBox.Show("Introduceți valori numerice valide pentru preț", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (minPrice <= maxPrice)
            {
                List<Product> filteredProducts = allProducts
                    .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                    .OrderBy(p => p.Price)
                    .ToList();

                RefreshProductList(filteredProducts);
            }
            else
            {
                MessageBox.Show("Prețul minim nu poate fi mai mare decât prețul maxim", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ResetFilterButton_Click_1(object sender, EventArgs e)
        {
            RefreshProductList(allProducts);
            minPriceTextBox.Clear();
            maxPriceTextBox.Clear();
        }

        private void ApplyDiscountButton_Click(object sender, EventArgs e)
        {
            IPriceStrategy discountStrategy = new DiscountPriceStrategy(10);

            Product product = GetCurrentProduct();
            product.ApplyPriceStrategy(discountStrategy);

            UpdatePriceLabel(product.GetFinalPrice());
        }

        private void ApplyTaxButton_Click(object sender, EventArgs e)
        {
            IPriceStrategy taxStrategy = new TaxPriceStrategy(10);

            Product product = GetCurrentProduct();
            product.ApplyPriceStrategy(taxStrategy);

            UpdatePriceLabel(product.GetFinalPrice());
        }

        private Product GetCurrentProduct()
        {
            if (productsListBox.SelectedItem != null)
            {
                string selectedProductText = productsListBox.SelectedItem.ToString();

                // Descompunere sir pentru a extrage numele si pretul
                string[] parts = selectedProductText.Split('-');

                // Verificare daca exista nume si pret
                if (parts.Length == 2)
                {
                    // Extragere si eliminare spatii
                    string name = parts[0].Trim();
                    string priceText = parts[1].Trim();

                    double price;
                    if (double.TryParse(priceText.Replace("RON", "").Trim(), out price))
                    {
                        return new Product(name, price);
                    }
                }
            }
            return new Product("", 0.0);
        }

        private void UpdatePriceLabel(double price)
        {
            PriceLabel.Text = $"Preț final: {price}";
        } 
    }
}
