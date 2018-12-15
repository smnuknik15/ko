using System;
using System.Collections.Generic;

namespace OnlineCourseWeb.Models
{
    public partial class Oders
    {
        public Oders()
        {
            Payments = new HashSet<Payments>();
        }

        public string OderId { get; set; }
        public DateTime? OderDate { get; set; }
        public string OderCustomerId { get; set; }
        public string OdersCouresId { get; set; }

        public Customers OderCustomer { get; set; }
        public Coures OdersCoures { get; set; }
        public ICollection<Payments> Payments { get; set; }
    }
}
