using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MVCPattern.Model
{
    public class KorisnikRepo : IKorisnikRepo
    {
        private readonly string _xmlFilePath;
        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(List<Korisnik>));
        private readonly Lazy<List<Korisnik>> _korisnici;

        public KorisnikRepo(string fullPath)
        {
            _xmlFilePath = fullPath + @"\customers.xml";

            if (!File.Exists(_xmlFilePath))
                CreateKorisnikXmlStub();

            _korisnici = new Lazy<List<Korisnik>>(() =>
            {
                using (var reader = new StreamReader(_xmlFilePath))
                {
                    return (List<Korisnik>)_serializer.Deserialize(reader);
                }
            });
        }

        private void CreateKorisnikXmlStub()
        {
            var stubKorisnikList = new List<Korisnik> {
                new Korisnik {Ime = "Deni", Adresa = "Mostar, BiH 88000", Telefon = "123-456"},
                new Korisnik {Ime = "Dani", Adresa = "Mostar, BiH 88000", Telefon = "124-456"},
                new Korisnik {Ime = "Danny", Adresa = "Mostar, BiH 88000", Telefon = "125-456"}
            };
            SaveKorisnikList(stubKorisnikList);
        }

        private void SaveKorisnikList(List<Korisnik> customers)
        {
            using (var writer = new StreamWriter(_xmlFilePath, false))
            {
                _serializer.Serialize(writer, customers);
            }
        }

        public IEnumerable<Korisnik> GetSviKorisnici()
        {
            return _korisnici.Value;
        }

        public Korisnik GetKorisnik(int id)
        {
            return _korisnici.Value[id];
        }

        public void SaveKorisnik(int id, Korisnik korisnik)
        {
            _korisnici.Value[id] = korisnik;
            SaveKorisnikList(_korisnici.Value);
        }

       
    }
}
