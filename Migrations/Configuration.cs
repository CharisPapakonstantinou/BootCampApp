namespace BootCampApp.Migrations
{
    using BootCampApp.Models;
    using BootCampApp.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BootCampDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BootCampDbContext context)
        {
            var students = new List<Student>
            {
                new Student { Firstname = "Carson",   Lastname = "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { Firstname = "Meredith", Lastname = "Alonso",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { Firstname = "Arturo",   Lastname = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { Firstname = "Gytis",    Lastname = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { Firstname = "Yan",      Lastname = "Li",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { Firstname = "Peggy",    Lastname = "Justice",
                    EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { Firstname = "Laura",    Lastname = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { Firstname = "Nino",     Lastname = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2005-09-01") }
            };

            students.ForEach(s => context.Students.AddOrUpdate(p => p.Lastname, s));
            context.SaveChanges();

            var instructors = new List<Instructor>
            {
                new Instructor { Firstname = "Kim",     Lastname = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { Firstname = "Fadi",    Lastname = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { Firstname = "Roger",   Lastname = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Instructor { Firstname = "Candace", Lastname = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Instructor { Firstname = "Roger",   Lastname = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12") }
            };
            instructors.ForEach(s => context.Instructors.AddOrUpdate(p => p.Lastname, s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department { Name = "English",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.Lastname == "Abercrombie").ID },
                new Department { Name = "Mathematics", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.Lastname == "Fakhouri").ID },
                new Department { Name = "Engineering", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.Lastname == "Harui").ID },
                new Department { Name = "Economics",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.Lastname == "Kapoor").ID }
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course {CourseID = 1050, Title = "Chemistry",      Hours = 3,
                  DepartmentID = departments.Single( s => s.Name == "Engineering").DepartmentID,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseID = 4022, Title = "Microeconomics", Hours = 3,
                  DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseID = 4041, Title = "Macroeconomics", Hours = 3,
                  DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseID = 1045, Title = "Calculus",       Hours = 4,
                  DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseID = 3141, Title = "Trigonometry",   Hours = 4,
                  DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseID = 2021, Title = "Composition",    Hours = 3,
                  DepartmentID = departments.Single( s => s.Name == "English").DepartmentID,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseID = 2042, Title = "Literature",     Hours = 4,
                  DepartmentID = departments.Single( s => s.Name == "English").DepartmentID,
                  Instructors = new List<Instructor>()
                },
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseID, s));
            context.SaveChanges();

            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.Lastname == "Fakhouri").ID,
                    Location = "Smith 17" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.Lastname == "Harui").ID,
                    Location = "Gowan 27" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.Lastname == "Kapoor").ID,
                    Location = "Thompson 304" },
            };
            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.InstructorID, s));
            context.SaveChanges();

            AddOrUpdateInstructor(context, "Chemistry", "Kapoor");
            AddOrUpdateInstructor(context, "Chemistry", "Harui");
            AddOrUpdateInstructor(context, "Microeconomics", "Zheng");
            AddOrUpdateInstructor(context, "Macroeconomics", "Zheng");

            AddOrUpdateInstructor(context, "Calculus", "Fakhouri");
            AddOrUpdateInstructor(context, "Trigonometry", "Harui");
            AddOrUpdateInstructor(context, "Composition", "Abercrombie");
            AddOrUpdateInstructor(context, "Literature", "Abercrombie");

            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment {
                    StudentID = students.Single(s => s.Lastname == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    Grade = Grade.A
                },
                 new Enrollment {
                    StudentID = students.Single(s => s.Lastname == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                    Grade = Grade.C
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.Lastname == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.Lastname == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.Lastname == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.Lastname == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.Lastname == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.Lastname == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                    Grade = Grade.B
                 },
                new Enrollment {
                    StudentID = students.Single(s => s.Lastname == "Barzdukas").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.Lastname == "Li").ID,
                    CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.Lastname == "Justice").ID,
                    CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                    Grade = Grade.B
                 }
            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                         s.Student.ID == e.StudentID &&
                         s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }

        void AddOrUpdateInstructor(BootCampDbContext context, string courseTitle, string instructorName)
        {
            var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
            var inst = crs.Instructors.SingleOrDefault(i => i.Lastname == instructorName);
            if (inst == null)
                crs.Instructors.Add(context.Instructors.Single(i => i.Lastname == instructorName));
        }
    }
}