using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text;

namespace Proiect_Paoo
{
    public partial class Form1 : Form
    {
        private RezervareDB rezervareDB;
        private Dictionary<int, string> locuriDisponibile;
        private int selectedOrarId = -1;
        private User currentUser; // Replace idUser with currentUser

        public Form1(User user) // Update constructor to accept User
        {
            InitializeComponent();
            rezervareDB = new RezervareDB();
            locuriDisponibile = new Dictionary<int, string>();
            currentUser = user; // Assign the User object
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            LoadInitialData();
            
        }

        private void ConfigureDataGridView()
        {
            // Configurare DataGridView
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

            // Adăugare coloane
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("ID", "ID");
                dataGridView1.Columns.Add("Spectacol", "Spectacol");
                dataGridView1.Columns.Add("Teatru", "Teatru");
                dataGridView1.Columns.Add("Ora", "Ora");
                dataGridView1.Columns.Add("Data", "Data");
                dataGridView1.Columns.Add("Pret", "Preț (RON)");
            }
        }

        private void LoadInitialData()
        {
            try
            {
                LoadSpectacole();
                // Scoatem LoadGenuri() din LoadInitialData pentru a evita conflictul de conexiuni
                LoadGenuri();
                radioButton1.Checked = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea datelor inițiale: {ex.Message}", "Eroare");
            }
        }

        private void LoadSpectacole()
        {
            MySqlConnection connection = null;
            try
            {
                dataGridView1.Rows.Clear();
                connection = DatabaseHelper.OpenConnection();

                string query = @"SELECT O.ID_ORAR, S.NUME AS NUME_SPECTACOL, T.NUME AS TEATRU, 
                        O.ORA, O.DATA, S.pret_pers, S.descriere, T.oras, SA.nume AS SALA 
                        FROM SPECTACOL S 
                        JOIN ORAR O ON O.id_spec = S.id_spec 
                        JOIN SALA SA ON SA.id_sala = O.id_sala 
                        JOIN TEATRU T ON T.id_teatru = SA.id_teatru
                        ORDER BY O.DATA, O.ORA";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(
                                reader["ID_ORAR"],
                                reader["NUME_SPECTACOL"],
                                reader["TEATRU"],
                                reader["ORA"],
                                Convert.ToDateTime(reader["DATA"]).ToShortDateString(),
                                reader["pret_pers"]
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea spectacolelor: {ex.Message}", "Eroare");
            }
            finally
            {
                if (connection != null)
                    DatabaseHelper.CloseConnection();
            }
        }

