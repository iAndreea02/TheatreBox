using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using Proiect_Paoo;

namespace Proiect_Paoo
{
    public partial class AdminTable : Form
    {
        private User currentUser;
        private SpectDB spectDb;
        private bool isEditMode = false; // Flag pentru modul de editare
        private int editingIdOrar = -1; // ID_ORAR pentru rândul editat

        public AdminTable(User user)
        {
            InitializeComponent();
            currentUser = user;
            spectDb = new SpectDB(user.Id, user.Role); // Initialize SpectDB with user details
        }

        private void AdminTable_Load(object sender, EventArgs e)
        {
            // Use currentUser for any user-specific logic
            LoadSpectacoleInTable();
            InitializeFormInputs();
        }

        private void InitializeFormInputs()
        {
            // Ascundem panoul de editare/adăugare la început
            panelForm.Visible = false;

            // Populăm câmpurile ComboBox
            try
            {
                comboBoxOras.Items.AddRange(new string[]
                {
                    "-- Selectați orașul --", "București", "Cluj-Napoca", "Timișoara", "Constanța", "Iași"
                });
                comboBoxOras.SelectedIndex = 0; // Default selection

                var spectacole = spectDb.AfisSpectacol();
                if (spectacole != null)
                {
                    comboBoxSpectacol.Items.AddRange(spectacole);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la inițializarea formularului: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBoxOras_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedOras = comboBoxOras.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedOras) && selectedOras != "-- Selectați orașul --")
            {
                try
                {
                    comboBoxTeatru.Items.Clear();
                    var teatre = spectDb.AfisTeatru(selectedOras);
                    if (teatre != null && teatre.Length > 0)
                    {
                        comboBoxTeatru.Items.AddRange(teatre);
                    }
                    else
                    {
                        comboBoxTeatru.Items.Add("Niciun teatru disponibil");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la încărcarea teatrelor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                comboBoxTeatru.Items.Clear();
                comboBoxTeatru.Items.Add("Selectați un oraș mai întâi");
            }
        }

        private void ComboBoxTeatru_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedTeatru = comboBoxTeatru.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedTeatru) && selectedTeatru != "Niciun teatru disponibil")
            {
                try
                {
                    comboBoxSala.Items.Clear();
                    var sali = spectDb.AfisSala(selectedTeatru);
                    if (sali != null && sali.Length > 0)
                    {
                        comboBoxSala.Items.AddRange(sali);
                    }
                    else
                    {
                        comboBoxSala.Items.Add("Nicio sală disponibilă");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la încărcarea sălilor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                comboBoxSala.Items.Clear();
                comboBoxSala.Items.Add("Selectați un teatru mai întâi");
            }
        }

        private async void LoadSpectacoleInTable()
        {
            // Curățăm rândurile existente din DataGridView
            dataGridView.Rows.Clear();

            try
            {
                // Use asynchronous call to prevent UI freezing
                var spectacole = await Task.Run(() => spectDb.AfisSpec());

                foreach (var spectacol in spectacole)
                {
                    var fields = spectacol.Split('|');
                    if (fields.Length == 8)
                    {
                        dataGridView.Rows.Add(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5], fields[6], fields[7]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea spectacolelor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            ClearFormFields();
            isEditMode = false; // Setăm modul de adăugare
            panelForm.Visible = true; // Afișăm formularul pentru adăugare
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                isEditMode = true;
                panelForm.Visible = true; // Afișăm panoul de editare

                // Încărcăm datele rândului selectat în formular
                var selectedRow = dataGridView.SelectedRows[0];
                editingIdOrar = Convert.ToInt32(selectedRow.Cells["columnIdOrar"].Value);
                comboBoxSpectacol.Text = selectedRow.Cells["columnNume"].Value.ToString();
                comboBoxTeatru.Text = selectedRow.Cells["columnTeatru"].Value.ToString();
                comboBoxSala.Text = selectedRow.Cells["columnSala"].Value.ToString();
                dateTimePickerDate.Value = DateTime.Parse(selectedRow.Cells["columnData"].Value.ToString());
                textBoxOra.Text = selectedRow.Cells["columnOra"].Value.ToString();

                // Log the action
                spectDb.LogActiune(currentUser.Id, currentUser.Role, "Editare Orar", "Update", $"ID Orar: {editingIdOrar}");
            }
            else
            {
                MessageBox.Show("Selectați un rând din tabel pentru a-l edita.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView.SelectedRows[0];
                var idOrar = selectedRow.Cells[0].Value.ToString();
                var numeSpectacol = selectedRow.Cells[1].Value.ToString();
                var data = selectedRow.Cells[5].Value.ToString();
                var ora = selectedRow.Cells[6].Value.ToString();

                // Confirmarea ștergerii cu detalii despre spectacolul selectat
                var result = MessageBox.Show(
                    $"Sigur doriți să ștergeți spectacolul:\n\n" +
                    $"ID: {idOrar}\n" +
                    $"Nume: {numeSpectacol}\n" +
                    $"Data: {data}\n" +
                    $"Ora: {ora}\n\n" +
                    "Această acțiune va șterge și toate rezervările asociate!",
                    "Confirmare ștergere",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteSpectacol(idOrar);
                }
            }
            else
            {
                MessageBox.Show(
                    "Selectați un spectacol pentru a șterge.",
                    "Nicio selecție",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void DeleteSpectacol(string idOrar)
        {
            using (var cn = DatabaseHelper.OpenConnection())
            {
                using (var transaction = cn.BeginTransaction())
                {
                    try
                    {
                        // 1. Ștergere bilete asociate
                        using (var cmdDeleteBilete = new MySqlCommand(
                            "DELETE FROM BILET WHERE ID_REZERVARE IN (SELECT ID_REZERVARE FROM REZERVARE WHERE ID_ORAR = @ID_ORAR)",
                            cn, transaction))
                        {
                            cmdDeleteBilete.Parameters.AddWithValue("@ID_ORAR", idOrar);
                            cmdDeleteBilete.ExecuteNonQuery();
                        }

                        // 2. Ștergere rezervări
                        using (var cmdDeleteRezervari = new MySqlCommand(
                            "DELETE FROM REZERVARE WHERE ID_ORAR = @ID_ORAR",
                            cn, transaction))
                        {
                            cmdDeleteRezervari.Parameters.AddWithValue("@ID_ORAR", idOrar);
                            cmdDeleteRezervari.ExecuteNonQuery();
                        }

                        // 3. Ștergere orar
                        using (var cmdDeleteOrar = new MySqlCommand(
                            "DELETE FROM ORAR WHERE ID_ORAR = @ID_ORAR",
                            cn, transaction))
                        {
                            cmdDeleteOrar.Parameters.AddWithValue("@ID_ORAR", idOrar);
                            var rowsAffected = cmdDeleteOrar.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Logare acțiune
                                using (var cmdLog = new MySqlCommand(@"
                                    INSERT INTO LOG_ACTIUNI 
                                    (ID_UTILIZATOR, ROL, OPERATIE, COMANDA_SQL, DETALII_OPERATIE, DATA_OPERATIE) 
                                    VALUES (@ID_UTILIZATOR, @ROL, @OPERATIE, @COMANDA_SQL, @DETALII_OPERATIE, UTC_TIMESTAMP())",
                                    cn, transaction))
                                {
                                    cmdLog.Parameters.AddWithValue("@ID_UTILIZATOR", currentUser.Id);
                                    cmdLog.Parameters.AddWithValue("@ROL", currentUser.Role);
                                    cmdLog.Parameters.AddWithValue("@OPERATIE", "Ștergere spectacol");
                                    cmdLog.Parameters.AddWithValue("@COMANDA_SQL", "DELETE CASCADE");
                                    cmdLog.Parameters.AddWithValue("@DETALII_OPERATIE", $"ID Orar: {idOrar}");
                                    cmdLog.ExecuteNonQuery();
                                }

                                transaction.Commit();

                                MessageBox.Show(
                                    "Spectacolul și toate datele asociate au fost șterse cu succes!",
                                    "Succes",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                                // Reîmprospătare tabel
                                LoadSpectacoleInTable();
                            }
                            else
                            {
                                throw new Exception("Nu s-a găsit spectacolul pentru ștergere.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        MessageBox.Show(
                            $"Eroare la ștergerea spectacolului:\n\n{ex.Message}\n\n" +
                            "Toate modificările au fost anulate.",
                            "Eroare",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadSpectacoleInTable(); // Reîmprospătăm tabelul
            MessageBox.Show("Datele au fost reîmprospătate!", "Reîmprospătează", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var spectacol = comboBoxSpectacol.Text;
            var sala = comboBoxSala.Text;
            var teatru = comboBoxTeatru.Text;
            var data = dateTimePickerDate.Value.ToString("yyyy-MM-dd");
            var ora = textBoxOra.Value.ToString("HH:mm");

            if (string.IsNullOrWhiteSpace(spectacol) || string.IsNullOrWhiteSpace(sala) || string.IsNullOrWhiteSpace(teatru))
            {
                MessageBox.Show("Toate câmpurile sunt obligatorii.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (isEditMode)
                {
                    spectDb.UpdateOrar(editingIdOrar, spectacol, sala, ora, data);
                    spectDb.LogActiune(currentUser.Id, currentUser.Role, "Editare Orar", "UPDATE", $"ID Orar: {editingIdOrar}");
                    MessageBox.Show("Modificările au fost salvate cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    spectDb.InserareOrar(spectacol, sala, ora, data);
                    spectDb.LogActiune(currentUser.Id, currentUser.Role, "Adăugare Orar", "INSERT", $"Spectacol: {spectacol}, Sala: {sala}, Data: {data}, Ora: {ora}");
                    MessageBox.Show("Spectacolul a fost adăugat cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                panelForm.Visible = false;
                LoadSpectacoleInTable();
            }
            catch (Exception ex)
            {
              //  MessageBox.Show($"Eroare la salvarea datelor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            panelForm.Visible = false; // Ascundem formularul
            ClearFormFields(); // Golim câmpurile formularului
        }

        private void ClearFormFields()
        {
            comboBoxSpectacol.SelectedIndex = -1;
            comboBoxSala.SelectedIndex = -1;
            comboBoxTeatru.SelectedIndex = -1;
            comboBoxOras.SelectedIndex = 0;
            dateTimePickerDate.Value = DateTime.Now;
            //textBoxOra.Clear();
        }

        private void labelNume_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxSala_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedSala = comboBoxSala.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedSala) && selectedSala != "Nicio sală disponibilă" && selectedSala != "Selectați un teatru mai întâi")
            {
                // Perform an action based on the selected room
                MessageBox.Show($"Ați selectat sala: {selectedSala}", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Handle invalid or no selection
                MessageBox.Show("Selectați o sală validă.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBoxTeatru_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            var selectedTeatru = comboBoxTeatru.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedTeatru) && selectedTeatru != "Niciun teatru disponibil")
            {
                // Populate Sala ComboBox
                comboBoxSala.Items.Clear();
                var sali = spectDb.AfisSala(selectedTeatru);
                if (sali.Length > 0)
                {
                    comboBoxSala.Items.AddRange(sali);
                }
                else
                {
                    comboBoxSala.Items.Add("Nicio sală disponibilă");
                }

                // Populate Spectacol ComboBox
                comboBoxSpectacol.Items.Clear();
                var spectacole = spectDb.AfisSpectacol();
                if (spectacole.Length > 0)
                {
                    comboBoxSpectacol.Items.AddRange(spectacole);
                }
                else
                {
                    comboBoxSpectacol.Items.Add("Niciun spectacol disponibil");
                }
            }
            else
            {
                comboBoxSala.Items.Clear();
                comboBoxSala.Items.Add("Nicio sală disponibilă");

                comboBoxSpectacol.Items.Clear();
                comboBoxSpectacol.Items.Add("Niciun spectacol disponibil");
            }
        }

        private void ExportToExcel(string fileName)
        {
            try
            {
                // Set the license context using the new EPPlus 8+ API
                OfficeOpenXml.ExcelPackage.License.SetNonCommercialPersonal("iAndreea02");

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Spectacole");

                    // Add metadata in Excel
                    int headerRow = 1;

                    // Add export information
                    worksheet.Cells[headerRow, 1].Value = $"Data export (UTC): {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}";
                    worksheet.Cells[headerRow, 1, headerRow, dataGridView.Columns.Count].Merge = true;
                    worksheet.Cells[headerRow, 1].Style.Font.Bold = true;
                    headerRow++;

                    worksheet.Cells[headerRow, 1].Value = $"Exported by: {Environment.UserName}";
                    worksheet.Cells[headerRow, 1, headerRow, dataGridView.Columns.Count].Merge = true;
                    worksheet.Cells[headerRow, 1].Style.Font.Bold = true;
                    headerRow++;
                    headerRow++; // Empty row for spacing

                    // Add headers
                    for (int i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        var cell = worksheet.Cells[headerRow, i + 1];

                        // Rename "Oraș" column to "Oras" in the exported file
                        if (dataGridView.Columns[i].HeaderText == "Oraș")
                        {
                            cell.Value = "Oras";
                        }
                        else
                        {
                            cell.Value = dataGridView.Columns[i].HeaderText;
                        }

                        // Style header
                        cell.Style.Font.Bold = true;
                        cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(48, 84, 150));
                        cell.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    }

                    // Add data
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView.Columns.Count; j++)
                        {
                            var cell = worksheet.Cells[i + headerRow + 1, j + 1];
                            var value = dataGridView.Rows[i].Cells[j].Value;

                            // Remove diacritics for the 'Oraș' column
                            if (dataGridView.Columns[j].Name == "columnOras" && value != null)
                            {
                                value = RemoveDiacritics(value.ToString());
                            }

                            cell.Value = value;

                            // Alternate row colors
                            if (i % 2 == 0)
                            {
                                cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(240, 240, 240));
                            }

                            cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        }
                    }

                    // Auto-fit columns
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Add borders for the entire table
                    var tableRange = worksheet.Cells[headerRow, 1, headerRow + dataGridView.Rows.Count, dataGridView.Columns.Count];
                    tableRange.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

                    // Save file
                    var fi = new FileInfo(fileName);
                    package.SaveAs(fi);

                    MessageBox.Show("Export completed successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Optionally: Open file after export
                    if (MessageBox.Show("Do you want to open the file?", "Open File",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = fileName,
                            UseShellExecute = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during export: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImportFromExcel(string fileName)
        {
            try
            {
                // Set the license context
                OfficeOpenXml.ExcelPackage.License.SetNonCommercialPersonal("iAndreea02");

                using (var package = new ExcelPackage(new FileInfo(fileName)))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // First worksheet
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    // Skip metadata rows (first 3 rows)
                    int startRow = 4; // Assuming 3 rows of metadata + 1 header row

                    // Validate header row
                    var headers = new List<string>();
                    for (int col = 1; col <= colCount; col++)
                    {
                        string header = worksheet.Cells[startRow, col].Text.Trim();
                        headers.Add(header);
                    }

                    // Debugging: Log detected headers
                    string detectedHeaders = string.Join(", ", headers);
                    MessageBox.Show($"Anteturile detectate: {detectedHeaders}", "Debugging", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Verify required columns exist
                    var requiredColumns = new[] { "ID Orar", "Nume", "Gen", "Sala", "Teatru", "Data", "Ora", "Oras" };
                    foreach (var required in requiredColumns)
                    {
                        if (!headers.Contains(required))
                        {
                            MessageBox.Show($"Coloana obligatorie '{required}' lipsește din fișierul Excel.",
                                "Eroare Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Create DataTable
                    var dt = new DataTable();
                    foreach (var header in headers)
                    {
                        dt.Columns.Add(header);
                    }

                    // Read data
                    for (int row = startRow + 1; row <= rowCount; row++)
                    {
                        var dataRow = dt.NewRow();
                        bool hasData = false;

                        for (int col = 1; col <= colCount; col++)
                        {
                            var value = worksheet.Cells[row, col].Text.Trim();
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                hasData = true;
                            }
                            dataRow[col - 1] = value;
                        }

                        if (hasData) // Only add row if it contains data
                        {
                            dt.Rows.Add(dataRow);
                        }
                    }

                    // Validate data types and formats
                    foreach (DataRow row in dt.Rows)
                    {
                        if (!DateTime.TryParse(row["Data"].ToString(), out _))
                        {
                            MessageBox.Show($"Data invalidă la rândul cu ID Orar: {row["ID Orar"]}.", "Eroare Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (!TimeSpan.TryParse(row["Ora"].ToString(), out _))
                        {
                            MessageBox.Show($"Ora invalidă la rândul cu ID Orar: {row["ID Orar"]}.", "Eroare Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Confirm import
                    var result = MessageBox.Show(
                        $"Vor fi importate {dt.Rows.Count} înregistrări. Doriți să continuați?",
                        "Confirmare Import",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        ImportDataToDatabase(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la import: {ex.Message}", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImportDataToDatabase(DataTable dt)
        {
            int successCount = 0;
            int errorCount = 0;
            var errors = new List<string>();

            using (var cn = DatabaseHelper.OpenConnection())
            {
                using (var transaction = cn.BeginTransaction())
                {
                    try
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                // First, check if the spectacol exists
                                using (var cmdCheck = new MySqlCommand(
                                    "SELECT COUNT(*) FROM ORAR WHERE ID_ORAR = @ID_ORAR", cn))
                                {
                                    cmdCheck.Transaction = transaction;
                                    cmdCheck.Parameters.AddWithValue("@ID_ORAR", row["ID Orar"]);
                                    var exists = Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0;

                                    if (exists)
                                    {
                                        // Update existing record
                                        using (var cmdUpdate = new MySqlCommand(@"
                                        UPDATE ORAR SET 
                                            ID_SPEC = (SELECT id_spec FROM SPECTACOL WHERE NUME = @NUME_SPEC),
                                            ID_SALA = (SELECT id_sala FROM SALA WHERE NUME = @SALA),
                                            ORA = @ORA,
                                            DATA = @DATA
                                        WHERE ID_ORAR = @ID_ORAR", cn))
                                        {
                                            cmdUpdate.Transaction = transaction;
                                            cmdUpdate.Parameters.AddWithValue("@ID_ORAR", row["ID Orar"]);
                                            cmdUpdate.Parameters.AddWithValue("@NUME_SPEC", row["Nume"]);
                                            cmdUpdate.Parameters.AddWithValue("@SALA", row["Sala"]);
                                            cmdUpdate.Parameters.AddWithValue("@ORA", row["Ora"]);
                                            cmdUpdate.Parameters.AddWithValue("@DATA", DateTime.Parse(row["Data"].ToString()));

                                            cmdUpdate.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        // Insert new record
                                        using (var cmdInsert = new MySqlCommand(@"
                                        INSERT INTO ORAR (ID_ORAR, ID_SPEC, ID_SALA, ORA, DATA)
                                        VALUES (@ID_ORAR,
                                               (SELECT id_spec FROM SPECTACOL WHERE NUME = @NUME_SPEC),
                                               (SELECT id_sala FROM SALA WHERE NUME = @SALA),
                                               @ORA, @DATA)", cn))
                                        {
                                            cmdInsert.Transaction = transaction;
                                            cmdInsert.Parameters.AddWithValue("@ID_ORAR", row["ID Orar"]);
                                            cmdInsert.Parameters.AddWithValue("@NUME_SPEC", row["Nume"]);
                                            cmdInsert.Parameters.AddWithValue("@SALA", row["Sala"]);
                                            cmdInsert.Parameters.AddWithValue("@ORA", row["Ora"]);
                                            cmdInsert.Parameters.AddWithValue("@DATA", DateTime.Parse(row["Data"].ToString()));

                                            cmdInsert.ExecuteNonQuery();
                                        }
                                    }
                                    successCount++;
                                }
                            }
                            catch (Exception ex)
                            {
                                errorCount++;
                                errors.Add($"Eroare la rândul {successCount + errorCount}: {ex.Message}");
                            }
                        }

                        // If we have any errors, show them and rollback
                        if (errors.Count > 0)
                        {
                            var errorMessage = string.Join("\n", errors);
                            MessageBox.Show($"Au apărut {errorCount} erori la import:\n\n{errorMessage}",
                                "Erori la Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            transaction.Rollback();
                        }
                        else
                        {
                            // Commit the transaction if everything is OK
                            transaction.Commit();
                            MessageBox.Show($"Import finalizat cu succes! Au fost procesate {successCount} înregistrări.",
                                "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Log the import action
                            spectDb.LogActiune(currentUser.Id, currentUser.Role, "Import Excel",
                                "Multiple INSERT/UPDATE", $"Înregistrări importate: {successCount}");

                            // Refresh the display
                            LoadSpectacoleInTable();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Eroare la procesarea importului: {ex.Message}",
                            "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btImport_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Selectați fișierul Excel pentru import"
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ImportFromExcel(ofd.FileName);
                }
            }

        }

        private void btExport_Click(object sender, EventArgs e)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            using (var sfd = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                FileName = $"Spectacole_{timestamp}.xlsx",
                Title = "Salvare raport Excel"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportToExcel(sfd.FileName);
                }
            }
        }

        private void mainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirect to Main form
            Main mainForm = new Main(currentUser);
            mainForm.Show();
            this.Hide();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show an About dialog
            MessageBox.Show("This is the Admin Table for Proiect_Paoo.\nVersion 1.0", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show documentation or redirect to a documentation page
            MessageBox.Show("Documentation is available at: https://example.com/docs", "Documentation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(System.Text.NormalizationForm.FormD);
            var stringBuilder = new System.Text.StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }

        private void tableLayoutForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddSpect spec = new AddSpect(); // Pass the User object
            spec.Show();
            ///this.Hide();
        }
    }
}