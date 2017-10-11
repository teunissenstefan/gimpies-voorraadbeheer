using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimpies
{
    public class Medewerkers
    {
        public bool Login(string wachtwoord)
        {
            if (wachtwoord == "1")
            {
                return true;
            }
            return false;
        }
    }
}
