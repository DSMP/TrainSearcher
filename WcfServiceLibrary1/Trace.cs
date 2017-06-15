using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary1
{
    public class Trace : Object
    {
        public Vertex FromTown { get; }
        public Vertex ToTown { get; }
        public DateTime FromDate { get; }
        public DateTime ToDate { get; }

        public Trace(Vertex fromTown, DateTime FromDate, Vertex toTown, DateTime toDate)
        {
            this.FromTown = fromTown;
            this.FromDate = FromDate;
            this.ToTown = toTown;
            this.ToDate = toDate;
        }
        
        public override string ToString()
        {
            return FromTown + " " + FromDate + " => " + ToTown + " " + ToDate;
        }
    }
}
