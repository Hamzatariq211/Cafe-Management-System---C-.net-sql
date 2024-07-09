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

namespace Project
{
    public partial class AdminRegisteration : Form
    {

        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";
        public AdminRegisteration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string username1 = username.Text;
            string password1 = username.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the provided credentials exist in the Admin table
                string query = "SELECT COUNT(*) FROM Admin WHERE Username = @Username AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username1);
                command.Parameters.AddWithValue("@Password", password1);

                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    // Admin login successful
                    AdminOPT Admin=new AdminOPT();
                    Admin.Show();
                    Dispose();

                   
                }
                else
                {
                    // Invalid credentials
                    MessageBox.Show("Invalid username or password!");
                }


                connection.Close();
            }




        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 F = new Form1();


            F.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
