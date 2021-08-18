using System;
using System.Collections.Generic;

namespace WebApplication5.Models
{
    public partial class Tblphotographer
    {
        public Tblphotographer()
        {
            Tblappointment = new HashSet<Tblappointment>();
            Tblappointmentslot = new HashSet<Tblappointmentslot>();
            Tblpackage = new HashSet<Tblpackage>();
            Tblphoto = new HashSet<Tblphoto>();
            Tblphotographerreview = new HashSet<Tblphotographerreview>();
        }

        public int Photographerid { get; set; }
        public string Photographername { get; set; }
        public string Phusername { get; set; }
        public string Phpassword { get; set; }
        public string Contact { get; set; }
        public string Phprofile { get; set; }
        public string Phmail { get; set; }
        public int? Cityid { get; set; }
        public string Lat { get; set; }
        public string Lan { get; set; }

        public virtual Tblcity City { get; set; }
        public virtual ICollection<Tblappointment> Tblappointment { get; set; }
        public virtual ICollection<Tblappointmentslot> Tblappointmentslot { get; set; }
        public virtual ICollection<Tblpackage> Tblpackage { get; set; }
        public virtual ICollection<Tblphoto> Tblphoto { get; set; }
        public virtual ICollection<Tblphotographerreview> Tblphotographerreview { get; set; }
    }
}
