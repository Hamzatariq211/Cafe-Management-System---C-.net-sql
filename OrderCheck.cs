using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public partial class OrderCheck : Form
    {
        
        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";
        private int userid;
        public OrderCheck(int userid)
        {
            InitializeComponent();
            LoadDataFromViewOrder();
            DisplayTotalPriceSum();
            this.userid = userid;
        }

        private void DisplayTotalPriceSum()
        {
            string query = "SELECT SUM(Total_Price) FROM ViewOrder"; // Query to calculate the sum of Total_Price

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            decimal totalPriceSum = Convert.ToDecimal(result);
                           
                            labelTotalPriceSum.Text = totalPriceSum.ToString();

                      //      PaymentOptions Po=new PaymentOptions();
                        //    Po.B = labelTotalPriceSum.Text;




                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }




        private void LoadDataFromViewOrder()
        {
            string query = "SELECT * FROM ViewOrder"; // Query to select all data from ViewOrder

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    try
                    {
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PaymentOptions Po = new PaymentOptions(userid);
           
            Po.Show();
             Dispose();


        }
    }
}
