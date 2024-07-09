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
    public partial class Register : Form
    {


        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";


        public Register()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string firstName = firstname.Text;
            string lastName = lastname.Text;
            string username1 = username.Text;
            string email1 = email.Text;
            string password1 = password.Text;
            string userRole = userrole.SelectedItem.ToString(); // Assuming it's a dropdown/select box

            // SQL query to insert a new user into the Users table
            string query = "INSERT INTO Users (First_name, Last_Name, username, Email, Password, User_Role) " +
                           "VALUES (@FirstName, @LastName, @Username, @Email, @Password, @UserRole)";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                // Establish a connection to the database
                using (connection)
                {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand object
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the SQL query to prevent SQL injection
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Username", username1);
                        command.Parameters.AddWithValue("@Email", email1);
                        command.Parameters.AddWithValue("@Password", password1);
                        command.Parameters.AddWithValue("@UserRole", userRole);

                        // Execute the SQL command
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User registered successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to register user. Please try again.");
                        }
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

        private void button2_Click(object sender, EventArgs e)
        {
            Customer Cs = new Customer();


            Cs.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

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

        private void email_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void userrole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 F = new Form1();


            F.Show();
            this.Hide();
        }
    }
}
