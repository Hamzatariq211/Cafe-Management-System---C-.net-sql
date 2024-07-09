using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Project
{
    public partial class UserManagement : Form
    {
        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";
        private string role; 

        public UserManagement(string role)
        {
            this.role = role;
            InitializeComponent();
            PopulateDataGridView();
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PopulateDataGridView()
        {
            string query="";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                
                using (connection)
                {
                    connection.Open();



                        query = $"SELECT User_ID, First_name, Last_Name, username, Email,Password ,User_Role FROM Users where User_Role='{role}'";
                   
                
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Dispose(); 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int userID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["User_ID"].Value);

                // Prompt the user for confirmation before deleting
                DialogResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DeleteUser(userID);
                    PopulateDataGridView(); // Refresh the DataGridView after deletion
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteUser(int userID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string query = "DELETE FROM Users WHERE User_ID = @UserID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserID", userID);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("User deletion failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            AdminAddsUser AAU = new AdminAddsUser(role);
            AAU.Show();
            Dispose();



        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int userID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["User_ID"].Value);
                string firstName = dataGridView1.Rows[selectedRowIndex].Cells["First_name"].Value.ToString();
                string lastName = dataGridView1.Rows[selectedRowIndex].Cells["Last_Name"].Value.ToString();
                string username = dataGridView1.Rows[selectedRowIndex].Cells["username"].Value.ToString();
                string email = dataGridView1.Rows[selectedRowIndex].Cells["Email"].Value.ToString();
                string password = dataGridView1.Rows[selectedRowIndex].Cells["Password"].Value.ToString(); ;

                ModifyUser Mu = new ModifyUser(role,userID, firstName, lastName, username, email, password);

  
                Mu.Show();
                Dispose();
            }
            else
            {
                MessageBox.Show("Please select a user to modify.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserManagement_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
