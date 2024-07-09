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
    public partial class Audit : Form
    {
        decimal sumQuantityTimesPrice = 0;
        decimal sumTotalPrice = 0;
        public Audit()
        {
            InitializeComponent();
            LoadData();
            CalculateSums();
        }
        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";
        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Order_ID, User_ID, Item_ID, Item_Name, Quantity, Price, Total_Price, Order_Time, Completion_Time, Status FROM OrderHistory";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Add a column "Supplier buying price" to the DataTable
                        dataTable.Columns.Add("Supplier buying price", typeof(decimal));

                        // Calculate "Supplier buying price" based on conditions
                        foreach (DataRow row in dataTable.Rows)
                        {
                            decimal price = Convert.ToDecimal(row["Price"]);

                            if (price < 5)
                            {
                                row["Supplier buying price"] = price - 1;
                            }
                            else if (price >= 5)
                            {
                                row["Supplier buying price"] = price - 3;
                            }
                        }

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void CalculateSums()
        {
            sumQuantityTimesPrice = 0;
            sumTotalPrice = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                decimal supplierBuyingPrice = Convert.ToDecimal(row.Cells["Supplier buying price"].Value);
                decimal totalPrice = Convert.ToDecimal(row.Cells["Total_Price"].Value);

                sumQuantityTimesPrice += quantity * supplierBuyingPrice;
                sumTotalPrice += totalPrice;
            }

            label3.Text =  sumQuantityTimesPrice.ToString();
            label4.Text =  sumTotalPrice.ToString();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Audit_Load(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            label6.Text = (sumTotalPrice - sumQuantityTimesPrice).ToString();

        }
    }
}
