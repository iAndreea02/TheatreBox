using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using javax.xml.crypto;
using MySql.Data.MySqlClient;
using OfficeOpenXml;

namespace Proiect_Paoo
{
    public partial class Dashboard : Form
    {
        private int idUser;
        private string rol;
        private SpectDB sp;

        public Dashboard(int id_user, string nume, string rol)
        {
            InitializeComponent();
            this.idUser = id_user;
            this.rol = rol;
            this.sp = new SpectDB(idUser, rol);

            panelForm.Visible = false;

            LoadSpectacoleInTable();
            InitializeComboBoxes();
            InitializeRegizorComboBox(); // Add this line

        }

        private void LoadSpectacoleInTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Nume Spectacol");
            table.Columns.Add("Gen");
            table.Columns.Add("Sala");
            table.Columns.Add("Teatru");
            table.Columns.Add("Data"); // Separate column for Data
            table.Columns.Add("Ora");  // Separate column for Ora
            table.Columns.Add("Oras");

            try
            {
                var listaSpec = sp.AfisSpec();
                foreach (var spectacol in listaSpec)
                {
                    var columns = spectacol.Split('|'); // Use the updated delimiter
                    if (columns.Length == table.Columns.Count) // Ensure the number of fields matches the number of columns
                    {
                        table.Rows.Add(columns);
                    }
                    else
                    {
                        // Log or display an error for rows with missing fields
                        MessageBox.Show($"Eroare: Datele spectacolului nu corespund structurii tabelului. Date: {spectacol}", 
                                        "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea spectacolelor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dataGridViewSpectacole.DataSource = table;
        }

        private void InitializeComboBoxes()
        {
            comboBoxOras.Items.AddRange(new string[]
            {
                "--oras--", "Bucuresti", "Cluj-Napoca", "Timisoara", "Constanta", "Iasi"
            });
            comboBoxOras.SelectedIndexChanged += ComboBoxOras_SelectedIndexChanged;
        }

        private void ComboBoxOras_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedOras = comboBoxOras.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedOras) && selectedOras != "--oras--")
            {
                comboBoxTeatru.Items.Clear();
                var teatre = sp.AfisTeatru(selectedOras);
                if (teatre.Length > 0)
                {
                    comboBoxTeatru.Items.AddRange(teatre);
                }
                else
                {
                    comboBoxTeatru.Items.Add("Niciun teatru disponibil");
                }
            }
        }

