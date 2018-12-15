using System;
using System.Collections.Generic;

namespace OnlineCourseWeb.Models
{
    public partial class Payments
    {
        public string PmtId { get; set; }
        public DateTime? PmtDate { get; set; }
        public string PmtOderId { get; set; }
        public byte[] PmtPicture { get; set; }

        public Oders PmtOder { get; set; }
    }
}
