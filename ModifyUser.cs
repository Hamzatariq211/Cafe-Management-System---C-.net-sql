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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project
{
    public partial class ModifyUser : Form
    {
        public string ro;

        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";
        

        public int UserID;
       public string firstname;
        public string lastname;
        public string username;
        public string email;
        public string password;

        

        public ModifyUser(string ro, int UserID, string firstname, string lastname, string username, string email, string password)
        {
            InitializeComponent();
            this.ro = ro;
            this.UserID = UserID;
            this.firstname = firstname;
            this.lastname = lastname;   
            this.username = username;   
            this.email = email; 
            this.password = password;

           label1.Text = "First Name: " + firstname;
            label2.Text = "Last Name: " + lastname;
            label3.Text = "Username: " + username;
            label4.Text = "Email: " + email;
            label5.Text = "Password: " + password;


        }
        








        private void ModifyUser_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            // Get updated information from textboxes
            string updatedFirstName = fn.Text;
            string updatedLastName = textBox2.Text;
            string updatedUsername = textBox3.Text;
            string updatedEmail = textBox4.Text;
            string updatedPassword = textBox5.Text;

            try
            {
                using (connection)
                {
                    connection.Open();

                    // Update the selected user in the database
                    string updateQuery = "UPDATE Users SET ";
                    bool needComma = false;

                    if (!string.IsNullOrWhiteSpace(updatedFirstName))
                    {
                        updateQuery += "First_name = @FirstName";
                        needComma = true;
                    }

                    if (!string.IsNullOrWhiteSpace(updatedLastName))
                    {
                        if (needComma)
                        {
                            updateQuery += ", ";
                        }
                        updateQuery += "Last_Name = @LastName";
                        needComma = true;
                    }

                    if (!string.IsNullOrWhiteSpace(updatedUsername))
                    {
                        if (needComma)
                        {
                            updateQuery += ", ";
                        }
                        updateQuery += "username = @Username";
                        needComma = true;
                    }

                    if (!string.IsNullOrWhiteSpace(updatedEmail))
                    {
                        if (needComma)
                        {
                            updateQuery += ", ";
                        }
                        updateQuery += "Email = @Email";
                        needComma = true;
                    }

                    if (!string.IsNullOrWhiteSpace(updatedPassword))
                    {
                        if (needComma)
                        {
                            updateQuery += ", ";
                        }
                        updateQuery += "Password = @Password";
                    }


                    updateQuery += " WHERE User_ID = @UserID";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        if (!string.IsNullOrWhiteSpace(updatedFirstName))
                        {
                            command.Parameters.AddWithValue("@FirstName", updatedFirstName);
                        }

                        if (!string.IsNullOrWhiteSpace(updatedLastName))
                        {
                            command.Parameters.AddWithValue("@LastName", updatedLastName);
                        }

                        if (!string.IsNullOrWhiteSpace(updatedUsername))
                        {
                            command.Parameters.AddWithValue("@Username", updatedUsername);
                        }

                        if (!string.IsNullOrWhiteSpace(updatedEmail))
                        {
                            command.Parameters.AddWithValue("@Email", updatedEmail);
                        }

                        if (!string.IsNullOrWhiteSpace(updatedPassword))
                        {
                            command.Parameters.AddWithValue("@Password", updatedPassword);
                        }

                        command.Parameters.AddWithValue("@UserID", UserID);

                        command.ExecuteNonQuery();

                        // Notify the user about the successful update
                        UserManagement userManagement = new UserManagement(ro);
                        userManagement.Show();
                        Dispose();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
