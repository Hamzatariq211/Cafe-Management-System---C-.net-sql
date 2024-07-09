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
    public partial class InventoryOPT : Form
    {
        public InventoryOPT()
        {
            InitializeComponent();
            DisplayInventoryData();


        }
        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";
        SqlConnection connection;


        private void DisplayInventoryData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Inventory";

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









        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddItem addItem = new AddItem();
            addItem.Show();
            Dispose();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = dataGridView1.SelectedRows[0].Index;

                    int inventoryID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["Inventory_ID"].Value);
                    int itemID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["ItemID"].Value);

                    // Confirmation dialog box
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        connection.Open();

                        // Delete from Inventory table
                        string deleteInventoryQuery = "DELETE FROM Inventory WHERE Inventory_ID = @InventoryID";

                        using (SqlCommand deleteInventoryCommand = new SqlCommand(deleteInventoryQuery, connection))
                        {
                            deleteInventoryCommand.Parameters.AddWithValue("@InventoryID", inventoryID);
                            deleteInventoryCommand.ExecuteNonQuery();
                        }

                        // Delete from Items table
                        string deleteItemQuery = "DELETE FROM Items WHERE Item_ID = @ItemID";

                        using (SqlCommand deleteItemCommand = new SqlCommand(deleteItemQuery, connection))
                        {
                            deleteItemCommand.Parameters.AddWithValue("@ItemID", itemID);
                            deleteItemCommand.ExecuteNonQuery();
                        }

                         DisplayInventoryData();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.");
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


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                    int itemID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["ItemID"].Value);

                    ModifyPrice MP = new ModifyPrice(itemID); // Pass itemID to ModifyPrice form
                    MP.Show();
                    Dispose();

                }
                else
                {
                    MessageBox.Show("Please select a row.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            DisplayInventoryData();

        }




        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                    int itemID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["ItemID"].Value);

                    ModifyQuantity MQ = new ModifyQuantity(itemID); // Pass itemID to ModifyPrice form
                    MQ.Show();
                    Dispose();

                }
                else
                {
                    MessageBox.Show("Please select a row.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            DisplayInventoryData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddSupplier addSupplier = new AddSupplier();    
            addSupplier.Show();
            Dispose();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DeleteSupplier deleteSupplier = new DeleteSupplier();
            deleteSupplier.Show();
            Dispose();
        }
    }
}
