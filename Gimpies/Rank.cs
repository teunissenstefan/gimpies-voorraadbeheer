using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimpies
{
    public class Rank
    {
        public Rank(long id,string title)
        {
            this._id = id;
            this._title = title;
        }

        private long _id;
        private string _title;

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
        
        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }
    }
}
