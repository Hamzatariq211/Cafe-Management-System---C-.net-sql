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
    public partial class CashierLogin : Form
    {
        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";
        public CashierLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string enteredUsername = username.Text;
            string enteredPassword = password.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the provided credentials exist in the Users table with User_Role as Cashier
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password AND User_Role = 'Cashier'";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", enteredUsername);
                command.Parameters.AddWithValue("@Password", enteredPassword);

                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    // Cashier login successful
                    MessageBox.Show("Cashier login successful!");
                    // Proceed with further actions after successful login
                    CashierOPT cashierOPT = new CashierOPT();
                    cashierOPT.Show();
                    Dispose();
                }
                else
                {
                    // Invalid credentials
                    MessageBox.Show("Invalid username or password or user role!");
                }

                connection.Close();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Form1 F = new Form1();


            F.Show();
            this.Hide();
        }
    }
}

