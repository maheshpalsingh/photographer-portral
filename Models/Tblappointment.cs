using System;
using System.Collections.Generic;

namespace WebApplication5.Models
{
    public partial class Tblappointment
    {
        public int Appointmentid { get; set; }
        public int? Slotid { get; set; }
        public int? Photographerid { get; set; }
        public int? Customerid { get; set; }
        public string Appointmentdesc { get; set; }
        public int? Statusid { get; set; }

        public virtual Tblcustomer Customer { get; set; }
        public virtual Tblphotographer Photographer { get; set; }
        public virtual Tblappointmentslot Slot { get; set; }
        public virtual Tblappointmentstatus Status { get; set; }
    }
}
