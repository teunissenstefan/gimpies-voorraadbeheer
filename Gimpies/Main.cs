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
    public partial class Main : Form
    {
        Global globalClass = new Global();

        List<Voorraad> voorraad = new List<Voorraad>();
        string naam = "";
        Form loginForm;
        bool closeApp = true;
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
        }

        private void Populate(string zoekterm = "")
        {
            artikelenList.Items.Clear();
            if (zoekterm == "")
            {
                voorraad = globalClass.VOORRAAD_LOAD();
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
            loginForm.Show();
            closeApp = false;
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

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closeApp)
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
            Voorraad newVoorraad = new Voorraad();
            if (voorraad.Count <= 0)
            {
                newVoorraad.ItemID = 1;
            }
            else
            {
                newVoorraad.ItemID = voorraad[voorraad.Count - 1].ItemID + 1;
            }
            newVoorraad.ItemDesc = (beschrijving);
            newVoorraad.ItemAmount = Int64.Parse(aantal);
            newVoorraad.ItemPrijs = (prijs.Replace(',', '.'));
            newVoorraad.ItemMaat = Int64.Parse(maat);
            voorraad.Add(newVoorraad);
            globalClass.VOORRAAD_SAVE(voorraad);
            Populate();
        }

        public void ArtikelBewerken(long id, string beschrijving, string aantal, string prijs, string maat)
        {
            //Bewerkenen en repopulaten
            bool itemBestaat = voorraad.Any(r => r.ItemID == id);
            if (itemBestaat)
            {
                var item = voorraad.Find(r => r.ItemID == id);
                item.ItemDesc = beschrijving;
                item.ItemAmount = Int64.Parse(aantal);
                item.ItemPrijs = prijs.Replace(',','.');
                item.ItemMaat = Int64.Parse(maat);

                globalClass.VOORRAAD_SAVE(voorraad); 
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
    }
}
