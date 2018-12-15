using System;
using System.Collections.Generic;

namespace OnlineCourseWeb.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Oders = new HashSet<Oders>();
        }

        public string CtmIdcard { get; set; }
        public string CtmName { get; set; }
        public string CtmLasname { get; set; }
        public string CtmTel { get; set; }
        public string CtmEmail { get; set; }

        public ICollection<Oders> Oders { get; set; }
    }
}
