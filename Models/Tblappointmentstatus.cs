using System;
using System.Collections.Generic;

namespace WebApplication5.Models
{
    public partial class Tblappointmentstatus
    {
        public Tblappointmentstatus()
        {
            Tblappointment = new HashSet<Tblappointment>();
        }

        public int Statusid { get; set; }
        public string Appstatus { get; set; }

        public virtual ICollection<Tblappointment> Tblappointment { get; set; }
    }
}
