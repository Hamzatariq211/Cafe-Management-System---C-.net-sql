using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class EmployeeWork : Form
    {
        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";


        public EmployeeWork()
        {
            InitializeComponent();
            DisplayItemsWithLowQuantity();
        }


        private void DisplayItemsWithLowQuantity()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
            

                using (connection)
                {
                    connection.Open();

                    string query = @"SELECT I.Item_ID, I.Item_Name, I.Category, I.Price, I.Description, ISNULL(SUM(IV.Quantity), 0) AS Quantity
                             FROM Items I
                             LEFT JOIN Inventory IV ON I.Item_ID = IV.ItemID
                             GROUP BY I.Item_ID, I.Item_Name, I.Category, I.Price, I.Description
                             HAVING ISNULL(SUM(IV.Quantity), 0) < 15";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                // Close the connection in the finally block
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TotalSales totalSales = new TotalSales();
            totalSales.Show();
            Dispose();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoyalCustomers loyalCustomers = new LoyalCustomers();
            loyalCustomers.Show();
            Dispose();
        }
    }
}
