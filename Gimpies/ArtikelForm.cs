using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gimpies
{
    public partial class ArtikelForm : Form
    {
        Main mainForm;
        long id;
        bool add = true;
        public ArtikelForm(Main _mainForm, long _id = -1)
        {
            InitializeComponent();

            mainForm = _mainForm;
            id = _id;
            if (id != -1)
            {
                add = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toevoegBtn_Click(object sender, EventArgs e)
        {
            if (add)
            {
                mainForm.ArtikelToevoegen(beschrijvingTextbox.Text, aantalTextbox.Text, prijsTextbox.Text, maatTextbox.Text);
            }
            else
            {
                mainForm.ArtikelBewerken(id,beschrijvingTextbox.Text,aantalTextbox.Text,prijsTextbox.Text,maatTextbox.Text);
            }
            this.Close();
        }

        private void ArtikelForm_Load(object sender, EventArgs e)
        {
            if (add==false)
            {
                toevoegBtn.Text = "Opslaan";
                Voorraad item = mainForm.GetItem(id);
                beschrijvingTextbox.Text = item.ItemDesc;
                aantalTextbox.Text = item.ItemAmount.ToString();
                prijsTextbox.Text = item.ItemPrijs;
                maatTextbox.Text = item.ItemMaat.ToString();
            }
            else
            {
                toevoegBtn.Text = "Toevoegen";
            }
        }
    }
}
