using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BootCampApp.Models
{
    public class Student : Person
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        // Navigation Property
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}