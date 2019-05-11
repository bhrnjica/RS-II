using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPattern.Model
{
    public class Korisnik
    {
        public string Ime { get; set; }

        public string Adresa { get; set; }

        public string Telefon { get; set; }

        public override bool Equals(object obj)
        {
            Korisnik other = obj as Korisnik;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Ime.GetHashCode()
                ^ Adresa.GetHashCode()
                ^ Telefon.GetHashCode();
        }

        public bool Equals(Korisnik other)
        {
            if (other == null)
                return false;

            return this.Ime == other.Ime
                && this.Adresa == other.Adresa
                && this.Telefon == other.Telefon;
        }
    }
}
