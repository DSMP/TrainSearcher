using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary1
{
    class Trace : Object
    {
        String  FromTown;
        String ToTown;
        DateTime FromDate;
        DateTime ToDate;

        public Trace(String fromTown, DateTime FromDate, String toTown, DateTime toDate)
        {
            this.FromTown = fromTown;
            this.FromDate = FromDate;
            this.ToTown = toTown;
            this.ToDate = toDate;
        }

        public string FromTown1
        {
            get
            {
                return FromTown;
            }

            set
            {
                FromTown = value;
            }
        }

        public string ToTown1
        {
            get
            {
                return ToTown;
            }

            set
            {
                ToTown = value;
            }
        }

        public DateTime FromDate1
        {
            get
            {
                return FromDate;
            }

            set
            {
                FromDate = value;
            }
        }

        public DateTime ToDate1
        {
            get
            {
                return ToDate;
            }

            set
            {
                ToDate = value;
            }
        }
        public override string ToString()
        {
            return FromTown + " " + FromDate + " " + ToTown + " " + ToDate;
        }
    }
}
