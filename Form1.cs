namespace Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer Cs = new Customer();

           
            Cs.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           AdminRegisteration Ar = new AdminRegisteration();


            Ar.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CashierLogin Cl = new CashierLogin();


            Cl.Show();
            this.Hide();
        }
    }
}