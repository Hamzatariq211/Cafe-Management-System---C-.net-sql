using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Project
{
    public partial class Customer : Form
    {
        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";


        public Customer()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            string inputUsername = u.Text;
            string inputPassword = p.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT User_ID FROM Users WHERE username = @username AND Password = @password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", inputUsername);
                    command.Parameters.AddWithValue("@password", inputPassword);

                    object result = command.ExecuteScalar();
                    if (result != null) // Check if user exists
                    {
                        int log = (int)result;

                        CustomerBuyingFirst menuWork = new CustomerBuyingFirst(); // Pass the logged-in user ID
                        menuWork.loggedInUserID = log;
                        menuWork.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password");
                    }
                }
                connection.Close();

            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Register Rs = new Register();


            Rs.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 F = new Form1();


            F.Show();
            this.Hide();
        }
    }


}



