using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPattern.View
{
    public interface IKorisnikView
    {
        IList<string> KorisnikList { get; set; }

        int SelectedKorisnik { get; set; }

        string KorisnikIme { get; set; }

        string Adresa { get; set; }

        string Telefon { get; set; }

        Presenter.KorisnikPresenter Presenter { set; }
    }
}
