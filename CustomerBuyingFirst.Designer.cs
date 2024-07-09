namespace Project
{
    partial class CustomerBuyingFirst
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.quantity1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.proceedcart = new System.Windows.Forms.Button();
            this.addtocart = new System.Windows.Forms.Button();
            this.Category = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Orange;
            this.panel1.Controls.Add(this.quantity1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.proceedcart);
            this.panel1.Controls.Add(this.addtocart);
            this.panel1.Controls.Add(this.Category);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1107, 470);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // quantity1
            // 
            this.quantity1.Location = new System.Drawing.Point(227, 136);
            this.quantity1.Name = "quantity1";
            this.quantity1.Size = new System.Drawing.Size(56, 27);
            this.quantity1.TabIndex = 8;
            this.quantity1.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(139, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Quantity:";
            // 
            // proceedcart
            // 
            this.proceedcart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.proceedcart.Location = new System.Drawing.Point(98, 334);
            this.proceedcart.Name = "proceedcart";
            this.proceedcart.Size = new System.Drawing.Size(185, 43);
            this.proceedcart.TabIndex = 6;
            this.proceedcart.Text = "Proceed to Cart";
            this.proceedcart.UseVisualStyleBackColor = true;
            this.proceedcart.Click += new System.EventHandler(this.button2_Click);
            // 
            // addtocart
            // 
            this.addtocart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.addtocart.Location = new System.Drawing.Point(139, 274);
            this.addtocart.Name = "addtocart";
            this.addtocart.Size = new System.Drawing.Size(115, 38);
            this.addtocart.TabIndex = 5;
            this.addtocart.Text = "Add To Cart";
            this.addtocart.UseVisualStyleBackColor = true;
            this.addtocart.Click += new System.EventHandler(this.button1_Click);
            // 
            // Category
            // 
            this.Category.FormattingEnabled = true;
            this.Category.Items.AddRange(new object[] {
            "Coffees",
            "Breakfast Items",
            "Desserts",
            "Salads",
            "Brunch Menu",
            "Signature Dishes",
            "Snacks",
            "Beverages",
            "Sandwiches",
            "Breads",
            "Pasta Dishes",
            "Burgers"});
            this.Category.Location = new System.Drawing.Point(154, 83);
            this.Category.Name = "Category";
            this.Category.Size = new System.Drawing.Size(188, 28);
            this.Category.TabIndex = 2;
            this.Category.SelectedIndexChanged += new System.EventHandler(this.Category_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(46, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Category";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeight = 29;
            this.dataGridView1.Location = new System.Drawing.Point(396, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(677, 393);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // CustomerBuyingFirst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1131, 497);
            this.Controls.Add(this.panel1);
            this.Name = "CustomerBuyingFirst";
            this.Text = "CustomerBuyingFirst";
            this.Load += new System.EventHandler(this.CustomerBuyingFirst_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private DataGridView dataGridView1;
        private ComboBox Category;
        private Label label1;
        private Button proceedcart;
        private Button addtocart;
        private TextBox quantity1;
        private Label label3;
    }
}