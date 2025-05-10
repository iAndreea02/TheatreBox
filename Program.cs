using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

namespace Proiect_Paoo
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Main(new User(1, "Radu", "Andreea", "ra@yahoo.com", "admin"))); 
            //Application.Run(new Form1(1));
            // Application.Run(new BiletulMeu(1));
            // Application.Run(new AdminTable(1,"admin"));
            //Application.Run(new Raport(new User(1, "Radu", "Andreea", "ra@yahoo.com", "admin")));
            Application.Run(new Login());
           // Application.Run(new AddSpect());
        }
    }
}
