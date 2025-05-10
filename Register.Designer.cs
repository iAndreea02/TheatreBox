using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Proiect_Paoo
{
    partial class Register
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelSurname;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Label labelPhone;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelConfirmPassword;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxSurname;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.TextBox textBoxPhone;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxConfirmPassword;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.LinkLabel linkLabelLogin;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            this.labelName = new System.Windows.Forms.Label();
            this.labelSurname = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.labelPhone = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelConfirmPassword = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxSurname = new System.Windows.Forms.TextBox();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.textBoxPhone = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxConfirmPassword = new System.Windows.Forms.TextBox();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.linkLabelLogin = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(18, 120);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(47, 16);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name:";
            // 
            // labelSurname
            // 
            this.labelSurname.AutoSize = true;
            this.labelSurname.Location = new System.Drawing.Point(18, 162);
            this.labelSurname.Name = "labelSurname";
            this.labelSurname.Size = new System.Drawing.Size(64, 16);
            this.labelSurname.TabIndex = 1;
            this.labelSurname.Text = "Surname:";
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(18, 202);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(44, 16);
            this.labelEmail.TabIndex = 2;
            this.labelEmail.Text = "Email:";
            // 
            // labelPhone
            // 
            this.labelPhone.AutoSize = true;
            this.labelPhone.Location = new System.Drawing.Point(18, 242);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(49, 16);
            this.labelPhone.TabIndex = 3;
            this.labelPhone.Text = "Phone:";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(18, 321);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(70, 16);
            this.labelPassword.TabIndex = 4;
            this.labelPassword.Text = "Password:";
            // 
            // labelConfirmPassword
            // 
            this.labelConfirmPassword.AutoSize = true;
            this.labelConfirmPassword.Location = new System.Drawing.Point(18, 365);
            this.labelConfirmPassword.Name = "labelConfirmPassword";
            this.labelConfirmPassword.Size = new System.Drawing.Size(118, 16);
            this.labelConfirmPassword.TabIndex = 5;
            this.labelConfirmPassword.Text = "Confirm Password:";
            // 
            // textBoxName
            // 
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxName.Location = new System.Drawing.Point(172, 110);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(180, 27);
            this.textBoxName.TabIndex = 6;
            // 
            // textBoxSurname
            // 
            this.textBoxSurname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSurname.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxSurname.Location = new System.Drawing.Point(172, 152);
            this.textBoxSurname.Name = "textBoxSurname";
            this.textBoxSurname.Size = new System.Drawing.Size(180, 27);
            this.textBoxSurname.TabIndex = 7;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxEmail.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxEmail.Location = new System.Drawing.Point(172, 192);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(180, 27);
            this.textBoxEmail.TabIndex = 8;
            this.textBoxEmail.TextChanged += new System.EventHandler(this.textBoxEmail_TextChanged);
            // 
            // textBoxPhone
            // 
            this.textBoxPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPhone.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxPhone.Location = new System.Drawing.Point(172, 232);
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.Size = new System.Drawing.Size(180, 27);
            this.textBoxPhone.TabIndex = 9;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxPassword.Location = new System.Drawing.Point(172, 311);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(180, 27);
            this.textBoxPassword.TabIndex = 10;
            // 
            // textBoxConfirmPassword
            // 
            this.textBoxConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxConfirmPassword.Location = new System.Drawing.Point(172, 355);
            this.textBoxConfirmPassword.Name = "textBoxConfirmPassword";
            this.textBoxConfirmPassword.PasswordChar = '*';
            this.textBoxConfirmPassword.Size = new System.Drawing.Size(180, 27);
            this.textBoxConfirmPassword.TabIndex = 11;
            // 
            // buttonRegister
            // 
            this.buttonRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRegister.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.buttonRegister.Location = new System.Drawing.Point(42, 404);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(310, 33);
            this.buttonRegister.TabIndex = 12;
            this.buttonRegister.Text = "Register";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // linkLabelLogin
            // 
            this.linkLabelLogin.AutoSize = true;
            this.linkLabelLogin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.linkLabelLogin.LinkColor = System.Drawing.Color.White;
            this.linkLabelLogin.Location = new System.Drawing.Point(98, 440);
            this.linkLabelLogin.Name = "linkLabelLogin";
            this.linkLabelLogin.Size = new System.Drawing.Size(183, 20);
            this.linkLabelLogin.TabIndex = 13;
            this.linkLabelLogin.TabStop = true;
            this.linkLabelLogin.Text = "Ai cont deja? Logheaza-te";
            this.linkLabelLogin.Click += new System.EventHandler(this.linkLabelLogin_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(11, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(617, 496);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelName);
            this.panel1.Controls.Add(this.labelSurname);
            this.panel1.Controls.Add(this.labelEmail);
            this.panel1.Controls.Add(this.labelPhone);
            this.panel1.Controls.Add(this.labelPassword);
            this.panel1.Controls.Add(this.labelConfirmPassword);
            this.panel1.Controls.Add(this.textBoxName);
            this.panel1.Controls.Add(this.textBoxSurname);
            this.panel1.Controls.Add(this.textBoxEmail);
            this.panel1.Controls.Add(this.textBoxPhone);
            this.panel1.Controls.Add(this.textBoxPassword);
            this.panel1.Controls.Add(this.textBoxConfirmPassword);
            this.panel1.Controls.Add(this.buttonRegister);
            this.panel1.Controls.Add(this.linkLabelLogin);
            this.panel1.Location = new System.Drawing.Point(616, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(415, 496);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 28.2F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(91, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 63);
            this.label1.TabIndex = 0;
            this.label1.Text = "iTravel";
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(1043, 554);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Register";
            this.Text = "Registration Page";
            this.Load += new System.EventHandler(this.Register_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;

        #endregion
    }
}