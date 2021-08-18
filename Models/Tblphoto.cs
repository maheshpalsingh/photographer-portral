using System;
using System.Collections.Generic;

namespace WebApplication5.Models
{
    public partial class Tblphoto
    {
        public Tblphoto()
        {
            Tblphotocomment = new HashSet<Tblphotocomment>();
        }

        public int Photoid { get; set; }
        public string Photourl { get; set; }
        public int? Photographerid { get; set; }
        public int? Customerid { get; set; }
        public int? Categoryid { get; set; }

        public virtual Tblcategory Category { get; set; }
        public virtual Tblcustomer Customer { get; set; }
        public virtual Tblphotographer Photographer { get; set; }
        public virtual ICollection<Tblphotocomment> Tblphotocomment { get; set; }
    }
}
