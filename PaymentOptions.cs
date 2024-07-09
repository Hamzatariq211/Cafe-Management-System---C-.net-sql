using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class PaymentOptions : Form
    {
        public int UID;
       
       //rderCheck Oc = new OrderCheck();
        public string billsend;

        string connectionString = "Data Source=DESKTOP-ICNK77G\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;Pooling=true;Max Pool Size=100;";
        
        public PaymentOptions(int UID)
        {
            this.UID = UID;
            InitializeComponent();
            DisplayTotalPriceSum();
            
           
            
          
        

        }


        private void DisplayTotalPriceSum()
        {
            string query = "SELECT SUM(Total_Price) FROM ViewOrder";
            decimal totalPrice = 0;
            int count = 0; // Variable to hold the count of loyal customers

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            totalPrice = Convert.ToDecimal(result);
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

            // Check if the user is a loyal customer
            string loyalCustomerQuery = $"SELECT COUNT(*) FROM LoyalCustomers WHERE User_ID = {UID}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(loyalCustomerQuery, connection))
                {
                 //   command.Parameters.AddWithValue("@UID", UID);

                    try
                    {
                        connection.Open();
                        count = (int)command.ExecuteScalar();
                     
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

            // Calculate discounted price based on loyalty status
            decimal discountedPrice = totalPrice;

            if (count > 0)
            {
                discountedPrice = totalPrice * 0.8m; // 20% discount for loyal customers
            }

            label3.Text = discountedPrice.ToString("C2");
            billsend = discountedPrice.ToString();
        }







        private string GetSelectedRole()
        {
            if (radioButton1.Checked)
            {
                return "Cash";
            }
            else if (radioButton2.Checked)
            {
                return "MasterCard";
            }
            else if (radioButton3.Checked)
            {
                return "EasyJazz";
            }
            else
            {
                return string.Empty;
            }
        }



















        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String choice = GetSelectedRole();

            if (choice == "Cash")
            {
                PaymentbyCash Pc=new PaymentbyCash();
                Pc.Show();
                Dispose();


            }

            else if (choice == "MasterCard")
            {
                
                Paymentbymastercard Pmc=new Paymentbymastercard(billsend);
            //    Pmc.Pay = B;
                Pmc.Show();
                Dispose();
            }

            else if (choice == "EasyJazz")
            {
                PayByEasyJazz BEJ = new PayByEasyJazz(billsend);

                BEJ.Show();
                Dispose();


            }



        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
