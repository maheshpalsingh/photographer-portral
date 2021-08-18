using System;
using System.Collections.Generic;

namespace WebApplication5.Models
{
    public partial class Tblstate
    {
        public Tblstate()
        {
            Tblcity = new HashSet<Tblcity>();
        }

        public int Stateid { get; set; }
        public string Statename { get; set; }

        public virtual ICollection<Tblcity> Tblcity { get; set; }
    }
}
