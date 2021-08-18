using System;
using System.Collections.Generic;

namespace WebApplication5.Models
{
    public partial class Tblcustomer
    {
        public Tblcustomer()
        {
            Tblappointment = new HashSet<Tblappointment>();
            Tblphoto = new HashSet<Tblphoto>();
            Tblphotocomment = new HashSet<Tblphotocomment>();
            Tblphotographerreview = new HashSet<Tblphotographerreview>();
        }

        public int Customerid { get; set; }
        public string Customername { get; set; }
        public string Cuusername { get; set; }
        public string Cupassword { get; set; }
        public string Contact { get; set; }
        public string Cuprofile { get; set; }
        public string Cumail { get; set; }

        public virtual ICollection<Tblappointment> Tblappointment { get; set; }
        public virtual ICollection<Tblphoto> Tblphoto { get; set; }
        public virtual ICollection<Tblphotocomment> Tblphotocomment { get; set; }
        public virtual ICollection<Tblphotographerreview> Tblphotographerreview { get; set; }
    }
}
