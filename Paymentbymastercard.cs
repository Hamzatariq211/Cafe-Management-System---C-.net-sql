using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project
{
   
    public partial class Paymentbymastercard : Form
    {


        public string Sb;


        public Paymentbymastercard(string Sb)
        {
            this.Sb = Sb;
            InitializeComponent();
            LoadImage();
            //private bool IsCreditCardNumberValid(string cardNumber);
        }


        private void LoadImage()
        {
            try
            {
                // Replace "path_to_your_image_file" with the actual path of your image
                string imagePath = "D:\\picture1.jpg";

                // Check if the file exists
                if (System.IO.File.Exists(imagePath))
                {
                    // Load the image from the file path
                    Image img = Image.FromFile(imagePath);

                    // Set the loaded image to the PictureBox
                    pictureBox1.Image = img;
                }
                else
                {
                    MessageBox.Show("Image file not found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if any of the text boxes or combo boxes are empty
            if (string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(cardholdername.Text) ||
            string.IsNullOrWhiteSpace(BankName.Text) || string.IsNullOrWhiteSpace(CvvName.Text) ||
                string.IsNullOrWhiteSpace(Month.Text) || string.IsNullOrWhiteSpace(year.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Get the credit card number from the input
            string creditCardNumber = textBox3.Text.Replace(" ", ""); // Remove spaces if any

            if (IsCreditCardNumberValid(creditCardNumber)==true)
            {
                // Credit card number is valid according to Luhn's algorithm
                PayedbyMasterCard Paymc = new PayedbyMasterCard(Sb);
                Paymc.Show();
                Dispose();
            }
            else
            {
                MessageBox.Show("Invalid credit card number.");
            }
        }

        private bool IsCreditCardNumberValid(string cardNumber)
        {
            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(cardNumber[i].ToString());

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
                alternate = !alternate;
            }

            return sum % 10 == 0;
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void p_Click(object sender, EventArgs e)
        {

        }
    }
}
