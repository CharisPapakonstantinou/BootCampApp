﻿using BootCampApp.Models;
using System.Collections.Generic;

namespace BootCampApp.ViewModels
{
    public class InstructorsData
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }

    }
}