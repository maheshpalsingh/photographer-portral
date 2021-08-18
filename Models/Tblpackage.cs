using System;
using System.Collections.Generic;

namespace WebApplication5.Models
{
    public partial class Tblpackage
    {
        public int Packageid { get; set; }
        public double? Price { get; set; }
        public int? Phototgrapherid { get; set; }
        public int? Clicks { get; set; }

        public virtual Tblphotographer Phototgrapher { get; set; }
    }
}
