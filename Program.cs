using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductManagement
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Configuration;
//using System.Data.SqlClient;

//namespace ProductManagement
//{
//    internal static class Program
//    {
//        [STAThread]
//        static void Main()
//        {
//            // 🔍 Test conexiune la baza de date înainte de a porni aplicația
//            try
//            {
//                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

//                using (SqlConnection connection = new SqlConnection(connectionString))
//                {
//                    connection.Open();
//                    MessageBox.Show("✅ Conexiune reușită la baza de date!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//            }
//            catch (SqlException ex)
//            {
//                MessageBox.Show("❌ Eroare SQL:\n" + ex.Message, "Eroare conexiune", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return; // Nu continuăm dacă nu ne putem conecta
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("❌ Altă eroare:\n" + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            // Dacă conexiunea a reușit, pornim aplicația
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);
//            Application.Run(new MainForm());
//        }
//    }
//}

