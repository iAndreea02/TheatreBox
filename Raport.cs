using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace Proiect_Paoo
{
    public partial class Raport : Form
    {
        User currentUser;
        public Raport(User currentUser)
        {   
            this.currentUser = currentUser;
            InitializeComponent();

            // Initialize the dataGridView
            //dataGridView = new DataGridView

            //Controls.Add(dataGridView);

            // Initialize the chart
            //
            // Add columns to the dataGridView
        
        }

        private void LoadReportData(DateTime startDate, DateTime endDate)
        {
            try
            {
                dataGridView.Rows.Clear();
                chart.Series.Clear();

                using (MySqlConnection connection = DatabaseHelper.OpenConnection())
                {
                    string query = @"
                        SELECT S.NUME AS Spectacol, T.NUME AS Teatru, SA.NUME AS Sala, T.ORAS AS Oras,
                               O.DATA AS Data, O.ORA AS Ora, COUNT(R.ID_REZERVARE) AS Rezervari,
                               COALESCE(S.PRET_PERS, 0) * COUNT(R.ID_REZERVARE) AS TotalEarnings
                        FROM SPECTACOL S
                        JOIN ORAR O ON O.ID_SPEC = S.ID_SPEC
                        JOIN SALA SA ON SA.ID_SALA = O.ID_SALA
                        JOIN TEATRU T ON T.ID_TEATRU = SA.ID_TEATRU
                        LEFT JOIN REZERVARE R ON R.ID_ORAR = O.ID_ORAR
                        WHERE O.DATA BETWEEN @StartDate AND @EndDate
                        GROUP BY S.NUME, T.NUME, SA.NUME, T.ORAS, O.DATA, O.ORA, S.PRET_PERS
                        ORDER BY O.DATA, O.ORA";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            string selectedChartType = comboBoxChartType.SelectedItem?.ToString() ?? "Bar";
                            SeriesChartType chartType = selectedChartType == "Pie" 
                                ? SeriesChartType.Pie 
                                : SeriesChartType.Column;

                            Series series = new Series("Rezervări")
                            {
                                ChartType = chartType,
                                XValueType = ChartValueType.String
                            };
                            chart.Series.Add(series);

                            int totalRezervari = 0;
                            var dataPoints = new List<(string Spectacol, int Rezervari)>();

                            while (reader.Read())
                            {
                                string spectacol = reader["Spectacol"]?.ToString() ?? "N/A";
                                int rezervari = int.Parse(reader["Rezervari"]?.ToString() ?? "0");
                                totalRezervari += rezervari;

                                dataPoints.Add((spectacol, rezervari));

                                string teatru = reader["Teatru"]?.ToString() ?? "N/A";
                                string sala = reader["Sala"]?.ToString() ?? "N/A";
                                string oras = reader["Oras"]?.ToString() ?? "N/A";
                                string data = reader["Data"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["Data"]).ToShortDateString()
                                    : "N/A";
                                string ora = reader["Ora"]?.ToString() ?? "N/A";
                                string totalEarnings = reader["TotalEarnings"]?.ToString() ?? "0";

                                dataGridView.Rows.Add(spectacol, teatru, sala, oras, data, ora, rezervari, totalEarnings);
                            }

                            foreach (var (spectacol, rezervari) in dataPoints)
                            {   
                                double percentage = totalRezervari > 0 ? (rezervari / (double)totalRezervari) * 100 : 0;
                                int pointIndex = series.Points.AddXY(spectacol, rezervari);
                                var point = series.Points[pointIndex]; // Access the DataPoint object
                                point.Label = $"{spectacol} - {rezervari} ({percentage:F2}%)";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea datelor raportului: {ex.Message}", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePickerStart.Value.Date;
            DateTime endDate = dateTimePickerEnd.Value.Date;

            if (startDate > endDate)
            {
                MessageBox.Show("Data de început nu poate fi mai mare decât data de sfârșit.", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadReportData(startDate, endDate);
        }

        private void Raport_Load(object sender, EventArgs e)
        {
            // Set default date range
            dateTimePickerStart.Value = DateTime.Now.AddMonths(-1);
            dateTimePickerEnd.Value = DateTime.Now;
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
            MessageBox.Show("This is the Raport form for Proiect_Paoo.\nVersion 1.0", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show documentation or redirect to a documentation page
            MessageBox.Show("Documentation is available at: https://example.com/docs", "Documentation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}