using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.ViewModels
{
   
        public class PhotoCommentViewModel
        {
            public int Photoid { get; set; }
            public string Photourl { get; set; }
            public int? Photographerid { get; set; }
            public int? Categoryid { get; set; }
            public int Commentid { get; set; }
            public string Cuusername { get; set; }
        public string Customername { get; set; }
        public int? Customerid { get; set; }
            public string Comment { get; set; }
            public DateTime? Commentdate { get; set; }

            public virtual Tblcustomer Customer { get; set; }
            public virtual Tblphoto Photo { get; set; }
        }
    
}
