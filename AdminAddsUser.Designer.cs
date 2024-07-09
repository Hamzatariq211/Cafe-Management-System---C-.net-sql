namespace Project
{
    partial class AdminAddsUser
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
            this.button1 = new System.Windows.Forms.Button();
            this.email = new System.Windows.Forms.TextBox();
            this.p = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.ln = new System.Windows.Forms.TextBox();
            this.fn = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Orange;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.email);
            this.panel1.Controls.Add(this.p);
            this.panel1.Controls.Add(this.username);
            this.panel1.Controls.Add(this.ln);
            this.panel1.Controls.Add(this.fn);
            this.panel1.Location = new System.Drawing.Point(63, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(672, 374);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(272, 289);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 42);
            this.button1.TabIndex = 5;
            this.button1.Text = "Add User";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(263, 203);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(152, 27);
            this.email.TabIndex = 4;
            this.email.Text = "Email";
            this.email.TextChanged += new System.EventHandler(this.email_TextChanged);
            // 
            // p
            // 
            this.p.Location = new System.Drawing.Point(413, 128);
            this.p.Name = "p";
            this.p.Size = new System.Drawing.Size(155, 27);
            this.p.TabIndex = 3;
            this.p.Text = "Password";
            this.p.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(106, 128);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(155, 27);
            this.username.TabIndex = 2;
            this.username.Text = "Username";
            this.username.TextChanged += new System.EventHandler(this.username_TextChanged);
            // 
            // ln
            // 
            this.ln.Location = new System.Drawing.Point(413, 70);
            this.ln.Name = "ln";
            this.ln.Size = new System.Drawing.Size(155, 27);
            this.ln.TabIndex = 1;
            this.ln.Text = "Last Name";
            this.ln.TextChanged += new System.EventHandler(this.lastname_TextChanged);
            // 
            // fn
            // 
            this.fn.Location = new System.Drawing.Point(106, 70);
            this.fn.Name = "fn";
            this.fn.Size = new System.Drawing.Size(155, 27);
            this.fn.TabIndex = 0;
            this.fn.Text = "First Name";
            this.fn.TextChanged += new System.EventHandler(this.firstname_TextChanged);
            // 
            // AdminAddsUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "AdminAddsUser";
            this.Text = "AdminAddsUser";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Button button1;
        private TextBox email;
        private TextBox p;
        private TextBox username;
        private TextBox ln;
        private TextBox fn;
    }
}