        private void LoadGenuri()
        {
            try
            {
                comboBox1.Items.Clear();
                comboBox1.Items.Add(""); // Opțiune goală pentru resetare filtru

                MySqlConnection connection = DatabaseHelper.OpenConnection();
                string query = "SELECT DISTINCT GEN FROM SPECTACOL ORDER BY GEN";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["GEN"].ToString());
                    }
                }

                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea genurilor: {ex.Message}", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DatabaseHelper.CloseConnection();
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var row = dataGridView1.SelectedRows[0];
                    if (row.Cells["ID"].Value != null)
                    {
                        selectedOrarId = Convert.ToInt32(row.Cells["ID"].Value);

                        // Retrieve description, city, and room for the selected show
                        MySqlConnection connection = DatabaseHelper.OpenConnection();
                        string query = @"SELECT S.descriere, T.oras, SA.nume AS SALA 
                                         FROM SPECTACOL S 
                                         JOIN ORAR O ON O.id_spec = S.id_spec 
                                         JOIN SALA SA ON SA.id_sala = O.id_sala 
                                         JOIN TEATRU T ON T.id_teatru = SA.id_teatru
                                         WHERE O.ID_ORAR = @IdOrar";

                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@IdOrar", selectedOrarId);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string descriere = reader["descriere"].ToString();
                                    string oras = reader["oras"].ToString();
                                    string sala = reader["SALA"].ToString();

                                    // Update textBox2 with the description, city, and room
                                    textBox2.Text = $"Descriere: {descriere}\r\nOraș: {oras}\r\nSală: {sala}";
                                    LoadLocuriDisponibile(selectedOrarId);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la selectarea rândului: {ex.Message}", "Eroare");
            }
        }

        private void LoadLocuriDisponibile(int idOrar)
        {
            MySqlConnection connection = null;
            try
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                locuriDisponibile.Clear();

                connection = DatabaseHelper.OpenConnection();
                string query = @"SELECT L.ID_LOC, L.pozitie 
                        FROM LOC L 
                        JOIN ORAR O ON O.ID_SALA = L.ID_SALA 
                        WHERE L.ID_LOC NOT IN (
                            SELECT ID_LOC FROM REZERVARE WHERE ID_ORAR = @IdOrar
                        ) AND O.ID_ORAR = @IdOrar
                        ORDER BY L.pozitie";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IdOrar", idOrar);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idLoc = Convert.ToInt32(reader["ID_LOC"]);
                            string pozitie = reader["pozitie"].ToString();
                            locuriDisponibile.Add(idLoc, pozitie);
                            listBox1.Items.Add(pozitie);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea locurilor disponibile: {ex.Message}", "Eroare");
            }
            finally
            {
                if (connection != null)
                    DatabaseHelper.CloseConnection();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Mutare locuri selectate din listBox1 în listBox2
            if (listBox1.SelectedItems.Count > 0)
            {
                List<string> selectedItems = new List<string>();
                foreach (string item in listBox1.SelectedItems)
                {
                    selectedItems.Add(item);
                }

                foreach (string item in selectedItems)
                {
                    listBox2.Items.Add(item);
                    listBox1.Items.Remove(item);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Mutare locuri selectate din listBox2 înapoi în listBox1
            if (listBox2.SelectedItems.Count > 0)
            {
                List<string> selectedItems = new List<string>();
                foreach (string item in listBox2.SelectedItems)
                {
                    selectedItems.Add(item);
                }

                foreach (string item in selectedItems)
                {
                    listBox1.Items.Add(item);
                    listBox2.Items.Remove(item);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string numeSpectacol = textBox1.Text.Trim();
                string gen = comboBox1.SelectedItem?.ToString() ?? "";
                bool locuriDisponibile = radioButton1.Checked;

                StringBuilder queryBuilder = new StringBuilder(@"
                    SELECT DISTINCT O.ID_ORAR, S.NUME AS NUME_SPECTACOL, T.NUME AS TEATRU, 
                    O.ORA, O.DATA, S.pret_pers 
                    FROM SPECTACOL S 
                    JOIN ORAR O ON O.id_spec = S.id_spec 
                    JOIN SALA SA ON SA.id_sala = O.id_sala 
                    JOIN TEATRU T ON T.id_teatru = SA.id_teatru 
                    WHERE 1=1");

                List<MySqlParameter> parameters = new List<MySqlParameter>();

                if (!string.IsNullOrEmpty(numeSpectacol))
                {
                    queryBuilder.Append(" AND S.NUME LIKE @NumeSpectacol");
                    parameters.Add(new MySqlParameter("@NumeSpectacol", $"%{numeSpectacol}%"));
                }

                if (!string.IsNullOrEmpty(gen))
                {
                    queryBuilder.Append(" AND S.GEN = @Gen");
                    parameters.Add(new MySqlParameter("@Gen", gen));
                }

                if (locuriDisponibile)
                {
                    queryBuilder.Append(@" AND O.ID_ORAR IN (
                        SELECT DISTINCT O2.ID_ORAR 
                        FROM ORAR O2 
                        JOIN LOC L ON L.ID_SALA = O2.ID_SALA 
                        WHERE L.ID_LOC NOT IN (
                            SELECT ID_LOC FROM REZERVARE R WHERE R.ID_ORAR = O2.ID_ORAR
                        )
                    )");
                }

                queryBuilder.Append(" ORDER BY O.DATA, O.ORA");

                MySqlConnection connection = DatabaseHelper.OpenConnection();
                using (MySqlCommand cmd = new MySqlCommand(queryBuilder.ToString(), connection))
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    dataGridView1.Rows.Clear();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(
                                reader["ID_ORAR"],
                                reader["NUME_SPECTACOL"],
                                reader["TEATRU"],
                                reader["ORA"],
                                Convert.ToDateTime(reader["DATA"]).ToShortDateString(),
                                reader["pret_pers"]
                            );
                        }
                    }
                }

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Nu s-au găsit spectacole care să corespundă criteriilor de filtrare.",
                        "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la filtrarea spectacolelor: {ex.Message}", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DatabaseHelper.CloseConnection();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Se poate adăuga logică suplimentară aici dacă este necesar
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Se poate adăuga logică suplimentară aici dacă este necesar
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox2.Items.Count == 0)
                {
                    MessageBox.Show("Nu ați selectat niciun loc pentru rezervare.", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Prepare data for the Plata form
                Dictionary<int, string> locuriSelectate = new Dictionary<int, string>();
                foreach (var item in listBox2.Items)
                {
                    foreach (var loc in locuriDisponibile)
                    {
                        if (loc.Value == item.ToString())
                        {
                            locuriSelectate.Add(loc.Key, loc.Value);
                        }
                    }
                }

                if (selectedOrarId == -1)
                {
                    MessageBox.Show("Nu ați selectat un spectacol.", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Retrieve the ticket price from the selected row in the DataGridView
                var selectedRow = dataGridView1.SelectedRows[0];
                int pretPers = Convert.ToInt32(selectedRow.Cells["Pret"].Value);

                // Open the Plata form
                Plata plataForm = new Plata(locuriSelectate, selectedOrarId, currentUser.Id, pretPers); // Use currentUser.Id
                plataForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la rezervarea locurilor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Implement event handlers for the menu items
        private void mainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirect to Main form
            Main mainForm = new Main(currentUser); // Pass the User object
            mainForm.Show();
            this.Hide();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show an About dialog
            MessageBox.Show("This is the Form1 page for Proiect_Paoo.\nVersion 1.0", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show documentation or redirect to a documentation page
            MessageBox.Show("Documentation is available at: https://example.com/docs", "Documentation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}