using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimpies
{
    public class Sale
    {
        public Sale(long id, long uid, long aant, string eur, DateTime dt)
        {
            this._id = id;
            this._uid = uid;
            this._aant = aant;
            this._eur = eur;
            this._dt = dt;
        }

        private long _id;
        private long _uid;
        private long _aant;
        private string _eur;
        private DateTime _dt;

        public long Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public long UserId
        {
            get
            {
                return this._uid;
            }
            set
            {
                this._uid = value;
            }
        }

        public long Aantal
        {
            get
            {
                return this._aant;
            }
            set
            {
                this._aant = value;
            }
        }

        public string Euro
        {
            get
            {
                return this._eur;
            }
            set
            {
                this._eur = value;
            }
        }

        public DateTime Datum
        {
            get
            {
                return this._dt;
            }
            set
            {
                this._dt = value;
            }
        }
    }
}
