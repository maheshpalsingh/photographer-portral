using System;
using System.Collections.Generic;

namespace WebApplication5.Models
{
    public partial class Tblcategory
    {
        public Tblcategory()
        {
            Tblphoto = new HashSet<Tblphoto>();
            Tbltag = new HashSet<Tbltag>();
        }

        public int Categoryid { get; set; }
        public string Categoryname { get; set; }

        public virtual ICollection<Tblphoto> Tblphoto { get; set; }
        public virtual ICollection<Tbltag> Tbltag { get; set; }
    }
}
