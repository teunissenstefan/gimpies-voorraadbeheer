using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gimpies
{
    public partial class Main : MForm
    {
        Global globalClass = new Global();

        List<Voorraad> voorraad = new List<Voorraad>();
        string naam = "";
        Form loginForm;
        public Main(string _naam, Form _loginForm)
        {
            InitializeComponent();

            naam = _naam;
            loginForm = _loginForm;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = "Voorraadbeheer | Welkom: "+naam+"!";
            Populate();
            this.CloseAppOnClose = true;
        }

        private void Populate(string zoekterm = "")
        {
            artikelenList.Items.Clear();
            if (zoekterm == "")
            {
                voorraad = globalClass.MysqlServerLoadArtikelen();
                foreach (var row in voorraad)
                {
                    var item = new ListViewItem(row.ItemID.ToString());
                    item.SubItems.Add(row.ItemDesc.ToString());
                    item.SubItems.Add(row.ItemAmount.ToString());
                    item.SubItems.Add(row.ItemMaat.ToString());
                    item.SubItems.Add(row.ItemPrijs.ToString());
                    artikelenList.Items.Add(item);
                }
            }
            else
            {
                //zoeken
                CultureInfo culture = CultureInfo.CurrentCulture;
                var zoekQuery = voorraad.FindAll(r => culture.CompareInfo.IndexOf(r.ItemDesc, zoekterm,CompareOptions.IgnoreCase) >= 0);
                foreach (var row in zoekQuery)
                {
                    var item = new ListViewItem(row.ItemID.ToString());
                    item.SubItems.Add(row.ItemDesc.ToString());
                    item.SubItems.Add(row.ItemAmount.ToString());
                    item.SubItems.Add(row.ItemMaat.ToString());
                    item.SubItems.Add(row.ItemPrijs.ToString());
                    artikelenList.Items.Add(item);
                }
            }
        }

        private void afsluitenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void uitloggenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Form> oForms = new List<Form>();
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name != "Login" && openForm.Name != "Main")
                {
                    oForms.Add(openForm);
                }

            }
            foreach (Form openForm in oForms)
            {
                openForm.Close();
            }
            loginForm.Show();
            this.CloseAppOnClose = false;
            this.Close();
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Zoeken in voorraad en dan tijdelijke lijst maken
                searchTextbox.SelectAll();
                Populate(searchTextbox.Text);
            }
            if (searchTextbox.Text == "")
            {
                //Als hij leeg is de normale voorraad lijst laden
                Populate();
            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            searchTextbox.SelectAll();
        }

        int closeAmount = 1;
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closeAmount == 1)
            { 
                globalClass.CheckOut(naam);
            }
            closeAmount--;
            if (this.CloseAppOnClose)
            {
                Application.Exit();
            }
        }

        private void artikelenList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Gimpies.ArtikelForm artikelForm = new Gimpies.ArtikelForm(this, Int64.Parse(artikelenList.SelectedItems[0].Text));
            artikelForm.Show();
            artikelForm.TopMost = true;
        }

        public void ArtikelToevoegen(string beschrijving, string aantal, string prijs, string maat)
        {
            //Toevoegen en list repopulaten
            globalClass.MysqlServerInsert(beschrijving,aantal,prijs,maat);
            Populate();
        }

        public void ArtikelBewerken(long id, string beschrijving, string aantal, string prijs, string maat)
        {
            //Bewerkenen en repopulaten
            bool itemBestaat = voorraad.Any(r => r.ItemID == id);
            if (itemBestaat)
            {
                globalClass.MysqlServerUpdate(id,beschrijving,aantal,prijs,maat);
                Populate();
            }
            else
            {
                MessageBox.Show("Artikel bestaat niet");
            }
        }

        private void artikelToevoegenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gimpies.ArtikelForm artikelForm = new Gimpies.ArtikelForm(this);
            artikelForm.Show();
            artikelForm.TopMost = true;
        }

        public Voorraad GetItem(long id)
        {
            bool itemBestaat = voorraad.Any(r => r.ItemID == id);
            if (itemBestaat)
            {
                var item = voorraad.Find(r => r.ItemID == id);
                return item;
            }
            return new Voorraad();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void bewerkenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gimpies.ArtikelForm artikelForm = new Gimpies.ArtikelForm(this, Int64.Parse(artikelenList.SelectedItems[0].Text));
            artikelForm.Show();
            artikelForm.TopMost = true;
        }

        private void verwijderenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            globalClass.MysqlServerDelete(Int64.Parse(artikelenList.SelectedItems[0].Text));
            Populate();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
