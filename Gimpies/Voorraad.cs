using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimpies
{
    public class Voorraad
    {
        public Voorraad(int itemido, string itemdesco, int aantalo)
        {
            this.itemid = itemido;
            this.itemdesc = itemdesco;
            this.itemamount = aantalo;
        }

        private int itemid;
        public int ItemID
        {
            get
            {
                return this.itemid;
            }
            set
            {
                this.itemid = value;
            }
        }

        private int itemamount;
        public int ItemAmount
        {
            get
            {
                return this.itemamount;
            }
            set
            {
                this.itemamount = value;
            }
        }

        private string itemdesc;
        public string ItemDesc
        {
            get
            {
                return this.itemdesc;
            }
            set
            {
                this.itemdesc = value;
            }
        }
    }
}
