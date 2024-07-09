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
    public partial class CashierInterface : Form
    {
        decimal totalSum;
        public CashierInterface()
        {
            InitializeComponent();
            LoadData();
            LoadDataGrid2();

        }
        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";

        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL query to select data from Items table and relevant data from Inventory table
                    string query = "SELECT I.Item_ID, I.Item_Name, I.Category, I.Price, I.Description, " +
                                   "IV.Quantity, IV.SupplierID, IV.Supplier_Name " +
                                   "FROM Items I " +
                                   "INNER JOIN Inventory IV ON I.Item_ID = IV.ItemID";

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
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();

        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    int itemId = Convert.ToInt32(selectedRow.Cells["Item_ID"].Value);
                    string itemName = selectedRow.Cells["Item_Name"].Value.ToString();
                    decimal price = Convert.ToDecimal(selectedRow.Cells["Price"].Value);
                    int quantity = Convert.ToInt32(quantity1.Value); // Get quantity from NumericUpDown control

                    // Get UserID from the TextBox
                    int userId;
                    if (!int.TryParse(textBox1.Text, out userId))
                    {
                        MessageBox.Show("Invalid User ID. Please enter a valid numeric value.");
                        return;
                    }

                    // Calculate total price based on quantity and item price
                    decimal totalPrice = quantity * price;

                    int orderId; // Variable to store the generated Order_ID

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Insert into OrderItem table and retrieve the generated Order_ID
                        string insertOrderItemQuery = "INSERT INTO OrderItem (User_ID, Item_ID, Item_Name, Quantity, Price, Total_Price) " +
                                                      "VALUES (@UserID, @ItemID, @ItemName, @Quantity, @Price, @TotalPrice); " +
                                                      "SELECT SCOPE_IDENTITY();";

                        using (SqlCommand insertOrderItemCommand = new SqlCommand(insertOrderItemQuery, connection))
                        {
                            insertOrderItemCommand.Parameters.AddWithValue("@UserID", userId);
                            insertOrderItemCommand.Parameters.AddWithValue("@ItemID", itemId);
                            insertOrderItemCommand.Parameters.AddWithValue("@ItemName", itemName);
                            insertOrderItemCommand.Parameters.AddWithValue("@Quantity", quantity);
                            insertOrderItemCommand.Parameters.AddWithValue("@Price", price);
                            insertOrderItemCommand.Parameters.AddWithValue("@TotalPrice", totalPrice);

                            // Execute scalar to get the generated Order_ID
                            orderId = Convert.ToInt32(insertOrderItemCommand.ExecuteScalar());
                        }

                        // Insert into ViewOrder table with the obtained Order_ID
                        string insertViewOrderQuery = "INSERT INTO ViewOrder (Order_ID, Name, Quantity, Price, Total_Price) " +
                                                      "VALUES (@OrderID, @Name, @Quantity, @Price, @TotalPrice)";

                        using (SqlCommand insertViewOrderCommand = new SqlCommand(insertViewOrderQuery, connection))
                        {
                            insertViewOrderCommand.Parameters.AddWithValue("@OrderID", orderId); // Use the obtained Order_ID
                            insertViewOrderCommand.Parameters.AddWithValue("@Name", itemName);
                            insertViewOrderCommand.Parameters.AddWithValue("@Quantity", quantity);
                            insertViewOrderCommand.Parameters.AddWithValue("@Price", price);
                            insertViewOrderCommand.Parameters.AddWithValue("@TotalPrice", totalPrice);

                            insertViewOrderCommand.ExecuteNonQuery();
                        }

                        // Update inventory by reducing the quantity of the selected item
                        string updateInventoryQuery = "UPDATE Inventory SET Quantity = Quantity - @Quantity WHERE ItemID = @ItemID";
                        using (SqlCommand updateInventoryCommand = new SqlCommand(updateInventoryQuery, connection))
                        {
                            updateInventoryCommand.Parameters.AddWithValue("@ItemID", itemId);
                            updateInventoryCommand.Parameters.AddWithValue("@Quantity", quantity);
                            updateInventoryCommand.ExecuteNonQuery();
                        }

                        MessageBox.Show("Item added to order successfully! Inventory quantity updated.");
                        LoadData();
                        LoadDataGrid2();

                    }
                }
                else
                {
                    MessageBox.Show("Please select an item from the grid.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void LoadDataGrid2()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT OI.Order_ID, OI.User_ID, VO.Name AS ViewOrder_Name, " +
                                   "OI.Item_ID, OI.Item_Name, OI.Quantity, OI.Price, OI.Total_Price, OI.Order_Time " +
                                   "FROM OrderItem OI " +
                                   "LEFT JOIN ViewOrder VO ON OI.Order_ID = VO.Order_ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Calculate the sum of Total_Price column
                        totalSum = 0;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            totalSum += Convert.ToDecimal(row["Total_Price"]);
                        }

                        // Display the total sum in a label or any desired way
                        label4.Text = totalSum.ToString(); 

                        // Bind the DataTable to dataGridView2
                        dataGridView2.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }















        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the input price from textBoxInputPrice
                if (decimal.TryParse(textBox2.Text, out decimal inputPrice))
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "SELECT SUM(Total_Price) AS TotalSum FROM OrderItem";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // ExecuteScalar retrieves a single value (total sum of Total_Price)
                            object result = command.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                decimal ts = Convert.ToDecimal(result);

                                // Calculate the difference
                                decimal priceDifference = inputPrice - ts;

                                // Display the price difference in labelPriceDifference
                                label7.Text =  priceDifference.ToString();
                            }
                            else
                            {
                                MessageBox.Show("No data found.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid price.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Copy data from OrderItem to OrderHistory and mark the status as 'Completed'
                    string copyDataQuery = @"
                    INSERT INTO OrderHistory (Order_ID, User_ID, Item_ID, Item_Name, Quantity, Price, Total_Price, Order_Time,Completion_Time, Status)
                    SELECT Order_ID, User_ID, Item_ID, Item_Name, Quantity, Price, Total_Price, Order_Time,GETDATE() AS Completion_Time, 'Completed' AS Status
                    FROM OrderItem";

                    using (SqlCommand command = new SqlCommand(copyDataQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        // MessageBox.Show(rowsAffected + " rows copied from OrderItem to OrderHistory with status 'Completed'.");
                    }

                    // Delete all rows from OrderItem table
                    string deleteOrderItemQuery = "DELETE FROM OrderItem";
                    using (SqlCommand command = new SqlCommand(deleteOrderItemQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    string deleteViewOrderQuery = "DELETE FROM ViewOrder";
                    using (SqlCommand command = new SqlCommand(deleteViewOrderQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    label7.Text = "0";
                    LoadData();
                    LoadDataGrid2();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }








        }
    }
}
