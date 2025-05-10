using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Proiect_Paoo
{
    public partial class AddSpect : Form
    {
        private SpectDB spectDB;
        private bool isNewDirector = false;

        public AddSpect()
        {
            InitializeComponent();
            spectDB = new SpectDB(1, "Admin"); // Replace with actual user ID and role
        }

        private void AddSpect_Load(object sender, EventArgs e)
        {
            LoadDirectors();
            LoadGenres();

            // Ensure the panel is hidden initially
            panelRegizor.Visible = false;
            regizorS.SelectedIndexChanged += regizorS_SelectedIndexChanged;
        }

        private void LoadDirectors()
        {
            var directors = spectDB.GetAllDirectors();
            regizorS.Items.Clear();
            foreach (var director in directors)
            {
                // Ensure the format is consistent and valid
                regizorS.Items.Add($"{director.Id}: {director.Nume} {director.Prenume}");
            }
            regizorS.Items.Add("Nou Regizor");
        }

        private void LoadGenres()
        {
            genS.Items.Clear();
            genS.Items.AddRange(SpectDB.GenreList);
        }

        private void regizorS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (regizorS.SelectedItem != null && regizorS.SelectedItem.ToString() == "Nou Regizor")
            {
                panelRegizor.Visible = true;
                isNewDirector = true; // Mark as new director
            }
            else
            {
                panelRegizor.Visible = false;
                isNewDirector = false; // Reset to existing director

                // Validate the selected director's format
                string selectedDirector = regizorS.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedDirector) && 
                    (!selectedDirector.Contains(":") || !int.TryParse(selectedDirector.Split(':')[0], out _)))
                {
                    MessageBox.Show("Formatul regizorului selectat este invalid. Asigură-te că selectezi un regizor valid.",
                        "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    regizorS.SelectedIndex = -1; // Reset selection
                }
            }

            panelRegizor.Refresh();
            this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e) // Save button
        {
            string spectName = numeS.Text;
            string genre = genS.SelectedItem?.ToString();
            decimal price = pretS.Value;
            string description = descriereS.Text;

            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(spectName))
                {
                    MessageBox.Show("Numele spectacolului este obligatoriu.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(genre))
                {
                    MessageBox.Show("Genul spectacolului este obligatoriu.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (price <= 0)
                {
                    MessageBox.Show("Prețul trebuie să fie un număr pozitiv.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(description))
                {
                    MessageBox.Show("Descrierea spectacolului este obligatorie.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isNewDirector)
                {
                    // Create new director first
                    string directorName = numeRegizor.Text;
                    string directorSurname = prenRegizor.Text;
                    DateTime birthDate = dataNasteriiRegizor.Value;

                    // Validate new director fields
                    if (string.IsNullOrWhiteSpace(directorName) || string.IsNullOrWhiteSpace(directorSurname))
                    {
                        MessageBox.Show("Numele și prenumele regizorului sunt obligatorii.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (birthDate > DateTime.Now)
                    {
                        MessageBox.Show("Data nașterii nu poate fi în viitor.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int newDirectorId = AddNewDirector(directorName, directorSurname, birthDate);

                    // Use the new director's ID to create the show
                    spectDB.InserareSpectacol(spectName, genre, price, description, newDirectorId);
                }
                else
                {
                    // Use the selected director's ID to create the show
                    if (regizorS.SelectedItem == null)
                    {
                        MessageBox.Show("Selectează un regizor din listă.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int selectedDirectorId = int.Parse(regizorS.SelectedItem.ToString().Split(':')[0]);
                    spectDB.InserareSpectacol(spectName, genre, price, description, selectedDirectorId);
                }

                MessageBox.Show("Spectacol adăugat cu succes!");
            }
            catch (FormatException)
            {
                MessageBox.Show("Formatul prețului sau al altor câmpuri este invalid.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la adăugarea spectacolului: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int AddNewDirector(string name, string surname, DateTime birthDate)
        {
            // Validate birth date
            if (birthDate > DateTime.Now)
            {
                throw new ArgumentException("Data nașterii nu poate fi în viitor.");
            }

            using (var cn = DatabaseHelper.OpenConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand(@"
                        INSERT INTO regizori (nume, prenume, data_nasterii) 
                        VALUES (@NUME, @PRENUME, @DATA_NASTERII);
                        SELECT LAST_INSERT_ID();", cn))
                    {
                        cmd.Parameters.AddWithValue("@NUME", name);
                        cmd.Parameters.AddWithValue("@PRENUME", surname);
                        cmd.Parameters.AddWithValue("@DATA_NASTERII", birthDate);

                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la adăugarea regizorului: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e) // Save button
        {
            if (string.IsNullOrWhiteSpace(numeS.Text))
            {
                MessageBox.Show("Numele spectacolului este obligatoriu.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (genS.SelectedItem == null)
            {
                MessageBox.Show("Genul spectacolului este obligatoriu.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(descriereS.Text))
            {
                MessageBox.Show("Descrierea spectacolului este obligatorie.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificare dacă utilizatorul dorește să adauge un regizor nou
            if (isNewDirector)
            {
                if (string.IsNullOrWhiteSpace(numeRegizor.Text))
                {
                    MessageBox.Show("Numele regizorului este obligatoriu.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(prenRegizor.Text))
                {
                    MessageBox.Show("Prenumele regizorului este obligatoriu.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime birthDate = dataNasteriiRegizor.Value;

                // Validate birth date
                if (birthDate > DateTime.Now)
                {
                    MessageBox.Show("Data nașterii nu poate fi în viitor.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                // Verificare selecție regizor
                if (regizorS.SelectedItem == null)
                {
                    MessageBox.Show("Selectează un regizor din listă.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string selectedDirector = regizorS.SelectedItem.ToString();

                // Validare format regizor selectat
                
            }

            // Salvare logică
            string spectName = numeS.Text;
            string genre = genS.SelectedItem?.ToString();
            decimal price = pretS.Value;
            string description = descriereS.Text;

            try
            {
                if (isNewDirector)
                {
                    // Creare regizor nou
                    string directorName = numeRegizor.Text;
                    string directorSurname = prenRegizor.Text;
                    DateTime birthDate = dataNasteriiRegizor.Value;
                    MessageBox.Show($"Regizor nou adăugat cu succes:\n\n" +
                            
                            $"Nume: {directorName} {directorSurname}\n" +
                            $"Data Nașterii: {birthDate:dd/MM/yyyy}",
                            "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    int newDirectorId = AddNewDirector(directorName, directorSurname, birthDate);

                    // Creează spectacol folosind ID-ul regizorului nou
                    spectDB.InserareSpectacol(spectName, genre, price, description, newDirectorId);
                }
                else
                {
                    // Folosește ID-ul regizorului selectat
                    int selectedDirectorId = int.Parse(regizorS.SelectedItem.ToString().Split(':')[0]);
                    spectDB.InserareSpectacol(spectName, genre, price, description, selectedDirectorId);
                }

                MessageBox.Show("Spectacol adăugat cu succes!");
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la adăugarea spectacolului: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}