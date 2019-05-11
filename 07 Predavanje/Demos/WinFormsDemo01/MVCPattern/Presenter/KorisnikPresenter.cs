using MVCPattern.Model;
using MVCPattern.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPattern.Presenter
{
    public class KorisnikPresenter
    {
        private readonly IKorisnikView _view;
        private readonly IKorisnikRepo _repository;

        public KorisnikPresenter(IKorisnikView view, IKorisnikRepo repository)
        {
            _view = view;
            view.Presenter = this;
            _repository = repository;

            UpdateKorisnikListView();
        }

        private void UpdateKorisnikListView()
        {
            var imenaKorisnika = from korisnik in _repository.GetSviKorisnici() select korisnik.Ime;
            int selectedKorisnik = _view.SelectedKorisnik >= 0 ? _view.SelectedKorisnik : 0;
            _view.KorisnikList = imenaKorisnika.ToList();
            _view.SelectedKorisnik = selectedKorisnik;
        }

        public void UpdateKorisnikView(int p)
        {
            // lista korisnika moze biti kesirana umjesto da sa uvijek ucitava
            // potrebno je vodoti racuna ako je lista velika
            Korisnik korisnik = _repository.GetKorisnik(p);
            _view.KorisnikIme = korisnik.Ime;
            _view.Adresa = korisnik.Adresa;
            _view.Telefon = korisnik.Telefon;
        }

        public void SaveCustomer()
        {
            Korisnik korisnik = new Korisnik { Ime = _view.KorisnikIme, Adresa = _view.Adresa, Telefon = _view.Telefon };
            _repository.SaveKorisnik(_view.SelectedKorisnik, korisnik);
            UpdateKorisnikListView();
        }
    }
}
