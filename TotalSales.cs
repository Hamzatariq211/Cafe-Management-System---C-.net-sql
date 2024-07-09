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
    public partial class TotalSales : Form
    {
        public TotalSales()
        {
            InitializeComponent();
            DisplayInventoryOrders();
            DisplayMostFrequentItem();
        }

        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";
        private void DisplayInventoryOrders()
        {
            try
            {
               

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT I.Item_ID, I.Item_Name, I.Category, I.Price, I.Description,
                    OH.Order_ID, OH.User_ID, OH.Item_ID AS OrderItemID,
                    OH.Item_Name AS OrderedItemName, OH.Quantity AS OrderedQuantity,
                    OH.Price AS OrderedPrice, OH.Total_Price AS TotalPrice,
                    OH.Order_Time, OH.Completion_Time, OH.Status
                FROM Items I
                INNER JOIN Inventory IV ON I.Item_ID = IV.ItemID
                INNER JOIN OrderHistory OH ON I.Item_ID = OH.Item_ID";

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
        }


        private void DisplayMostFrequentItem()
        {
            try
            {
               
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT TOP 1 I.Item_ID, I.Item_Name, I.Category, I.Price, I.Description,
                    COUNT(OH.Item_ID) AS OrderCount
                FROM Items I
                INNER JOIN OrderHistory OH ON I.Item_ID = OH.Item_ID
                GROUP BY I.Item_ID, I.Item_Name, I.Category, I.Price, I.Description
                ORDER BY COUNT(OH.Item_ID) DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView2.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            EmployeeWork employeeWork = new EmployeeWork();
            employeeWork.Show();
            Dispose();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Audit audit = new Audit();
            audit.Show();
            Dispose();

        }
    }
}
