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
    public partial class LoyalCustomers : Form
    {
        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";

        public LoyalCustomers()
        {
            InitializeComponent();
            DisplayFrequentUsers();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EmployeeWork employeeWork = new EmployeeWork();
            employeeWork.Show();
            Dispose();

        }

        private void DisplayFrequentUsers()
        {
            try
            {
              
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT U.User_ID, U.First_Name, U.Last_Name, U.Email, U.User_Role,
                    (SELECT COUNT(*) FROM OrderHistory OH WHERE OH.User_ID = U.User_ID) AS OrderCount
                FROM Users U
                ORDER BY OrderCount DESC";

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


        private void LoyalCustomers_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = dataGridView1.SelectedRows[0].Index;

                    int userID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["User_ID"].Value);
                    string firstName = dataGridView1.Rows[selectedRowIndex].Cells["First_Name"].Value.ToString();
                    string lastName = dataGridView1.Rows[selectedRowIndex].Cells["Last_Name"].Value.ToString();
                    

                    connection.Open();

                    string insertQuery = "INSERT INTO LoyalCustomers (User_ID, First_Name, Last_Name) " +
                                         "VALUES (@UserID, @FirstName, @LastName)";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                       

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Selected user added to Loyal Customers.");
                            // Refresh DataGridView or perform any other required action
                        }
                        else
                        {
                            MessageBox.Show("Failed to add user to Loyal Customers.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a user from the DataGridView.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

    }
}
