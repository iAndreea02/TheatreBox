using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;
using System.Globalization;

namespace Proiect_Paoo
{
    public class RezervareDB
    {
        public List<string> AfisSpec()
        {
            List<string> results = new List<string>();
            MySqlConnection cn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                cn = DatabaseHelper.OpenConnection();
                string query = @"SELECT O.ID_ORAR, S.NUME AS NUME_SPECTACOL, T.NUME AS TEATRU, 
                        O.ORA, O.DATA, S.pret_pers 
                        FROM SPECTACOL S 
                        JOIN ORAR O ON O.id_spec = S.id_spec 
                        JOIN SALA SA ON SA.id_sala = O.id_sala 
                        JOIN TEATRU T ON T.id_teatru = SA.id_teatru
                        ORDER BY O.DATA, O.ORA";

                cmd = new MySqlCommand(query, cn);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string idOrar = reader["ID_ORAR"].ToString();
                    string numeSpectacol = reader["NUME_SPECTACOL"].ToString();
                    string teatru = reader["TEATRU"].ToString();
                    string ora = reader["ORA"].ToString();
                    string data = Convert.ToDateTime(reader["DATA"]).ToShortDateString();
                    string pret = reader["pret_pers"].ToString();

                    string row = $"{idOrar}/{numeSpectacol}/{teatru}/{ora}/{data}/{pret}";
                    results.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la citirea spectacolelor: {ex.Message}", "Eroare");
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                if (cmd != null) cmd.Dispose();
                DatabaseHelper.CloseConnection();
            }

            return results;
        }

        public Dictionary<int, string> AfiseazaLocuriDisponibile(int idOrar)
        {
            Dictionary<int, string> locuriDisponibile = new Dictionary<int, string>();
            MySqlConnection cn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                cn = DatabaseHelper.OpenConnection();
                string query = @"SELECT L.ID_LOC, L.pozitie 
                               FROM LOC L 
                               JOIN ORAR O ON O.ID_SALA = L.ID_SALA 
                               WHERE L.ID_LOC NOT IN (
                                   SELECT ID_LOC FROM REZERVARE WHERE ID_ORAR = @IdOrar
                               ) AND O.ID_ORAR = @IdOrar
                               ORDER BY L.pozitie";

                cmd = new MySqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@IdOrar", idOrar);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int idLoc = Convert.ToInt32(reader["ID_LOC"]);
                    string pozitie = reader["pozitie"].ToString();
                    locuriDisponibile.Add(idLoc, pozitie);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea locurilor disponibile: {ex.Message}", "Eroare");
            }
            finally
            {
                if (reader != null) reader.Close();
                if (cmd != null) cmd.Dispose();
                DatabaseHelper.CloseConnection();
            }

