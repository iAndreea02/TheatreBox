using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Proiect_Paoo
{
    public partial class Main : Form
    {
        private User currentUser;
        private Label timeLabel;
        private Panel headerPanel;
        private Button btnAddShow;
        private Button btnGenerateReport;
        private System.Windows.Forms.Timer dateTimeTimer;

        public Main(User user)
        {
            InitializeComponent();
            currentUser = user;
            // InitializeCustomComponents();  
            labelNume.Text = user.Nume + "  " + user.Prenume;

            if (currentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                pictureBox2.Image = Image.FromFile("D:\\Facultate\\Proiect_Sem2\\Proiect_Paoo\\Proiect_Paoo\\img\\admin1.png");
            }
            else
            {
                button3.Visible = false; // Hide 'Adauga Spectacol' button
                button4.Visible = false; // Hide 'Raport' button
                //pictureBox1.Image = Image.FromFile("D:\\Facultate\\Proiect_Sem2\\Proiect_Paoo\\Proiect_Paoo\\img\\user_image.png");
            }
        }

        private void InitializeCustomComponents()
        {
            // Initialize header panel  
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(48, 84, 150)
            };

            // Add welcome label with role indicator  
            Label welcomeLabel = new Label
            {
                Text = $"Welcome, {currentUser.Nume} {currentUser.Prenume} ({currentUser.Role})",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 10)
            };
            headerPanel.Controls.Add(welcomeLabel);

            // Add time label  
            timeLabel = new Label
            {
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(20, 35)
            };
            headerPanel.Controls.Add(timeLabel);

            // Add admin controls if user has admin role  
            if (currentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                btnAddShow = new Button
                {
                    Text = "Add Show",
                    BackColor = Color.FromArgb(72, 123, 209),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(100, 30),
                    Location = new Point(this.Width - 240, 15)
                };
                btnAddShow.Click += BtnAddShow_Click;
                btnAddShow.FlatAppearance.BorderSize = 0;
                headerPanel.Controls.Add(btnAddShow);

                btnGenerateReport = new Button
                {
                    Text = "Generate Report",
                    BackColor = Color.FromArgb(72, 123, 209),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(100, 30),
                    Location = new Point(this.Width - 120, 15)
                };
                btnGenerateReport.Click += BtnGenerateReport_Click;
                btnGenerateReport.FlatAppearance.BorderSize = 0;
                headerPanel.Controls.Add(btnGenerateReport);
            }

            this.Controls.Add(headerPanel);

            // Adjust main panel position  
            if (panel1 != null)
            {
                panel1.Location = new Point(panel1.Location.X, headerPanel.Bottom + 10);
            }
        }

        private void BtnAddShow_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard(currentUser.Id, $"{currentUser.Nume} {currentUser.Prenume}", currentUser.Role);
            dashboard.Show();
            this.Hide();
        }

        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard(currentUser.Id, $"{currentUser.Nume} {currentUser.Prenume}", currentUser.Role);
            dashboard.Show();
            this.Hide();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                this.Text = $"Welcome, {currentUser.Nume} {currentUser.Prenume}";
            }

            // Set visibility of buttons based on user role
            

            // Initialize and start the timer for updating date and time
            if (dateTimeTimer == null)
            {
                dateTimeTimer = new System.Windows.Forms.Timer();
                dateTimeTimer.Interval = 1000; // 1 second
                dateTimeTimer.Tick += UpdateDateTime;
                dateTimeTimer.Start();
            }
        }

        private void UpdateDateTime(object sender, EventArgs e)
        {
            if (labelDateTime != null)
            {
                labelDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle bounds = this.panel1.ClientRectangle;
            int cornerRadius = 20; // Adjust for desired roundness  
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(bounds.X, bounds.Y, cornerRadius, cornerRadius, 180, 90);
                path.AddArc(bounds.Right - cornerRadius, bounds.Y, cornerRadius, cornerRadius, 270, 90);
                path.AddArc(bounds.Right - cornerRadius, bounds.Bottom - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                path.AddArc(bounds.X, bounds.Bottom - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                path.CloseAllFigures();
                this.panel1.Region = new Region(path);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(currentUser); // Pass the User object
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BiletulMeu biletulMeuForm = new BiletulMeu(currentUser); // Pass the User object
            biletulMeuForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminTable adminTable = new AdminTable(currentUser);
            adminTable.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Raport rp = new Raport(currentUser);
            rp.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Redirect to the Login form
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (headerPanel != null && btnAddShow != null && btnGenerateReport != null)
            {
                btnAddShow.Location = new Point(this.Width - 240, 15);
                btnGenerateReport.Location = new Point(this.Width - 120, 15);
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop and dispose of the timer to release resources
            dateTimeTimer?.Stop();
            dateTimeTimer?.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
