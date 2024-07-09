using Stripe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class CustomerBuyingFirst : Form
    {

        public int loggedInUserID { get; set; }

        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=500;Connection Timeout=60;";
        public CustomerBuyingFirst()
        {
            InitializeComponent();
            string selectedCategory = "";

            LoadMenu(selectedCategory);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void LoadMenu(string selectedCategory)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string query = "SELECT Item_ID, Item_Name, Category, Price, Description FROM Items WHERE Category = @Category";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Category", selectedCategory);
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataGridView to the DataTable
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Dispose();
            }
        }

        private void Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = Category.SelectedItem.ToString();
            LoadMenu(selectedCategory);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

                int itemID = Convert.ToInt32(selectedRow.Cells["Item_ID"].Value);
                string itemName = Convert.ToString(selectedRow.Cells["Item_Name"].Value);
                decimal itemPrice = Convert.ToDecimal(selectedRow.Cells["Price"].Value);

                if (int.TryParse(quantity1.Text, out int quantity))
                {
                    using (connection)
                    {
                        connection.Open();
                        SqlTransaction transaction = connection.BeginTransaction();

                        try
                        {
                            // Check if sufficient quantity is available in Inventory
                            string checkQuantityQuery = "SELECT Quantity FROM Inventory WHERE ItemID = @ItemID";

                            SqlCommand checkQuantityCommand = new SqlCommand(checkQuantityQuery, connection, transaction);
                            checkQuantityCommand.Parameters.AddWithValue("@ItemID", itemID);

                            int availableQuantity = Convert.ToInt32(checkQuantityCommand.ExecuteScalar());

                            if (availableQuantity >= quantity)
                            {
                                // Calculate total price
                                decimal totalPrice = itemPrice * quantity;

                                // Insert into OrderItem table
                                string insertQuery = @"INSERT INTO OrderItem (User_ID, Item_ID, Item_Name, Quantity, Price, Total_Price) 
                                              VALUES (@UserID, @ItemID, @ItemName, @Quantity, @Price, @TotalPrice)";

                                SqlCommand insertCommand = new SqlCommand(insertQuery, connection, transaction);
                                insertCommand.Parameters.AddWithValue("@UserID", loggedInUserID);
                                insertCommand.Parameters.AddWithValue("@ItemID", itemID);
                                insertCommand.Parameters.AddWithValue("@ItemName", itemName);
                                insertCommand.Parameters.AddWithValue("@Quantity", quantity);
                                insertCommand.Parameters.AddWithValue("@Price", itemPrice);
                                insertCommand.Parameters.AddWithValue("@TotalPrice", totalPrice);

                                int rowsAffected = insertCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    // Reduce the quantity in the Inventory table
                                    string updateInventoryQuery = "UPDATE Inventory SET Quantity = Quantity - @Quantity WHERE ItemID = @ItemID";
                                    SqlCommand updateInventoryCommand = new SqlCommand(updateInventoryQuery, connection, transaction);
                                    updateInventoryCommand.Parameters.AddWithValue("@Quantity", quantity);
                                    updateInventoryCommand.Parameters.AddWithValue("@ItemID", itemID);
                                    updateInventoryCommand.ExecuteNonQuery();

                                    transaction.Commit();
                                    MessageBox.Show("Item added to cart successfully!");

                                    
                                }
                                else
                                {
                                    MessageBox.Show("Failed to add item to cart.");
                                    transaction.Rollback();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Insufficient quantity available in inventory.");
                                transaction.Rollback();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                            transaction.Rollback();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid quantity.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Dispose();
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    

                    string sqlQuery = @"
                INSERT INTO ViewOrder (Order_ID, Name, Quantity, Price, Total_Price)
                SELECT 
                    O.Order_ID,
                    CONCAT(U.First_name, ' ', U.Last_Name) AS Name,
                    O.Quantity,
                    I.Price AS ItemPrice,
                    O.Total_Price
                FROM OrderItem O
                JOIN Users U ON O.User_ID = U.User_ID
                JOIN Items I ON O.Item_ID = I.Item_ID;
            ";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        // command.Connection = PaymentMethodConfigurationP24;
                        //command.CommandTimeout = 100;

                        //command.CommandType = CommandType.Text;
                        SqlConnection.ClearAllPools();
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            OrderCheck OC = new OrderCheck(loggedInUserID);
                            OC.Show();

                            Dispose();


                        }
                        else
                        {
                            MessageBox.Show("Failed to insert data into ViewOrder.");
                        }

                        connection.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            //finally
            //{
            //    if (connection.State == ConnectionState.Open)
            //    {
            //        connection.Close();
            //    }
            //    connection.Dispose(); 
            //}



















            //OrderCheck OC = new OrderCheck();
            // OC.Show();
            // Dispose();






        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CustomerBuyingFirst_Load(object sender, EventArgs e)
        {

        }
    }
}