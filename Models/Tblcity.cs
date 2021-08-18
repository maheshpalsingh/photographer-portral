using System;
using System.Collections.Generic;

namespace WebApplication5.Models
{
    public partial class Tblcity
    {
        public Tblcity()
        {
            Tblphotographer = new HashSet<Tblphotographer>();
        }

        public int Cityid { get; set; }
        public string Cityname { get; set; }
        public int? Stateid { get; set; }

        public virtual Tblstate State { get; set; }
        public virtual ICollection<Tblphotographer> Tblphotographer { get; set; }
    }
}
