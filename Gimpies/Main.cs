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
using System.Windows.Forms.DataVisualization.Charting;

namespace Gimpies
{
    public partial class Main : MForm
    {
        Global globalClass = new Global();

        public List<Voorraad> voorraad = new List<Voorraad>();
        public List<Werknemer> werknemers = new List<Werknemer>();
        public List<Sale> sales = new List<Sale>();
        public Werknemer loggedInWerknemer;
        string naam = "";
        bool isAdmin = false;
        Form loginForm;
        
        public Main(string _naam, Form _loginForm, Werknemer loggedInWerknemer, List<Werknemer> werknemersList)
        {
            InitializeComponent();

            naam = _naam;
            loginForm = _loginForm;
            isAdmin = (loggedInWerknemer.Rank == 1) ? true : false;
            this.werknemers = werknemersList;
            this.loggedInWerknemer = loggedInWerknemer;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = "Voorraadbeheer | Welkom: " + naam + "!" + ((isAdmin) ? " Admin" : "");
            Populate();
            artikelenList.Focus();
            tabControl1.ItemSize = new Size(tabControl1.ItemSize.Width,25);
            this.CloseAppOnClose = true;
        }

        private void LoadVoorraadChart()
        {
            chart1.Series.Clear();
            Series VerkochteSchoenenSeries = new Series
            {
                Name = "Verkochte schoenen",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            this.chart1.Series.Add(VerkochteSchoenenSeries);
            
            foreach (Voorraad artikel in voorraad)
            {
                VerkochteSchoenenSeries.Points.AddXY(artikel.ItemDesc, artikel.ItemAmount);
            }

            chart1.Invalidate();
            chart1.ResetAutoValues();
        }


        public void LoadStatistischeGegevens()
        {
            sales = globalClass.MysqlServerLoadSales();
            verkochtListView.Items.Clear();
            if (isAdmin)
            {
                long varTotaalVerkocht = 0;
                float varTotaalVerkochtEuro = 0.0f;
                //Statistieken van iedereen weergeven
                foreach (Sale s in sales)
                {
                    varTotaalVerkocht += Int64.Parse(s.Aantal.ToString());
                    varTotaalVerkochtEuro += float.Parse(s.Euro.ToString());
                    //In listview stoppen \/
                    Voorraad vr = voorraad.Find(r => r.ItemID == s.ArtikelId);
                    Werknemer wn = werknemers.Find(r => r.Id == s.UserId);

                    ListViewItem item = new ListViewItem(s.Id.ToString());
                    item.SubItems.Add(globalClass.FIRST_CHAR_UC(wn.Username));
                    item.SubItems.Add(vr.ItemDesc);
                    item.SubItems.Add(s.Aantal.ToString());
                    item.SubItems.Add(s.Euro);
                    item.SubItems.Add(s.Datum.ToString());
                    verkochtListView.Items.Add(item);
                }
                totaalVerkocht.Text = varTotaalVerkocht.ToString();
                totaalVerkochtEuro.Text = varTotaalVerkochtEuro.ToString();
            }
            else
            {
                long varTotaalVerkocht = 0;
                float varTotaalVerkochtEuro = 0.0f;
                //Statistieken van mezelf weergeven
                List<Sale> mySales = sales.FindAll(r => r.UserId == loggedInWerknemer.Id);
                foreach (Sale s in mySales)
                {
                    varTotaalVerkocht += Int64.Parse(s.Aantal.ToString());
                    varTotaalVerkochtEuro += float.Parse(s.Euro.ToString());
                    //In listview stoppen \/
                    Voorraad vr = voorraad.Find(r => r.ItemID == s.ArtikelId);

                    ListViewItem item = new ListViewItem(s.Id.ToString());
                    item.SubItems.Add(globalClass.FIRST_CHAR_UC(loggedInWerknemer.Username));
                    item.SubItems.Add(vr.ItemDesc);
                    item.SubItems.Add(s.Aantal.ToString());
                    item.SubItems.Add(s.Euro);
                    item.SubItems.Add(s.Datum.ToString());
                    verkochtListView.Items.Add(item);
                }
                totaalVerkocht.Text = varTotaalVerkocht.ToString();
                totaalVerkochtEuro.Text = varTotaalVerkochtEuro.ToString();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                //Ctrl+F = zoekbox focussen
                searchTextBox.Focus();
                return true;
            }
            if (keyData == (Keys.Control | Keys.T))
            {
                //Admin:        Ctrl+T = Nieuw artikel toevoegen
                //Gebruiker:    Ctrl+T = Verkoop registreren
                if (isAdmin)
                {
                    NieuwArtikelForm();
                }
                else
                {
                    VerkoopRegistrerenForm();
                }
                return true;
            }
            if (keyData == (Keys.Alt | Keys.W))
            {
                LogOut();
            }
            if (keyData == (Keys.Alt | Keys.Q))
            {
                //Alt+Q = Afsluiten
                Application.Exit();
                return true;
            }
            if (keyData == (Keys.None | Keys.Delete))
            {
                if (artikelenList.Focused)
                {
                    VerwijderArtikel();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void Populate(string zoekterm = "")
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
                    item.SubItems.Add(row.ItemVerkocht.ToString());
                    item.SubItems.Add(row.ItemMaat.ToString());
                    item.SubItems.Add(row.ItemPrijs.ToString());
                    artikelenList.Items.Add(item);
                }
            }
            else
            {
                //zoeken
                var zoekQuery = voorraad.FindAll(r => r.ItemDesc.ToLower().Contains(zoekterm.ToLower()));
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
            LoadVoorraadChart();
            LoadStatistischeGegevens();
        }

        private void afsluitenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void uitloggenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogOut();
        }

        private void LogOut()
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

        int closeAmount = 1;
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closeAmount == 1)
            { 
                globalClass.CheckOut(loggedInWerknemer.Id);
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

        public void ArtikelToevoegen(string beschrijving, string aantal, string prijs, string maat, string verkocht)
        {
            //Toevoegen en list repopulaten
            globalClass.MysqlServerInsert(beschrijving,aantal,prijs,maat);
            Populate();
        }

        public void ArtikelBewerken(long id, string beschrijving, string aantal, string prijs, string maat, string verkocht)
        {
            //Bewerkenen en repopulaten
            bool itemBestaat = voorraad.Any(r => r.ItemID == id);
            if (itemBestaat)
            {
                globalClass.MysqlServerUpdate(id,beschrijving,aantal,prijs,maat,verkocht);
                Populate();
            }
            else
            {
                MessageBox.Show("Artikel bestaat niet");
            }
        }

        private void artikelToevoegenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NieuwArtikelForm();
        }

