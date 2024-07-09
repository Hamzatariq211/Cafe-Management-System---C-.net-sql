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
    public partial class AddItem : Form
    {
        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";

        public AddItem()
        {
            InitializeComponent();
            DisplaySuppliers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Assuming your textboxes for item details are named textBoxItemName, textBoxCategory, textBoxPrice, textBoxDescription
            string itemName = textBoxItemName.Text;
            string category = textBoxCategory.Text;
            decimal price = Convert.ToDecimal(textBoxPrice.Text);
            string description = textBoxDescription.Text;

            // Get selected supplier details from DataGridView
            int supplierID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Supplier_ID"].Value);
            string supplierName = dataGridView1.SelectedRows[0].Cells["Supplier_Name"].Value.ToString();

            // Insert item into Items table
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertItemQuery = "INSERT INTO Items (Item_Name, Category, Price, Description) VALUES (@ItemName, @Category, @Price, @Description); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(insertItemQuery, connection))
                {
                    command.Parameters.AddWithValue("@ItemName", itemName);
                    command.Parameters.AddWithValue("@Category", category);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Description", description);

                    int itemID = Convert.ToInt32(command.ExecuteScalar());

                    // Insert item into Inventory table with supplier and quantity
                    string insertInventoryQuery = "INSERT INTO Inventory (ItemID, SupplierID, Supplier_Name, Item_Name, Price, Category, Quantity) VALUES (@ItemID, @SupplierID, @SupplierName, @ItemName, @Price, @Category, @Quantity)";

                    using (SqlCommand insertInventoryCommand = new SqlCommand(insertInventoryQuery, connection))
                    {
                        insertInventoryCommand.Parameters.AddWithValue("@ItemID", itemID);
                        insertInventoryCommand.Parameters.AddWithValue("@SupplierID", supplierID);
                        insertInventoryCommand.Parameters.AddWithValue("@SupplierName", supplierName);
                        insertInventoryCommand.Parameters.AddWithValue("@ItemName", itemName);
                        insertInventoryCommand.Parameters.AddWithValue("@Price", price);
                        insertInventoryCommand.Parameters.AddWithValue("@Category", category);
                        insertInventoryCommand.Parameters.AddWithValue("@Quantity", Convert.ToInt32(textBoxQuantity.Text)); // Assuming quantity input textbox is named textBoxQuantity

                        insertInventoryCommand.ExecuteNonQuery();

                        InventoryOPT inventoryOPT = new InventoryOPT();
                        inventoryOPT.Show();
                        Dispose();


                    }
                }
            }
        }



            private void DisplaySuppliers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {




        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
