using System;
using System.Collections.Generic;

namespace WebApplication5.Models
{
    public partial class Tblappointmentslot
    {
        public Tblappointmentslot()
        {
            Tblappointment = new HashSet<Tblappointment>();
        }

        public int Slotid { get; set; }
        public int? Photographerid { get; set; }
        public string Duration { get; set; }

        public virtual Tblphotographer Photographer { get; set; }
        public virtual ICollection<Tblappointment> Tblappointment { get; set; }
    }
}