        private void NieuwArtikelForm(string id = "-1")
        {
            Gimpies.ArtikelForm artikelForm = new Gimpies.ArtikelForm(this, Int64.Parse(id));
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
            if (artikelenList.SelectedItems.Count > 0)
            {
                NieuwArtikelForm(artikelenList.SelectedItems[0].Text);
            }
        }

        private void verwijderenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerwijderArtikel();
        }

        private void VerwijderArtikel()
        {
            if (artikelenList.SelectedItems.Count > 0)
            {
                globalClass.MysqlServerDelete(Int64.Parse(artikelenList.SelectedItems[0].Text));
                Populate();
            }
        }

        private void artikelenList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (artikelenList.SelectedItems.Count > 0)
                {
                    NieuwArtikelForm(artikelenList.SelectedItems[0].Text);
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NieuwArtikelForm();
        }

        private void toolStripTextBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Zoeken in voorraad en dan tijdelijke lijst maken
                searchTextBox.SelectAll();
                Populate(searchTextBox.Text);
            }
            if (searchTextBox.Text == "")
            {
                //Als hij leeg is de normale voorraad lijst laden
                Populate();
            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            searchTextBox.SelectAll();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            VerkoopRegistrerenForm();
        }

        private void VerkoopRegistrerenForm()
        {
            VerkoopRegistreren verkoopRegistreren = new VerkoopRegistreren(this, loggedInWerknemer);
            verkoopRegistreren.Show();
        }
    }
}