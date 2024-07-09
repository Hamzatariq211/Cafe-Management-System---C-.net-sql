using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class AdminOPT : Form
    {
        public AdminOPT()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserManagement UM=new UserManagement("User");
            UM.Show();
            Dispose();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserManagement UM = new UserManagement("Cashier");
            UM.Show();
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InventoryOPT inventory = new InventoryOPT();
            inventory.Show();
            Dispose();
        }
    }
}
