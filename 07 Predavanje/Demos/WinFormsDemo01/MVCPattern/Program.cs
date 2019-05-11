using MVCPattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCPattern
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //ucitavanje podataka
            var repository = new KorisnikRepo(Application.StartupPath);
            var view = new Form1();

            // TODO: IOC
            var presenter = new Presenter.KorisnikPresenter(view, repository);

            Application.Run(view);
        }
    }
}
