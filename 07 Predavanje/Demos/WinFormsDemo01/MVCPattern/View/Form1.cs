using MVCPattern.Presenter;
using MVCPattern.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCPattern
{
    public partial class Form1 : Form, IKorisnikView
    {
        private bool m_isEditMode = false;
        public Form1()
        {
            InitializeComponent();
        }
        #region Interface implementation
        public IList<string> KorisnikList
        {
            get { return (IList<string>)this.korisniciListBox.DataSource; }
            set { this.korisniciListBox.DataSource = value; }
        }

        public int SelectedKorisnik
        {
            get { return this.korisniciListBox.SelectedIndex; }
            set { this.korisniciListBox.SelectedIndex = value; }
        }

        public string Adresa
        {
            get { return this.adresaTextBox.Text; }
            set { this.adresaTextBox.Text = value; }
        }

        public string KorisnikIme
        {
            get { return this.imeTextBox.Text; }
            set { this.imeTextBox.Text = value; }
        }

        public string Telefon
        {
            get { return this.telefonTextBox.Text; }
            set { this.telefonTextBox.Text = value; }
        }

        public Presenter.KorisnikPresenter Presenter
        { private get; set; }
        #endregion


        private void customerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
            Presenter.UpdateKorisnikView(korisniciListBox.SelectedIndex);
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            this.adresaTextBox.ReadOnly = m_isEditMode;
            this.imeTextBox.ReadOnly = m_isEditMode;
            this.telefonTextBox.ReadOnly = m_isEditMode;

            m_isEditMode = !m_isEditMode;

            this.editButton.Text = m_isEditMode ? "Pohrani podatke" : "Izmjeni Podatke";
            //

            if (!m_isEditMode)
            {
                //ToDo
                //dodavanje validacija slicno prethodnom primjeru
                Presenter.SaveCustomer();
            }
        }
    }
}
