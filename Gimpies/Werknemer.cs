using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimpies
{
    public class Werknemer
    {
        public Werknemer(long id, string username, int rank)
        {
            this._id = id;
            this._username = username;
            this._rank = rank;
        }

        private long _id;
        private string _username;
        private int _rank;

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

        public string Username
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }

        public int Rank
        {
            get
            {
                return this._rank;
            }
            set
            {
                this._rank = value;
            }
        }
    }
}
