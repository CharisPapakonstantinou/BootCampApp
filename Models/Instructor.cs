using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BootCampApp.Models
{
    public class Instructor : Person
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        // Navigation Properties
        public virtual ICollection<Course> Courses { get; set; }

        // Next Project
        //private ICollection<Course> _courses;
        //public virtual ICollection<Course> Courses
        //{
        //    get
        //    {
        //        return _courses ?? (_courses = new List<Course>());
        //    }
        //    set
        //    {
        //        _courses = value;
        //    }
        //}
        public virtual OfficeAssignment OfficeAssignment { get; set; }
    }
}