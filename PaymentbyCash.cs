﻿using System;
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
    public partial class PaymentbyCash : Form
    {
        public PaymentbyCash()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";
        private void button1_Click(object sender, EventArgs e)
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

                    Dispose();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
