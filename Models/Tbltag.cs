using System;
using System.Collections.Generic;

namespace WebApplication5.Models
{
    public partial class Tbltag
    {
        public int Tagid { get; set; }
        public string Tagname { get; set; }
        public int? Categoryid { get; set; }

        public virtual Tblcategory Category { get; set; }
    }
}
