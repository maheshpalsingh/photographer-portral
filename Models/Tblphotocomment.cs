using System;
using System.Collections.Generic;

namespace WebApplication5.Models
{
    public partial class Tblphotocomment
    {
        public int Commentid { get; set; }
        public int? Photoid { get; set; }
        public int? Customerid { get; set; }
        public string Comment { get; set; }

        public virtual Tblcustomer Customer { get; set; }
        public virtual Tblphoto Photo { get; set; }
    }
}
