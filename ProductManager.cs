using System;
using System.Data.SqlClient;

namespace ProductManagementApp
{
    // Singleton pentru gestionarea datelor despre produse
    // o singura instanta a clasei ProductManager este creata și returnata de metoda statica Instance
    public class ProductManager
    {
        private static ProductManager instance;
        
        // Conexiunea cu baza de date
        readonly private string connectionString = "Data Source=Teodora;Initial Catalog=ProductManagementDB; Integrated Security=True";
        
        private ProductManager()
        {
        }

        public static ProductManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductManager();
                }
                return instance;
            }
        }

        public void AddProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO [dbo].[Products] (Name, Price) VALUES (@Name, @Price)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);

                try
                {
                    connection.Open();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Eroare conexiune: " + ex.Message);
                }
                command.ExecuteNonQuery();
            }
        }

        public void RemoveProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM [dbo].[Products] WHERE Name = @Name"; // Modificăm criteriul de ștergere
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", product.Name);

                try
                {
                    connection.Open();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Eroare conexiune: " + ex.Message);
                }
                command.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(string productName, Product newProduct)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE [dbo].[Products] SET Name = @NewName, Price = @NewPrice WHERE Name = @ProductName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NewName", newProduct.Name);
                command.Parameters.AddWithValue("@NewPrice", newProduct.Price);
                command.Parameters.AddWithValue("@ProductName", productName);

                try
                {
                    connection.Open();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Eroare conexiune: " + ex.Message);
                }
                command.ExecuteNonQuery();
            }
        }

        public double GetProductPrice(string productName)
        {
            double price = 0.0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Price FROM [dbo].[Products] WHERE Name = @ProductName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductName", productName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    price = Convert.ToDouble(reader["Price"]);
                }

                reader.Close();
            }

            return price;
        }

    }
}