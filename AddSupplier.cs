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
    public partial class AddSupplier : Form
    {
        public AddSupplier()
        {
            InitializeComponent();
            DisplaySupplierData();

        }
        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void DisplaySupplierData()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string query = "SELECT * FROM Supplier";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string supplierName = textBox1.Text; // Assuming the textbox name for supplier name is textBoxSupplierName
            string contactInfo = textBox2.Text;   // Assuming the textbox name for contact info is textBoxContactInfo

            if (!string.IsNullOrWhiteSpace(supplierName) && !string.IsNullOrWhiteSpace(contactInfo))
            {
                SqlConnection connection = new SqlConnection(connectionString);
                try
                {
                    using (connection)
                    {
                        connection.Open();

                        string insertQuery = "INSERT INTO Supplier (Supplier_Name, Contact_Info) VALUES (@SupplierName, @ContactInfo)";

                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@SupplierName", supplierName);
                            command.Parameters.AddWithValue("@ContactInfo", contactInfo);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Supplier added successfully.");
                                // Refresh the DataGridView to display the newly added supplier
                                DisplaySupplierData();
                            }
                            else
                            {
                                MessageBox.Show("Failed to add supplier.");
                            }
                        }
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
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter both supplier name and contact info.");
            }

            InventoryOPT inventoryOPT = new InventoryOPT();
            inventoryOPT.Show();
            Dispose();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
