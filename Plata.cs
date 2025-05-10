using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_Paoo
{
    public partial class Plata : Form
    {
        private Dictionary<int, string> locuriSelectate;
        private int idOrar;
        private int idUser;
        private int pretPers;
        private string metodaPlata = "Card"; // Default payment method

        private RezervareDB rezervareDB;

        public Plata(Dictionary<int, string> locuriSelectate, int idOrar, int idUser, int pretPers)
        {
            InitializeComponent();
            this.locuriSelectate = locuriSelectate;
            this.idOrar = idOrar;
            this.idUser = idUser;
            this.pretPers = pretPers;

            rezervareDB = new RezervareDB();

            // Initialize UI elements
           
            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            try
            {
                if (locuriSelectate == null || locuriSelectate.Count == 0)
                {
                    throw new InvalidOperationException("Nu există locuri selectate pentru calcularea prețului total.");
                }

                int totalPrice = locuriSelectate.Count * pretPers;
                labelTotalPrice.Text = $"Total: {totalPrice} RON";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la actualizarea prețului total: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (locuriSelectate == null || locuriSelectate.Count == 0)
                {
                    MessageBox.Show("Nu ați selectat locuri pentru rezervare.", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(metodaPlata))
                {
                    MessageBox.Show("Nu ați selectat metoda de plată.", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Calculate the total price
                int totalPrice = locuriSelectate.Count * pretPers;

                // Debug message to confirm parameters
                Console.WriteLine($"Calling EmiteBiletPDF with parameters: locuriSelectate={locuriSelectate.Count}, idOrar={idOrar}, idUser={idUser}, metodaPlata={metodaPlata}, totalPrice={totalPrice}");

                // Call EmiteBiletPDF to generate the ticket
                rezervareDB.EmiteBiletPDF(locuriSelectate, idOrar, idUser, metodaPlata, totalPrice);

                // Notify the user of success
                MessageBox.Show("Plata a fost realizată cu succes și biletul a fost emis!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Close the payment form
                this.Close();
            }
            catch (Exception ex)
            {
                // Notify the user of any errors
                MessageBox.Show($"Eroare la procesarea plății: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            metodaPlata = "Cash";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            metodaPlata = "Card";
        }

        private void Plata_Load(object sender, EventArgs e)
        {

        }
    }
}
