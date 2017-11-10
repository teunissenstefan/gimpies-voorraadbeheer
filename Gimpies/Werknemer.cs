using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimpies
{
    public class Werknemer
    {
        public Werknemer(long id, string username, int rank, string password)
        {
            this._id = id;
            this._username = username;
            this._password = password;
            this._rank = rank;
        }

        private long _id;
        private string _username;
        private string _password;
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

        public string Wachtwoord
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
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
