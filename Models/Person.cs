using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootCampApp.Models
{
    public class Person
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "LastName must be under 50 characters")]
        public string Lastname { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "FirstName must be under 50 characters")]
        public string Firstname { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return Lastname + " " + Firstname;
            }
        }
    }
}