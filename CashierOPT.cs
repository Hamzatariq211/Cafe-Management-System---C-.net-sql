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
    public partial class CashierOPT : Form
    {
        public CashierOPT()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EmployeeWork emp = new EmployeeWork();
            emp.Show();
            Dispose();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CashierInterface cashierInterface = new CashierInterface();
            cashierInterface.Show();
            Dispose();

        }
    }
}
