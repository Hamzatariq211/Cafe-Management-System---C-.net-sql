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
    public partial class ModifyQuantity : Form
    {
        public int Itemid;
        public ModifyQuantity(int Itemid)
        {
            InitializeComponent();
            this.Itemid = Itemid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int newQuantity = Convert.ToInt32(textBox1.Text); // Assuming the textbox name is textBox1

                string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Update the quantity in the Inventory table
                    string updateInventoryQuery = "UPDATE Inventory SET Quantity = @NewQuantity WHERE ItemID = @Itemid";
                    using (SqlCommand updateInventoryCommand = new SqlCommand(updateInventoryQuery, connection))
                    {
                        updateInventoryCommand.Parameters.AddWithValue("@NewQuantity", newQuantity);
                        updateInventoryCommand.Parameters.AddWithValue("@Itemid", Itemid);
                        updateInventoryCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Quantity updated successfully.");
                    InventoryOPT inventoryOPT = new InventoryOPT();
                    inventoryOPT.Show();
                    Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
