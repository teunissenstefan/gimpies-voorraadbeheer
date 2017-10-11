using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimpies
{
    public class Voorraad
    {
        public Voorraad(long itemido, string itemdesco, long aantalo, float prijso, long maato)
        {
            this.itemid = itemido;
            this.itemdesc = itemdesco;
            this.itemamount = aantalo;
            this.itemprijs = prijso;
            this.itemmaat = maato;
        }

        private long itemid;
        public long ItemID
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

        private long itemamount;
        public long ItemAmount
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

        private float itemprijs;
        public float ItemPrijs
        {
            get
            {
                return this.itemprijs;
            }
            set
            {
                this.itemprijs = value;
            }
        }

        private long itemmaat;
        public long ItemMaat
        {
            get
            {
                return this.itemmaat;
            }
            set
            {
                this.itemmaat = value;
            }
        }
    }
}
