using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Proiect_Paoo
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            // Validate all fields are filled
            if (string.IsNullOrWhiteSpace(textBoxName.Text) ||
                string.IsNullOrWhiteSpace(textBoxSurname.Text) ||
                string.IsNullOrWhiteSpace(textBoxEmail.Text) ||
                string.IsNullOrWhiteSpace(textBoxPhone.Text) ||
                string.IsNullOrWhiteSpace(textBoxPassword.Text) ||
                string.IsNullOrWhiteSpace(textBoxConfirmPassword.Text))
            {
                MessageBox.Show("Please fill in all fields!", "Registration Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate email format
            if (!IsValidEmail(textBoxEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address!", "Registration Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate phone format
            if (!IsValidPhone(textBoxPhone.Text))
            {
                MessageBox.Show("Please enter a valid phone number!", "Registration Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if passwords match
            if (textBoxPassword.Text != textBoxConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match!", "Registration Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check password length
            if (textBoxPassword.Text.Length < 3)
            {
                MessageBox.Show("Password must be at least 3 characters long!", "Registration Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MySqlConnection connection = null;

            try
            {
                connection = DatabaseHelper.OpenConnection();
                string query = "INSERT INTO utilizatori (nume, pren, email, rol, parola, tel) VALUES (@Name, @Surname, @Email, @Role, @Password, @Phone)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", textBoxName.Text);
                    command.Parameters.AddWithValue("@Surname", textBoxSurname.Text);
                    command.Parameters.AddWithValue("@Email", textBoxEmail.Text);
                    command.Parameters.AddWithValue("@Role", "User"); // Default role
                    command.Parameters.AddWithValue("@Password", textBoxPassword.Text); // Consider hashing the password
                    command.Parameters.AddWithValue("@Phone", textBoxPhone.Text);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registration successful!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Open login form and hide registration
                        Login loginForm = new Login();
                        loginForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("An error occurred during registration.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhone(string phone)
        {
            // Basic phone validation - you might want to adjust this based on your requirements
            return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\+?[\d\s-()]{10,}$");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabelLogin_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
}