            return locuriDisponibile;
        }

        public List<string> FiltreazaSpectacole(string numeSpectacol, string gen, bool locuriDisponibile)
        {
            List<string> results = new List<string>();
            MySqlConnection cn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
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

                cn = DatabaseHelper.OpenConnection();
                cmd = new MySqlCommand(queryBuilder.ToString(), cn);

                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);
                }

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string row = $"{reader["ID_ORAR"]}/{reader["NUME_SPECTACOL"]}/{reader["TEATRU"]}/{reader["ORA"]}/" +
                               $"{Convert.ToDateTime(reader["DATA"]).ToShortDateString()}/{reader["pret_pers"]}";
                    results.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la filtrarea spectacolelor: {ex.Message}", "Eroare");
            }
            finally
            {
                if (reader != null) reader.Close();
                if (cmd != null) cmd.Dispose();
                DatabaseHelper.CloseConnection();
            }

            return results;
        }

        public void EmiteBiletPDF(Dictionary<int, string> locuriSelectate, int idOrar, int idUser, string metodaPlata, int suma)
        {
            MySqlConnection cn = null;
            MySqlTransaction transaction = null;

            try
            {
                cn = DatabaseHelper.OpenConnection();
                transaction = cn.BeginTransaction();

                // Obține informațiile despre spectacol
                string querySpectacol = @"SELECT S.NUME, T.NUME AS TEATRU, O.DATA, O.ORA 
                                        FROM SPECTACOL S 
                                        JOIN ORAR O ON O.id_spec = S.id_spec 
                                        JOIN SALA SA ON SA.id_sala = O.id_sala 
                                        JOIN TEATRU T ON T.id_teatru = SA.id_teatru 
                                        WHERE O.ID_ORAR = @IdOrar";

                string numeSpectacol = "", teatru = "", ora = "";
                DateTime data = DateTime.Now;

                using (var cmdSpectacol = new MySqlCommand(querySpectacol, cn, transaction))
                {
                    cmdSpectacol.Parameters.AddWithValue("@IdOrar", idOrar);
                    using (var reader = cmdSpectacol.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            numeSpectacol = reader["NUME"].ToString();
                            teatru = reader["TEATRU"].ToString();
                            ora = reader["ORA"].ToString();
                            data = Convert.ToDateTime(reader["DATA"]);
                        }
                        reader.Close();
                    }
                }

                // Procesează fiecare loc selectat
                foreach (var loc in locuriSelectate)
                {
                    // Creează rezervarea
                    string queryRezervare = "INSERT INTO REZERVARE(id_orar, id_loc) VALUES (@IdOrar, @IdLoc); SELECT LAST_INSERT_ID();";
                    long idRezervare;
                    using (var cmdRezervare = new MySqlCommand(queryRezervare, cn, transaction))
                    {
                        cmdRezervare.Parameters.AddWithValue("@IdOrar", idOrar);
                        cmdRezervare.Parameters.AddWithValue("@IdLoc", loc.Key);
                        idRezervare = Convert.ToInt64(cmdRezervare.ExecuteScalar());
                    }

                    // Creează biletul
                    string queryBilet = @"INSERT INTO BILET (id_user, id_rezervare, metoda_plata, suma, data_vanzarii) 
                                        VALUES (@IdUser, @IdRezervare, @MetodaPlata, @Suma, NOW())";
                    using (var cmdBilet = new MySqlCommand(queryBilet, cn, transaction))
                    {
                        cmdBilet.Parameters.AddWithValue("@IdUser", idUser);
                        cmdBilet.Parameters.AddWithValue("@IdRezervare", idRezervare);
                        cmdBilet.Parameters.AddWithValue("@MetodaPlata", metodaPlata);
                        cmdBilet.Parameters.AddWithValue("@Suma", suma);
                        cmdBilet.ExecuteNonQuery();
                    }
                }

                // Generează PDF-ul
                string fileName = $"Bilet_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);
                    document.Open();

                    Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20);
                    Font subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                    Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                    // Adaugă conținutul PDF-ului
                    Paragraph title = new Paragraph("BILET SPECTACOL", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    title.SpacingAfter = 20f;
                    document.Add(title);

                    document.Add(new Paragraph($"Spectacol: {numeSpectacol}", subtitleFont));
                    document.Add(new Paragraph($"Teatru: {teatru}", normalFont));
                    document.Add(new Paragraph($"Data: {data.ToShortDateString()}", normalFont));
                    document.Add(new Paragraph($"Ora: {ora}", normalFont));
                    document.Add(new Paragraph("\n"));

                    document.Add(new Paragraph("Detalii bilet:", subtitleFont));
                    document.Add(new Paragraph($"Metoda plată: {metodaPlata}", normalFont));
                    document.Add(new Paragraph($"Preț per loc: {suma} RON", normalFont));
                    document.Add(new Paragraph($"Total: {suma * locuriSelectate.Count} RON", normalFont));
                    document.Add(new Paragraph("\nLocuri rezervate:", subtitleFont));

                    foreach (var loc in locuriSelectate)
                    {
                        document.Add(new Paragraph($"- Loc: {loc.Value}", normalFont));
                    }

                    document.Add(new Paragraph("\n"));
                    Paragraph footer = new Paragraph($"Data emiterii: {DateTime.Now:dd/MM/yyyy HH:mm}", normalFont);
                    footer.Alignment = Element.ALIGN_RIGHT;
                    document.Add(footer);

                    document.Close();
                }

                transaction.Commit();
                MessageBox.Show($"Biletul a fost emis cu succes!\nFișierul PDF a fost creat: {fileName}", "Succes");
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();
                throw new Exception($"Eroare la emiterea biletului: {ex.Message}");
            }
            finally
            {
                DatabaseHelper.CloseConnection();
            }

        }

        public List<string> AfisTicketUser(int idUser)
        {
            List<string> tickets = new List<string>();
            MySqlConnection cn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                cn = DatabaseHelper.OpenConnection();
                string query = @"SELECT S.NUME AS NUME_SPECT, T.NUME AS NUME_TEATRU, O.DATA, O.ORA, L.pozitie 
                        FROM BILET B 
                        JOIN REZERVARE R ON R.id_rezervare = B.id_rezervare 
                        JOIN ORAR O ON O.id_orar = R.id_orar 
                        JOIN SPECTACOL S ON S.id_spec = O.id_spec 
                        JOIN SALA SA ON SA.id_sala = O.id_sala 
                        JOIN TEATRU T ON T.id_teatru = SA.id_teatru 
                        JOIN LOC L ON L.ID_LOC = R.ID_LOC
                        WHERE B.id_user = @IdUser 
                        ORDER BY O.DATA, O.ORA";

                cmd = new MySqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@IdUser", idUser);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string numeSpectacol = reader["NUME_SPECT"].ToString();
                    string teatru = reader["NUME_TEATRU"].ToString();
                    DateTime data = Convert.ToDateTime(reader["DATA"]);
                    string ora = reader["ORA"].ToString();
                    string pozitie = reader["pozitie"].ToString();

                    string ticket = $"{numeSpectacol}/{teatru}/{data.ToString("yyyy-MM-dd")}/{ora}/{pozitie}";
                    tickets.Add(ticket);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea biletelor: {ex.Message}", "Eroare");
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                if (cmd != null) cmd.Dispose();
                DatabaseHelper.CloseConnection();
            }

            return tickets;
        }
    }
}