        private void ComboBoxTeatru_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedTeatru = comboBoxTeatru.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedTeatru) && selectedTeatru != "Niciun teatru disponibil")
            {
                // Populate Sala ComboBox
                comboBoxSala.Items.Clear();
                var sali = sp.AfisSala(selectedTeatru);
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
                var spectacole = sp.AfisSpectacol();
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

        private void PopulateComboBoxTeatru(string oras)
        {
            var teatre = sp.AfisTeatru(oras);
            comboBoxTeatru.Items.Clear();

            if (teatre != null && teatre.Length > 0)
            {
                comboBoxTeatru.Items.AddRange(teatre);
            }
            else
            {
                comboBoxTeatru.Items.Add("Niciun teatru disponibil");
            }

            comboBoxTeatru.SelectedIndexChanged += (s, e) =>
            {
                var selectedTeatru = comboBoxTeatru.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedTeatru) && selectedTeatru != "Niciun teatru disponibil")
                {
                    PopulateComboBoxSala(selectedTeatru);
                }
                else
                {
                    comboBoxSala.Items.Clear();
                    comboBoxSala.Items.Add("Nicio sală disponibilă");
                }
            };
        }

        private void PopulateComboBoxSala(string teatru)
        {
            var sali = sp.AfisSala(teatru);
            comboBoxSala.Items.Clear();

            if (sali != null && sali.Length > 0)
            {
                comboBoxSala.Items.AddRange(sali);
            }
            else
            {
                comboBoxSala.Items.Add("Nicio sală disponibilă");
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var numeSpectacol = comboBoxSpectacol.SelectedItem?.ToString();
            var numeSala = comboBoxSala.SelectedItem?.ToString();
            var oraText = textBoxOra.Text;
            var dataSelectata = dateTimePickerData.Value;

            if (string.IsNullOrEmpty(numeSpectacol) || string.IsNullOrEmpty(numeSala) ||
                string.IsNullOrEmpty(oraText) || dataSelectata == null)
            {
                MessageBox.Show("Toate câmpurile sunt obligatorii.", "Eroare", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var dataFormatata = dataSelectata.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            sp.InserareOrar(numeSpectacol, numeSala, oraText, dataFormatata);
        }

        private void ButtonAddOrar_Click(object sender, EventArgs e)
        {
            // Get input values
            var numeSpectacol = comboBoxSpectacol.SelectedItem?.ToString();
            var numeSala = comboBoxSala.SelectedItem?.ToString();
            var oraText = textBoxOra.Text; // Example: "14:30:00"
            var dataSelectata = dateTimePickerData.Value; // Example: "2024-01-01"

            // Validate input fields
            if (string.IsNullOrEmpty(numeSpectacol) || string.IsNullOrEmpty(numeSala) ||
                string.IsNullOrEmpty(oraText) || dataSelectata == null)
            {
                MessageBox.Show("Toate câmpurile sunt obligatorii.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Format the date
            var dataFormatata = dataSelectata.ToString("yyyy-MM-dd");

            // Call the method to insert the schedule into the database
            sp.InserareOrar(numeSpectacol, numeSala, oraText, dataFormatata);

            // Refresh the table
            LoadSpectacoleInTable();
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            LoadSpectacoleInTable();
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
                    worksheet.Cells[headerRow, 1, headerRow, dataGridViewSpectacole.Columns.Count].Merge = true;
                    worksheet.Cells[headerRow, 1].Style.Font.Bold = true;
                    headerRow++;

                    worksheet.Cells[headerRow, 1].Value = $"Exported by: {Environment.UserName}";
                    worksheet.Cells[headerRow, 1, headerRow, dataGridViewSpectacole.Columns.Count].Merge = true;
                    worksheet.Cells[headerRow, 1].Style.Font.Bold = true;
                    headerRow++;
                    headerRow++; // Empty row for spacing

                    // Add headers
                    for (int i = 0; i < dataGridViewSpectacole.Columns.Count; i++)
                    {
                        var cell = worksheet.Cells[headerRow, i + 1];
                        cell.Value = dataGridViewSpectacole.Columns[i].HeaderText;

                        // Style header
                        cell.Style.Font.Bold = true;
                        cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(48, 84, 150));
                        cell.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    }

                    // Add data
                    for (int i = 0; i < dataGridViewSpectacole.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridViewSpectacole.Columns.Count; j++)
                        {
                            var cell = worksheet.Cells[i + headerRow + 1, j + 1];
                            var value = dataGridViewSpectacole.Rows[i].Cells[j].Value;
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
                    var tableRange = worksheet.Cells[headerRow, 1, headerRow + dataGridViewSpectacole.Rows.Count, dataGridViewSpectacole.Columns.Count];
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

        // Metodă pentru butonul de export
        private void ButtonExport_Click(object sender, EventArgs e)
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
        private void ButtonImport_Click(object sender, EventArgs e)
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
                        string header = worksheet.Cells[startRow, col].Text;
                        headers.Add(header);
                    }

                    // Verify required columns exist
                    var requiredColumns = new[] { "ID", "Nume Spectacol", "Gen", "Sala", "Teatru", "Data", "Ora", "Oras" };
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
                            var value = worksheet.Cells[row, col].Text;
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
                                    cmdCheck.Parameters.AddWithValue("@ID_ORAR", row["ID"]);
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
                                            cmdUpdate.Parameters.AddWithValue("@ID_ORAR", row["ID"]);
                                            cmdUpdate.Parameters.AddWithValue("@NUME_SPEC", row["Nume Spectacol"]);
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
                                            cmdInsert.Parameters.AddWithValue("@ID_ORAR", row["ID"]);
                                            cmdInsert.Parameters.AddWithValue("@NUME_SPEC", row["Nume Spectacol"]);
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
                            sp.LogActiune(this.idUser, this.rol, "Import Excel",
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

        private void ButtonSterge_Click(object sender, EventArgs e)
        {
            if (dataGridViewSpectacole.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewSpectacole.SelectedRows[0];
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
                                    cmdLog.Parameters.AddWithValue("@ID_UTILIZATOR", this.idUser);
                                    cmdLog.Parameters.AddWithValue("@ROL", this.rol);
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

                        // Logare eroare
                        using (var cmdLogError = new MySqlCommand(@"
                    INSERT INTO LOG_ACTIUNI 
                    (ID_UTILIZATOR, ROL, OPERATIE, COMANDA_SQL, DETALII_OPERATIE, DATA_OPERATIE) 
                    VALUES (@ID_UTILIZATOR, @ROL, @OPERATIE, @COMANDA_SQL, @DETALII_OPERATIE, UTC_TIMESTAMP())",
                            cn))
                        {
                            cmdLogError.Parameters.AddWithValue("@ID_UTILIZATOR", this.idUser);
                            cmdLogError.Parameters.AddWithValue("@ROL", this.rol);
                            cmdLogError.Parameters.AddWithValue("@OPERATIE", "Eroare ștergere spectacol");
                            cmdLogError.Parameters.AddWithValue("@COMANDA_SQL", "DELETE CASCADE");
                            cmdLogError.Parameters.AddWithValue("@DETALII_OPERATIE",
                                $"ID Orar: {idOrar}, Eroare: {ex.Message}");
                            cmdLogError.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewSpectacole.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewSpectacole.SelectedRows[0];

                // Retrieve data from the selected row
                var idOrar = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                var numeSpectacol = selectedRow.Cells["Nume Spectacol"].Value.ToString();
                var sala = selectedRow.Cells["Sala"].Value.ToString();
                var ora = selectedRow.Cells["Ora"].Value.ToString();
                var data = selectedRow.Cells["Data"].Value.ToString();
                var oras = selectedRow.Cells["Oras"].Value.ToString();
                var teatru = selectedRow.Cells["Teatru"].Value.ToString();

                // Populate the form fields
                comboBoxOras.Text = oras;
                PopulateComboBoxTeatru(oras); // Populate theaters for the selected city
                comboBoxTeatru.Text = teatru;
                comboBoxSala.Text = sala;
                comboBoxSpectacol.Text = numeSpectacol;
                textBoxOra.Text = ora;
                dateTimePickerData.Value = DateTime.Parse(data);

                // Show the form for editing
                panelForm.Visible = true;

                // Show only the "Save Changes" button
                ShowOnlyButton(ButtonSaveEdit);

                // Save changes when the user confirms
                ButtonSaveEdit.Click -= SaveEditHandler; // Detach any previous handlers
                ButtonSaveEdit.Click += SaveEditHandler;

                void SaveEditHandler(object s, EventArgs ev)
                {
                    var updatedNumeSpectacol = comboBoxSpectacol.Text;
                    var updatedSala = comboBoxSala.Text;
                    var updatedOra = textBoxOra.Text;
                    var updatedData = dateTimePickerData.Value.ToString("yyyy-MM-dd");

                    sp.UpdateOrar(idOrar, updatedNumeSpectacol, updatedSala, updatedOra, updatedData);

                    // Refresh the table
                    LoadSpectacoleInTable();
                    panelForm.Visible = false;

                    // Reset button visibility
                    ResetButtonVisibility();

                    MessageBox.Show("Modificările au fost salvate cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Selectați un rând pentru a edita.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonAddSpectacol_Click(object sender, EventArgs e)
        {
            ShowOnlyPanel(panel1); // Afișează doar panel1

            // Resetează câmpurile formularului
            comboBoxSpectacol.Text = string.Empty;
            comboBoxSala.Text = string.Empty;
            textBoxOra.Text = string.Empty;
            dateTimePickerData.Value = DateTime.Now;

            // Setează logica pentru adăugare orar
            buttonAddOrar.Click -= AddOrarHandler; // Detach any previous handlers
            buttonAddOrar.Click += AddOrarHandler;

            void AddOrarHandler(object s, EventArgs ev)
            {
                var numeSpectacol = comboBoxSpectacol.SelectedItem?.ToString();
                var numeSala = comboBoxSala.SelectedItem?.ToString();
                var oraText = textBoxOra.Text;
                var dataSelectata = dateTimePickerData.Value;

                if (string.IsNullOrEmpty(numeSpectacol) || string.IsNullOrEmpty(numeSala) ||
                    string.IsNullOrEmpty(oraText) || dataSelectata == null)
                {
                    MessageBox.Show("Toate câmpurile sunt obligatorii.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var dataFormatata = dataSelectata.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                sp.InserareOrar(numeSpectacol, numeSala, oraText, dataFormatata);

                // Reîmprospătare tabel
                LoadSpectacoleInTable();

                // Resetează vizibilitatea
                ShowOnlyPanel(spectacolPanel);

                MessageBox.Show("Orar adăugat cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void ShowOnlyButton(Button buttonToShow)
        {
            // Hide all buttons in the panelForm
            foreach (Control control in panelForm.Controls)
            {
                if (control is Button button)
                {
                    button.Visible = false;
                }
            }

            // Show the specified button
            buttonToShow.Visible = true;
        }

        private void ResetButtonVisibility()
        {
            // Show all buttons in the panelForm
            foreach (Control control in panelForm.Controls)
            {
                if (control is Button button)
                {
                    button.Visible = true;
                }
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void ButtonSaveEdit_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private class ComboBoxItem
        {
            public int Id { get; set; }
            public string Text { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void LoadDirectors()
        {
            try
            {
                comboBoxRegizor.Items.Clear();
                
                // Add default selection
                comboBoxRegizor.Items.Add(new ComboBoxItem { Id = -1, Text = "-- Selectați regizorul --" });
                
                var directors = sp.GetAllDirectors();
                foreach (var director in directors)
                {
                    comboBoxRegizor.Items.Add(new ComboBoxItem 
                    { 
                        Id = director.Id,
                        Text = $"{director.Nume} {director.Prenume}"
                    });
                }
                
                comboBoxRegizor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea regizorilor: {ex.Message}", 
                    "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxRegizor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRegizor.SelectedItem is ComboBoxItem item && item.Id != -1)
            {
                try
                {
                    // Here you can handle what happens when a director is selected
                    // For example, you might want to store the selected director ID
                    // for later use when saving the spectacol
                    int selectedDirectorId = item.Id;
                    // You can store this ID in a class field if needed
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la selectarea regizorului: {ex.Message}", 
                        "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InitializeRegizorComboBox()
        {
            LoadDirectors();
        }
        private void LoadGenres()
        {
            try
            {
                comboBox2.Items.Clear();
                comboBox2.Items.AddRange(SpectDB.GenreList);
                comboBox2.SelectedIndex = 0; // Select the first genre by default
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea genurilor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
    try
    {
        // Collect data from form fields
        string nume = textBox1.Text.Trim();
        string gen = comboBox2.SelectedItem?.ToString();
        decimal pretPers = numericUpDown1.Value;
        string descriere = textBox2.Text.Trim();

        // Get selected director ID
        int regizorId = -1;
        if (comboBoxRegizor.SelectedItem is ComboBoxItem item)
        {
            regizorId = item.Id;
        }

        // Validate input
        if (string.IsNullOrEmpty(nume))
        {
            MessageBox.Show("Introduceți numele spectacolului.", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrEmpty(gen) || gen == "-- Selectați genul --")
        {
            MessageBox.Show("Selectați genul spectacolului.", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (regizorId == -1)
        {
            MessageBox.Show("Selectați regizorul spectacolului.", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Call the insertion method
        sp.InserareSpectacol(nume, gen, pretPers, descriere, regizorId);

        // Notify success
        MessageBox.Show("Spectacolul a fost adăugat cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // Clear form fields
        textBox1.Clear();
        comboBox2.SelectedIndex = 0;
        numericUpDown1.Value = 0;
        textBox2.Clear();
        comboBoxRegizor.SelectedIndex = 0;

        // Refresh the spectacles table
        LoadSpectacoleInTable();
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Eroare la salvarea spectacolului: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

        private void button2_Click(object sender, EventArgs e)
        {
            HideAllPanels();

            // Set spectacolPanel to be visible
            spectacolPanel.Visible = true;

            // Clear form fields
            comboBoxSpectacol.Text = string.Empty;
            comboBoxSala.Text = string.Empty;
            textBoxOra.Text = string.Empty;
            dateTimePickerData.Value = DateTime.Now;

            // Show only the "Add Orar" button
            ShowOnlyButton(buttonAddOrar);

            // Save the new schedule when the user confirms
            buttonAddOrar.Click -= AddOrarHandler; // Detach any previous handlers
            buttonAddOrar.Click += AddOrarHandler;

            void AddOrarHandler(object s, EventArgs ev)
            {
                var numeSpectacol = comboBoxSpectacol.SelectedItem?.ToString();
                var numeSala = comboBoxSala.SelectedItem?.ToString();
                var oraText = textBoxOra.Text; // Example: "14:30:00"
                var dataSelectata = dateTimePickerData.Value; // Example: "2024-01-01"

                if (string.IsNullOrEmpty(numeSpectacol) || string.IsNullOrEmpty(numeSala) || string.IsNullOrEmpty(oraText) || dataSelectata == null)
                {
                    MessageBox.Show("Toate câmpurile sunt obligatorii.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var dataFormatata = dataSelectata.ToString("yyyy-MM-dd");

                sp.InserareOrar(numeSpectacol, numeSala, oraText, dataFormatata);

                // Refresh the table
                LoadSpectacoleInTable();
                spectacolPanel.Visible = false;

                // Reset button visibility
                ResetButtonVisibility();

                MessageBox.Show("Orar adăugat cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void HideAllPanels()
        {
            // Add all panels to be hidden here
            panelForm.Visible = false;
            spectacolPanel.Visible = false;
            // Add other panels if necessary
        }

        private void ShowOnlyPanel(Panel panelToShow)
        {
            // Ascunde toate panourile
            panel1.Visible = false;
            spectacolPanel.Visible = false;

            // Afișează doar panoul specificat
            panelToShow.Visible = true;
        }
        private void ButtonSaveEdit_Click_1(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridViewSpectacole_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }
