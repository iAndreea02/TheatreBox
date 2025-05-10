using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Proiect_Paoo
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            CustomizeComponents();
        }

        private void CustomizeComponents()
        {
            // Style the login button
            buttonLogin.FlatStyle = FlatStyle.Flat;
            buttonLogin.FlatAppearance.BorderSize = 0;
            buttonLogin.BackColor = Color.FromArgb(46, 204, 113);
            buttonLogin.ForeColor = Color.White;
            buttonLogin.Cursor = Cursors.Hand;
            
            // Style the form
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            
            // Add hover effect to login button
            buttonLogin.MouseEnter += (s, e) => buttonLogin.BackColor = Color.FromArgb(39, 174, 96);
            buttonLogin.MouseLeave += (s, e) => buttonLogin.BackColor = Color.FromArgb(46, 204, 113);

            // Style welcome label
            labelWelcome.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            labelWelcome.ForeColor = Color.White;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string email = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MySqlConnection connection = null;

            try
            {
                connection = DatabaseHelper.OpenConnection();
                string query = "SELECT id_user, nume, rol, pren FROM utilizatori WHERE email = @Email AND parola = @Password";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {   int id = reader.GetInt32("id_user");
                            string nume = reader.GetString("nume");
                            string prenume = reader.GetString("pren");
                            string role = reader.GetString("rol");
                            User user = new User(id,nume, prenume, email,role);

                            //MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Main mainForm = new Main(user);
                            mainForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void linkLabelRegister_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            registerForm.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
