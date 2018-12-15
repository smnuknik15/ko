using System;
using System.Collections.Generic;

namespace OnlineCourseWeb.Models
{
    public partial class Coures
    {
        public Coures()
        {
            Oders = new HashSet<Oders>();
        }

        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal? CoursePrice { get; set; }
        public DateTime? CourseDate { get; set; }
        public string CourseHourNum { get; set; }
        public string CourseDetail { get; set; }

        public ICollection<Oders> Oders { get; set; }
    }
}
