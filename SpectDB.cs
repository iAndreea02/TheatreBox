using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace Proiect_Paoo
{
    public class SpectDB
    {
        private readonly int idUser;
        private readonly string rol; public static readonly string[] GenreList = new[]
    {
        "Dramă",
        "Comedie",
        "Tragedie",
        "Musical",
        "Operă",
        "Balet",
        "Teatru experimental",
        "Pantomimă",
        "Teatru pentru copii",
        "Teatru clasic",
        "Teatru contemporan",
        "Teatru absurd",
        "Operetă",
        "Revistă",
        "Improvizație"
    };

        // Constructor
        public SpectDB(int idUser, string rol)
        {
            this.idUser = idUser;
            this.rol = rol;
        }

        private int GenerateNextIdSpec()
        {
            int nextId = 1; // Default to 1 if no records exist

            using (var cn = DatabaseHelper.OpenConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand("SELECT MAX(ID_SPEC) FROM SPECTACOL", cn))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            nextId = Convert.ToInt32(result) + 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la generarea ID-ului pentru spectacol: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return nextId;
        }



        #region Logging Operations

        public void LogActiune(int idUtilizator, string rol, string operatie, string comandaSQL, string detaliiOperatie)
        {
            using (var cn = DatabaseHelper.OpenConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand(@"
                        INSERT INTO LOG_ACTIUNI 
                        (ID_UTILIZATOR, ROL, OPERATIE, COMANDA_SQL, DETALII_OPERATIE, DATA_OPERATIE) 
                        VALUES (@ID_UTILIZATOR, @ROL, @OPERATIE, @COMANDA_SQL, @DETALII_OPERATIE, UTC_TIMESTAMP())", cn))
                    {
                        cmd.Parameters.AddWithValue("@ID_UTILIZATOR", idUtilizator);
                        cmd.Parameters.AddWithValue("@ROL", rol);
                        cmd.Parameters.AddWithValue("@OPERATIE", operatie);
                        cmd.Parameters.AddWithValue("@COMANDA_SQL", comandaSQL);
                        cmd.Parameters.AddWithValue("@DETALII_OPERATIE", detaliiOperatie);

                        cmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    DatabaseHelper.CloseConnection();
                }
            }
        }

        #endregion

        #region Display Operations

        public string[] AfisTeatru(string oras)
        {
            var listaTeatre = new List<string>();
            using (var cn = DatabaseHelper.OpenConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand("SELECT NUME FROM TEATRU WHERE ORAS = @ORAS", cn))
                    {
                        cmd.Parameters.AddWithValue("@ORAS", oras);
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                listaTeatre.Add(reader.GetString("NUME"));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la încărcarea teatrelor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return listaTeatre.ToArray();
        }

        public string[] AfisSala(string teatru)
        {
            var listaSala = new List<string>();
            using (var cn = DatabaseHelper.OpenConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand(@"
                        SELECT S.NUME 
                        FROM SALA S 
                        JOIN TEATRU T ON T.ID_TEATRU = S.ID_TEATRU 
                        WHERE T.NUME = @TEATRU", cn))
                    {
                        cmd.Parameters.AddWithValue("@TEATRU", teatru);
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                listaSala.Add(reader.GetString("NUME"));
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Eroare la interogarea sălilor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return listaSala.ToArray();
        }

        public string[] 
            
            
            AfisSpectacol()
        {
            var listaSpectacole = new List<string>();
            using (var cn = DatabaseHelper.OpenConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand("SELECT NUME FROM SPECTACOL", cn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaSpectacole.Add(reader.GetString("NUME"));
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    //MessageBox.Show($"Eroare la interogarea spectacolelor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    DatabaseHelper.CloseConnection();
                }
            }
            return listaSpectacole.Count > 0 ? listaSpectacole.ToArray() : new[] { "Niciun spectacol disponibil" };
        }

        #endregion

        #region File Operations

        public void OpenFile(string file)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = file,
                    UseShellExecute = true
                });
            }
            catch (Exception e)
            {
                MessageBox.Show($"Eroare la deschiderea fișierului: {e.Message}", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcelFromDatabase()
        {
            using (var sfd = new SaveFileDialog { Filter = "Excel Files|*.xlsx" })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;

                using (var cn = DatabaseHelper.OpenConnection())
                {
                    try
                    {
                        using (var cmd = new MySqlCommand("SELECT * FROM SPECTACOL", cn))
                        using (var reader = cmd.ExecuteReader())
                        {
                            using (var package = new ExcelPackage())
                            {
                                var worksheet = package.Workbook.Worksheets.Add("Spectacole");

                                // Write headers
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    worksheet.Cells[1, i + 1].Value = reader.GetName(i);
                                }

                                // Write data
                                int row = 2;
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        worksheet.Cells[row, i + 1].Value = reader.GetValue(i)?.ToString();
                                    }
                                    row++;
                                }

                                string fileName = sfd.FileName;
                                if (!fileName.EndsWith(".xlsx")) fileName += ".xlsx";
                                package.SaveAs(new FileInfo(fileName));
                                OpenFile(fileName);
                            }
                        }

                        LogActiune(idUser, rol, "Export Excel", "Export", "Export complet");
                        MessageBox.Show("Export realizat cu succes!", "Succes",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Eroare la export: {ex.Message}", "Eroare",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        DatabaseHelper.CloseConnection();
                    }
                }
            }
        }

        public List<string> AfisSpec()
        {
            var listaSpec = new List<string>();
            using (var cn = DatabaseHelper.OpenConnection())
            {
                try
                {
                    string query = @"SELECT O.ID_ORAR, S.NUME, S.GEN, SA.NUME AS SALA, T.NUME AS TEATRU, 
                                            DATE_FORMAT(O.DATA, '%Y-%m-%d') AS DATA, 
                                            TIME_FORMAT(O.ORA, '%H:%i') AS ORA, 
                                            T.ORAS 
                                     FROM SPECTACOL S 
                                     JOIN ORAR O ON O.id_spec = S.id_spec 
                                     JOIN SALA SA ON SA.id_sala = O.id_sala 
                                     JOIN TEATRU T ON T.id_teatru = SA.id_teatru";
                    using (var cmd = new MySqlCommand(query, cn))
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var idOrar = reader["ID_ORAR"] != DBNull.Value ? reader["ID_ORAR"].ToString() : "0";
                            var nume = reader["NUME"]?.ToString().Replace("|", " ") ?? string.Empty;
                            var gen = reader["GEN"]?.ToString().Replace("|", " ") ?? string.Empty;
                            var sala = reader["SALA"]?.ToString().Replace("|", " ") ?? string.Empty;
                            var teatru = reader["TEATRU"]?.ToString().Replace("|", " ") ?? string.Empty;
                            var data = reader["DATA"]?.ToString() ?? string.Empty;
                            var ora = reader["ORA"]?.ToString() ?? string.Empty;
                            var oras = reader["ORAS"]?.ToString().Replace("|", " ") ?? string.Empty;

                            var rand = $"{idOrar}|{nume}|{gen}|{sala}|{teatru}|{data}|{ora}|{oras}";
                            listaSpec.Add(rand);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Eroare la interogarea bazei de date: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return listaSpec;
        }

        public async Task<List<string>> AfisSpecAsync()
        {
            return await Task.Run(() => AfisSpec());
        }

        public void InserareOrar(string numeSpec, string numeSala, string ora, string data)
        {
            // Updated validation to match the new maximum length for VARCHAR columns
            if (numeSpec.Length > 255) // Assuming 255 is the new max length for the column
            {
                MessageBox.Show("Numele spectacolului este prea lung. Maxim 255 caractere.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (numeSala.Length > 255) // Assuming 255 is the new max length for the column
            {
                MessageBox.Show("Numele sălii este prea lung. Maxim 255 caractere.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ora.Length > 8) // Assuming 'HH:mm:ss' format
            {
                MessageBox.Show("Ora este într-un format invalid.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (data.Length > 10) // Assuming 'YYYY-MM-DD' format
            {
                MessageBox.Show("Data este într-un format invalid.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsSpectacol(numeSala, ora, DateTime.Parse(data)))
            {
                MessageBox.Show("Există deja un spectacol la această oră, dată și sală.", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var cn = DatabaseHelper.OpenConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand(@"
                        INSERT INTO ORAR (id_spec, id_sala, ora, data) 
                        VALUES (
                            (SELECT id_spec FROM SPECTACOL WHERE nume = @NUME_SPEC),
                            (SELECT id_sala FROM SALA WHERE nume = @NUME_SALA),
                            @ORA, @DATA)", cn))
                    {
                        cmd.Parameters.AddWithValue("@NUME_SPEC", numeSpec);
                        cmd.Parameters.AddWithValue("@NUME_SALA", numeSala);
                        cmd.Parameters.AddWithValue("@ORA", ora);
                        cmd.Parameters.AddWithValue("@DATA", DateTime.Parse(data));

                        cmd.ExecuteNonQuery();
                        LogActiune(idUser, rol, "Inserare Orar", "INSERT", $"Spectacol: {numeSpec}, Sala: {numeSala}");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Eroare la inserarea orarului: {ex.Message}\nComanda SQL: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool IsSpectacol(string numeSala, string ora, DateTime data)
        {
            using (var cn = DatabaseHelper.OpenConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand(@"
                        SELECT COUNT(*) FROM ORAR 
                        WHERE ID_SALA = (SELECT id_sala FROM SALA WHERE nume = @NUME_SALA) 
                        AND ORA = @ORA AND DATA = @DATA", cn))
                    {
                        cmd.Parameters.AddWithValue("@NUME_SALA", numeSala);
                        cmd.Parameters.AddWithValue("@ORA", ora);
                        cmd.Parameters.AddWithValue("@DATA", data);

                        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    }
                }
                finally
                {
                    DatabaseHelper.CloseConnection();
                }
            }
        }

        public void UpdateOrar(int idOrar, string numeSpec, string numeSala, string ora, string data)
        {
            using (var cn = DatabaseHelper.OpenConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand(@"
                        UPDATE ORAR 
                        SET 
                            ID_SPEC = (SELECT id_spec FROM SPECTACOL WHERE nume = @NUME_SPEC),
                            ID_SALA = (SELECT id_sala FROM SALA WHERE nume = @NUME_SALA),
                            ORA = @ORA, 
                            DATA = @DATA
                        WHERE ID_ORAR = @ID_ORAR", cn))
                    {
                        cmd.Parameters.AddWithValue("@ID_ORAR", idOrar);
                        cmd.Parameters.AddWithValue("@NUME_SPEC", numeSpec);
                        cmd.Parameters.AddWithValue("@NUME_SALA", numeSala);
                        cmd.Parameters.AddWithValue("@ORA", ora);
                        cmd.Parameters.AddWithValue("@DATA", DateTime.Parse(data));

                        cmd.ExecuteNonQuery();
                        LogActiune(idUser, rol, "Editare Orar", "EDIT", $"ID Orar: {idOrar}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la actualizarea orarului: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void InserareSpectacol(string nume, string gen, decimal pretPers, string descriere)
        {
            using (var cn = DatabaseHelper.OpenConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand(@"
                        INSERT INTO SPECTACOL (NUME, GEN, PRET_PERS, DESCRIERE) 
                        VALUES (@NUME, @GEN, @PRET_PERS, @DESCRIERE)", cn))
                    {
                        cmd.Parameters.AddWithValue("@NUME", nume);
                        cmd.Parameters.AddWithValue("@GEN", gen);
                        cmd.Parameters.AddWithValue("@PRET_PERS", pretPers);
                        cmd.Parameters.AddWithValue("@DESCRIERE", descriere);

                        cmd.ExecuteNonQuery();
                        LogActiune(idUser, rol, "Inserare Spectacol", "INSERT", $"Spectacol: {nume}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la inserarea spectacolului: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void InserareSpectacol(string nume, string gen, decimal pretPers, string descriere, int idRegizor)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(nume))
                throw new ArgumentException("Numele spectacolului nu poate fi gol.");
            if (string.IsNullOrWhiteSpace(gen))
                throw new ArgumentException("Genul spectacolului nu poate fi gol.");
            if (pretPers <= 0)
                throw new ArgumentException("Prețul per persoană trebuie să fie un număr pozitiv.");
            if (string.IsNullOrWhiteSpace(descriere))
                throw new ArgumentException("Descrierea spectacolului nu poate fi goală.");
            if (idRegizor <= 0)
                throw new ArgumentException("ID-ul regizorului trebuie să fie un număr valid.");

            using (var cn = DatabaseHelper.OpenConnection())
            {
                try
                {
                    int idSpec = GenerateNextIdSpec();
                    using (var cmd = new MySqlCommand(@"
                        INSERT INTO SPECTACOL (NUME, GEN, PRET_PERS, DESCRIERE, ID_REGIZOR) 
                        VALUES (@NUME, @GEN, @PRET_PERS, @DESCRIERE, @ID_REGIZOR)", cn))
                    {
                        cmd.Parameters.AddWithValue("@ID_SPEC", idSpec);
                        cmd.Parameters.AddWithValue("@NUME", nume);
                        cmd.Parameters.AddWithValue("@GEN", gen);
                        cmd.Parameters.AddWithValue("@PRET_PERS", pretPers);
                        cmd.Parameters.AddWithValue("@DESCRIERE", descriere);
                        cmd.Parameters.AddWithValue("@ID_REGIZOR", idRegizor);

                        cmd.ExecuteNonQuery();
                        LogActiune(idUser, rol, "Inserare Spectacol", 
                            "INSERT", $"Spectacol: {nume}, Regizor ID: {idRegizor}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Eroare la inserarea spectacolului: {ex.Message}");
                }
            }
        }

        public void UpdateSpectacolRegizor(int spectacolId, int regizorId)
        {
            using (var cn = DatabaseHelper.OpenConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand(@"
                        UPDATE SPECTACOL 
                        SET ID_REGIZOR = @ID_REGIZOR 
                        WHERE ID_SPEC = @ID_SPEC", cn))
                    {
                        cmd.Parameters.AddWithValue("@ID_REGIZOR", regizorId);
                        cmd.Parameters.AddWithValue("@ID_SPEC", spectacolId);
                        cmd.ExecuteNonQuery();
                        
                        LogActiune(idUser, rol, "Update Spectacol Regizor", 
                            cmd.CommandText, $"Spectacol ID: {spectacolId}, Regizor ID: {regizorId}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Eroare la actualizarea regizorului pentru spectacol: {ex.Message}");
                }
            }
        }

        public List<(int Id, string Nume, string Prenume, DateTime DataNasterii)> GetAllDirectors()
        {
            var directors = new List<(int, string, string, DateTime)>();
            using (var cn = DatabaseHelper.OpenConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand("SELECT id_regizor, nume, prenume, data_nasterii FROM regizori", cn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            directors.Add((
                                reader.GetInt32("id_regizor"),
                                reader.GetString("nume"),
                                reader.GetString("prenume"),
                                reader.GetDateTime("data_nasterii")
                            ));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error fetching directors: {ex.Message}");
                }
            }
            return directors;
        }

        #endregion
    }
}