using BootCampApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace BootCampApp.DAL
{
    public class BootCampInitializer : DropCreateDatabaseIfModelChanges<BootCampDbContext>
    {
        protected override void Seed(BootCampDbContext context)
        {
            var students = new List<Student>
            {
                new Student
                {
                    Firstname = "Peri",
                    Lastname = "Aidinopoulos",
                    EnrollmentDate = DateTime.Parse("2018-09-01")
                },
                new Student
                {
                    Firstname = "Nick",
                    Lastname = "Galis",
                    EnrollmentDate = DateTime.Parse("2018-09-01")
                },
                new Student
                {
                    Firstname = "Lionel",
                    Lastname = "Messi",
                    EnrollmentDate = DateTime.Parse("2018-11-01")
                },
                new Student
                {
                    Firstname = "Viktoras",
                    Lastname = "Klonaridis",
                    EnrollmentDate = DateTime.Parse("2018-11-01")
                },
                new Student
                {
                    Firstname = "Rodrigo",
                    Lastname = "Galo",
                    EnrollmentDate = DateTime.Parse("2018-10-01")
                },
                new Student
                {
                    Firstname = "Michael",
                    Lastname = "Schumacher",
                    EnrollmentDate = DateTime.Parse("2018-10-01")
                },
                new Student
                {
                    Firstname = "Thodora",
                    Lastname = "Antonopoulou",
                    EnrollmentDate = DateTime.Parse("2018-11-01")
                }
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course
                {
                    CourseID = 1050,
                    Title = "OOP",
                    Hours = 654
                },
                new Course
                {
                    CourseID = 1060,
                    Title = "C#",
                    Hours = 876
                },
                new Course
                {
                    CourseID = 1070,
                    Title = "Javascript",
                    Hours = 987
                },
                new Course
                {
                    CourseID = 1080,
                    Title = ".NET",
                    Hours = 3
                },
                new Course
                {
                    CourseID = 1090,
                    Title = "MVC",
                    Hours = 87
                },
                new Course
                {
                    CourseID = 1110,
                    Title = "Python",
                    Hours = 99
                },
                new Course
                {
                    CourseID = 1120,
                    Title = "BootStrap",
                    Hours = 56
                }
            };

            courses.ForEach(c => context.Courses.Add(c));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment
                {
                    StudentID = 1,
                    CourseID = 1050,
                    Grade = Grade.C
                },
                new Enrollment
                {
                    StudentID = 1,
                    CourseID = 1060,
                    Grade = Grade.B
                },
                new Enrollment
                {
                    StudentID = 1,
                    CourseID = 1070,
                    Grade = Grade.C
                },
                new Enrollment
                {
                    StudentID = 2,
                    CourseID = 1050,
                    Grade = Grade.A
                },
                new Enrollment
                {
                    StudentID = 2,
                    CourseID = 1070,
                    Grade = Grade.B
                },
                new Enrollment
                {
                    StudentID = 3,
                    CourseID = 1110,
                    Grade = Grade.A
                },
                new Enrollment
                {
                    StudentID = 3,
                    CourseID = 1120,
                    Grade = Grade.A
                },
                new Enrollment
                {
                    StudentID = 4,
                    CourseID = 1050,
                    Grade = Grade.F
                },
                new Enrollment
                {
                    StudentID = 5,
                    CourseID = 1080,
                    Grade = Grade.B
                },
                new Enrollment
                {
                    StudentID = 6,
                    CourseID = 1090,
                    Grade = Grade.B
                },
                new Enrollment
                {
                    StudentID = 6,
                    CourseID = 1110
                },
                new Enrollment
                {
                    StudentID = 7,
                    CourseID = 1090,
                    Grade = Grade.D
                }
            };

            enrollments.ForEach(e => context.Enrollments.Add(e));
            context.SaveChanges();

        }
    }
}