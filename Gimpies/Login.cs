﻿using System;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        Global globalClass = new Global();
        private void Login_Load(object sender, EventArgs e)
        {
            logo.ImageLocation = "data/logo.png";
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int currLogin = 1;
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (globalClass.LOGIN(wachtwoordTxtBox.Text))
            {
                //Gebruikersnaam meenemen naar main form
                string _gebruikersnaam = globalClass.FIRST_CHAR_UC(gebruikersnaamTxtBox.Text);

                currLogin = 1;
                gebruikersnaamTxtBox.Text = "";
                wachtwoordTxtBox.Text = "";
                gebruikersnaamTxtBox.Focus();
                Main mainForm = new Main(_gebruikersnaam, this);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                //Max 3 keer proberen
                if (currLogin >= globalClass.MAX_LOGIN_TRIES())
                {
                    MessageBox.Show("Wachtwoord fout.\nEr is te vaak geprobeerd in te loggen");
                    gebruikersnaamTxtBox.Enabled = false;
                    wachtwoordTxtBox.Enabled = false;
                    LoginBtn.Enabled = false;
                }
                else
                {
                    currLogin++;
                    MessageBox.Show("Wachtwoord fout, nog "+ (globalClass.MAX_LOGIN_TRIES()-currLogin+1) +" keer over");
                    gebruikersnaamTxtBox.Text = "";
                    wachtwoordTxtBox.Text = "";
                    gebruikersnaamTxtBox.Focus();
                }
            }
        }
    }
}
