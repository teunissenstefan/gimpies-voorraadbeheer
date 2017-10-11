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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        Global globalClass = new Global();
        private void Login_Load(object sender, EventArgs e)
        {
            MessageBox.Show(globalClass.MAX_LOGIN_TRIES().ToString());
        }
    }
}
