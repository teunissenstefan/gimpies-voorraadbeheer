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
    public partial class WerknemerForm : Form
    {
        Global globalClass = new Global();

        Main mainForm;
        Werknemer werknemer;
        bool add = true;
        public WerknemerForm(Main _mainForm, Werknemer wnemer, bool _add)
        {
            InitializeComponent();

            mainForm = _mainForm;
            werknemer = wnemer;
            add = _add;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toevoegBtn_Click(object sender, EventArgs e)
        {
            if (add)
            {
                //Toevoegen
                //Kijken of item bestaat
                bool itemBestaat = ranks.Any(r => r.Id == Int64.Parse(comboBox1.SelectedValue.ToString()));
                if (itemBestaat)
                {
                    //Kijken of gebruikersnaam al bestaat
                    bool gebruikersBestaat = mainForm.werknemers.Any(r => r.Username == gebruikerTextbox.Text);
                    if (!gebruikersBestaat)
                    {
                        //Gebruikersnaam bestaat nog niet dus toevoegen
                        globalClass.MysqlGebruikerToevoegen(gebruikerTextbox.Text, wachtwoordTextbox.Text, Int64.Parse(comboBox1.SelectedValue.ToString()));
                        mainForm.Populate();
                        this.Close();
                    }
                    else
                    {
                        //Gebruikersnaam bestaat al
                        MessageBox.Show("Gebruikersnaam al in gebruik");
                    }
                }
                else
                {
                    MessageBox.Show("Geselecteerde rang bestaat niet");
                }
            }
            else
            {
                //Bewerken
            }
        }


        List<Rank> ranks = new List<Rank>();
        private void ArtikelForm_Load(object sender, EventArgs e)
        {
            //Ranks laden in combobox
            ranks = globalClass.GetRanks();
            bindingSource1.DataSource = ranks;
            comboBox1.DataSource = bindingSource1.DataSource;
            comboBox1.DisplayMember = "Title";
            comboBox1.ValueMember = "Id";

            if (add)
            {
                toevoegBtn.Text = "Toevoegen";
            }
            else
            {
                var item = ranks.Find(r => r.Id == werknemer.Rank);
                comboBox1.SelectedIndex = comboBox1.FindStringExact(item.Title);
                gebruikerTextbox.Text = werknemer.Username;
                wachtwoordTextbox.Text = werknemer.Wachtwoord;
                toevoegBtn.Text = "Opslaan";
            }
        }
    }
}
