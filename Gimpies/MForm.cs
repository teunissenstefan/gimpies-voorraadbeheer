using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gimpies
{
    public class MForm : Form
    {
        private bool closeApp;

        public bool CloseAppOnClose
        {
            get { return closeApp; }
            set { closeApp = value; }
        }
    }
}
