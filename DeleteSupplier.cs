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
    public partial class DeleteSupplier : Form
    {
        public DeleteSupplier()
        {
            InitializeComponent();
            DisplaySupplierData();
        }
        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";
        private void DisplaySupplierData()
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL query to select all data from the Supplier table
                    string query = "SELECT * FROM Supplier";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // Get the selected row index
                    int selectedRowIndex = dataGridView1.SelectedRows[0].Index;

                    // Get the value from a specific column in the selected row
                    string supplierID = dataGridView1.Rows[selectedRowIndex].Cells["Supplier_ID"].Value.ToString();

                    // Your connection string



                    // Your SQL query to delete the row from the table
                    string deleteQuery = "DELETE FROM Supplier WHERE Supplier_ID = @SupplierID";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                        {
                            command.Parameters.AddWithValue("@SupplierID", supplierID);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Row deleted successfully.");


                                InventoryOPT inventoryOPT = new InventoryOPT();
                                inventoryOPT.Show();
                                Dispose();

                            }
                            else
                            {
                                MessageBox.Show("Failed to delete row.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }


    }
}
