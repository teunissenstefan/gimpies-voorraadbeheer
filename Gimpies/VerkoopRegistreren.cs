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
    public partial class VerkoopRegistreren : Form
    {
        Global globalClass = new Global();

        Main mainForm;
        Werknemer werknemer;
        public VerkoopRegistreren(Main _mainForm, Werknemer wnemer)
        {
            InitializeComponent();

            mainForm = _mainForm;
            werknemer = wnemer;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toevoegBtn_Click(object sender, EventArgs e)
        {
            //Er MOET een item GESELECTEERD zijn, niet getypt
            //Kijken of item bestaat
            //Kijken of aantal verkocht niet meer is dan beschikbaar zijn
            //mainForm.ArtikelBewerken(id,beschrijvingTextbox.Text,aantalTextbox.Text,prijsTextbox.Text,maatTextbox.Text);
            //this.Close();
            //MessageBox.Show(comboBox1.SelectedIndex.ToString());
            if (comboBox1.SelectedIndex != -1)
            {
                //Er is iets geselecteerd
                //Kijken of item bestaat
                bool itemBestaat = mainForm.voorraad.Any(r => r.ItemID == Int64.Parse(idComboBox.Text));
                if (itemBestaat)
                {
                    //Geselecteerde artikel bestaat
                    var item = mainForm.voorraad.Find(r => r.ItemID == Int64.Parse(idComboBox.Text));
                    //Hoeveelheid checken (aantal-verkocht)
                    if ((item.ItemAmount-item.ItemVerkocht)-Int64.Parse(aantalTextbox.Text) >= 0)
                    {
                        //Genoeg beschikbaar
                        //In SALES tabel toevoegen als sale, en ook het aantal verkocht updaten in de ARTIKELEN tabel (gwn MysqlServerUpdate)
                        //Totale euro uitrekenen
                        float euro = float.Parse(item.ItemPrijs) * Int64.Parse(aantalTextbox.Text);
                        //SALES tabel toevoeging
                        globalClass.MysqlAddSale(werknemer.Id.ToString(),item.ItemID.ToString(),aantalTextbox.Text,euro.ToString());
                        //ARTIKELEN tabel update
                        globalClass.MysqlServerUpdate(item.ItemID,item.ItemDesc,item.ItemAmount.ToString(),item.ItemPrijs,item.ItemMaat.ToString(),(item.ItemVerkocht+Int64.Parse(aantalTextbox.Text)).ToString());
                        mainForm.Populate();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Er zijn er niet zoveel beschikbaar");
                    }
                }
                else
                {
                    MessageBox.Show("Geselecteerde artikel bestaat niet");
                }
            }
            else
            {
                //Er is niks geselecteerd
                MessageBox.Show("Je moet een artikel selecteren");
            }
        }


        List<long> ids = new List<long>();
        List<string> descs = new List<string>();
        private void ArtikelForm_Load(object sender, EventArgs e)
        {
            foreach (Voorraad item in mainForm.voorraad)
            {
                //comboBox1.Items.Add(item.ItemDesc);
                ids.Add(item.ItemID);
                descs.Add(item.ItemDesc);
            }
            for(int i = 0;i < ids.Count; i++)
            {
                idComboBox.Items.Add(ids[i]);
                comboBox1.Items.Add(descs[i]);
            }
        }

        private void idComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = idComboBox.SelectedIndex;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            idComboBox.SelectedIndex = comboBox1.SelectedIndex;
        }
    }
}
