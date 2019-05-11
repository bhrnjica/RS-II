using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsKlijent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OnValidate_Event(object sender, CancelEventArgs e)
        {
            ValidateForm();
        }


        private bool ValidateForm()
        {
            bool bStatus = true;
            if (textBox1.Text == "")
            {
                errorProvider1.SetError(textBox1, "Unos polja za ime ne može biti prazan.");
                bStatus = false;
            }
            if (textBox2.Text == "")
            {
                errorProvider1.SetError(textBox2, "Unos polja za prezime ne može biti prazan.");
                bStatus = false;
            }
            if (dateTimePicker1.Text == "")
            {
                errorProvider1.SetError(dateTimePicker1, "Datum rodjenja ne može biti prazan.");
                bStatus = false;
            }
            if (comboBox1.Text != "Muški" && comboBox1.Text !="Ženski")
            {
                errorProvider1.SetError(comboBox1, "Nepoznat spol, molim unesite poznat spol.");
                bStatus = false;
            }
            
            return bStatus;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (ValidateForm() == false)
            {
                MessageBox.Show("Registracija ne može započeti, jer podaci nisu validni.", "WinForms Best Client Ever", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                errorProvider1.Clear();
                MessageBox.Show("Registracija uredno završena.", "WinForms Best Client Ever", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
