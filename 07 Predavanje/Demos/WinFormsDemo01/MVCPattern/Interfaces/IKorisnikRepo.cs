using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPattern.Model
{
    public interface IKorisnikRepo
    {
        IEnumerable<Korisnik> GetSviKorisnici();

        Korisnik GetKorisnik(int id);

        void SaveKorisnik(int id, Korisnik korisnik);
    }
}
