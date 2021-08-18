using System;
using System.Collections.Generic;

namespace WebApplication5.Models
{
    public partial class Tblphotographerreview
    {
        public int Reviewid { get; set; }
        public int? Review { get; set; }
        public int? Photographerid { get; set; }
        public int? Customerid { get; set; }
        public DateTime? Reviewdate { get; set; }

        public virtual Tblcustomer Customer { get; set; }
        public virtual Tblphotographer Photographer { get; set; }
    }
}
