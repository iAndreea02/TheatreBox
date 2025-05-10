using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;

namespace Proiect_Paoo
{
    public partial class BiletulMeu : Form
    {
        private readonly User currentUser; // Replace idUser with currentUser
        private readonly RezervareDB rezervareDB;

        public BiletulMeu(User user) // Update constructor to accept User
        {
            InitializeComponent();
            currentUser = user; // Assign the User object
            rezervareDB = new RezervareDB();

            // Setup DataGridView
            dataGridViewTickets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewTickets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewTickets.ReadOnly = true;

            // Setup MonthCalendar with custom drawing
            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar1.ShowToday = true;
            monthCalendar1.ShowTodayCircle = true;
            monthCalendar1.BoldedDates = new DateTime[] { };

            // Register event handlers
            this.Load += BiletulMeu_Load;
            monthCalendar1.DateSelected += MonthCalendar1_DateSelected;
        }

        private void BiletulMeu_Load(object sender, EventArgs e)
        {
            LoadUserTickets();
        }

        private void LoadUserTickets()
        {
            try
            {
                dataGridViewTickets.Rows.Clear();
                List<string> tickets = rezervareDB.AfisTicketUser(currentUser.Id); // Use currentUser.Id

                // Clear existing bolded dates
                monthCalendar1.RemoveAllBoldedDates();

                foreach (string ticket in tickets)
                {
                    string[] ticketDetails = ticket.Split('/');

                    // Ensure we have all 5 columns of data
                    if (ticketDetails.Length >= 5)
                    {
                        // Parse the date and time
                        if (DateTime.TryParseExact(ticketDetails[2],
                            new[] { "yyyy-MM-dd", "dd/MM/yyyy", "MM/dd/yyyy" }, // Accept multiple formats
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out DateTime ticketDate))
                        {
                            // Keep the date in YYYY-MM-DD format
                            ticketDetails[2] = ticketDate.ToString("yyyy-MM-dd");

                            // Format the time if needed
                            if (TimeSpan.TryParse(ticketDetails[3], out TimeSpan ticketTime))
                            {
                                ticketDetails[3] = ticketTime.ToString(@"hh\:mm");
                            }

                            // Add all columns to the grid
                            dataGridViewTickets.Rows.Add(
                                ticketDetails[0],  // Spectacol
                                ticketDetails[1],  // Teatru
                                ticketDetails[2],  // Data
                                ticketDetails[3],  // Ora
                                ticketDetails[4]   // Pozitie
                            );

                            // Add the date to calendar and set it as bolded
                            monthCalendar1.AddBoldedDate(ticketDate);

                            // Set the calendar's display to show the month of the first ticket
                            if (dataGridViewTickets.Rows.Count == 1)
                            {
                                monthCalendar1.SetDate(ticketDate);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Format invalid pentru bilet", "Eroare",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                // Update the calendar to show the bolded dates
                monthCalendar1.UpdateBoldedDates();

                if (tickets.Count == 0)
                {
                    MessageBox.Show("Nu există bilete pentru acest utilizator.", "Informație",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea biletelor: {ex.Message}", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MonthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            // Filter the grid to show only tickets for the selected date
            foreach (DataGridViewRow row in dataGridViewTickets.Rows)
            {
                if (row.Cells[2].Value != null)
                {
                    // Parse the date in YYYY-MM-DD format
                    if (DateTime.TryParseExact(row.Cells[2].Value.ToString(),
                        "yyyy-MM-dd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime ticketDate))
                    {
                        row.Visible = ticketDate.Date == e.Start.Date;
                    }
                }
            }
        }

        private void BiletulMeu_Load_1(object sender, EventArgs e)
        {

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
            MessageBox.Show("This is the My Tickets page for Proiect_Paoo.\nVersion 1.0", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show documentation or redirect to a documentation page
            MessageBox.Show("Documentation is available at: https://example.com/docs", "Documentation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
