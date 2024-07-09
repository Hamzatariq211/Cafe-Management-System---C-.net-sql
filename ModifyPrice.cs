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
    public partial class ModifyPrice : Form
    {
        public int Itemid;
        public ModifyPrice(int Itemid)
        {
            InitializeComponent();
            this.Itemid = Itemid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                decimal newPrice = decimal.Parse(textBox1.Text);

                using (connection)
                {
                    connection.Open();

                    // Update the price in the Items table
                    string updateItemQuery = "UPDATE Items SET Price = @newPrice WHERE Item_ID = @Itemid";
                    using (SqlCommand updateItemCommand = new SqlCommand(updateItemQuery, connection))
                    {
                        updateItemCommand.Parameters.AddWithValue("@NewPrice", newPrice);
                        updateItemCommand.Parameters.AddWithValue("@Itemid", Itemid);
                        updateItemCommand.ExecuteNonQuery();
                    }

                    // Update the price in the Inventory table
                    string updateInventoryQuery = "UPDATE Inventory SET Price = @newPrice WHERE ItemID = @Itemid";
                    using (SqlCommand updateInventoryCommand = new SqlCommand(updateInventoryQuery, connection))
                    {
                        updateInventoryCommand.Parameters.AddWithValue("@newPrice", newPrice);
                        updateInventoryCommand.Parameters.AddWithValue("@Itemid", Itemid);
                        updateInventoryCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Price updated successfully.");

                    InventoryOPT inventoryOPT = new InventoryOPT();
                    inventoryOPT.Show();

                   Dispose();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
