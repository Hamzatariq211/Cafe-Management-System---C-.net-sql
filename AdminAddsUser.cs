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
    public partial class AdminAddsUser : Form
    {
        private string r;
        
        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";

        public AdminAddsUser(string r)
        {
            InitializeComponent();
            this.r = r;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get the user details from text boxes
            string firstName1 = fn.Text;
            string lastName1 = ln.Text;
            string username1 = username.Text;
            string email1 = email.Text;
            string password1 = p.Text;

            string userRole1 = "";

               userRole1 = r;
  

            // Validate if any required field is empty
            if (string.IsNullOrWhiteSpace(firstName1) || string.IsNullOrWhiteSpace(lastName1) ||
                string.IsNullOrWhiteSpace(username1) || string.IsNullOrWhiteSpace(email1) ||
                string.IsNullOrWhiteSpace(password1) || string.IsNullOrWhiteSpace(userRole1))
            {
                MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Insert the user details into the database
            InsertNewUser(firstName1, lastName1, username1, email1, password1, userRole1);

            // Close the form after adding the user
            this.Close();
        }


        private void InsertNewUser(string firstName, string lastName, string username, string email, string password, string userRole)
        {
            // Your database connection string
           

            // Your SQL query to insert the user
            string query = "INSERT INTO Users (First_name, Last_Name, username, Email, Password, User_Role) " +
                           "VALUES (@FirstName, @LastName, @Username, @Email, @Password, @UserRole)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@UserRole", userRole);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        UserManagement userControl = new UserManagement(r);
                        userControl.Show();
                        Dispose();


                    }
                    else
                    {
                        MessageBox.Show("Failed to add user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void firstname_TextChanged(object sender, EventArgs e)
        {

        }

        private void lastname_TextChanged(object sender, EventArgs e)
        {

        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void email_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
