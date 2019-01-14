using BootCampApp.DAL;
using BootCampApp.Models;
using BootCampApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BootCampApp.Controllers
{
    public class InstructorsController : Controller
    {
        private BootCampDbContext db = new BootCampDbContext();

        // GET: Instructors
        public ActionResult Index(int? id, int? courseID)
        {
            var viewModel = new InstructorsData();

            // eager Loading
            viewModel.Instructors = db.Instructors
                .Include(i => i.OfficeAssignment)
                //.(i => i.Courses.Select(c => c.Department))
                .OrderBy(i => i.Lastname);

            if (id != null)
            {
                ViewBag.InstructorID = id.Value;
                viewModel.Courses = viewModel.Instructors
                    .Where(i => i.ID == id.Value).Single().Courses;
            }

            if (courseID != null)
            {
                ViewBag.CourseID = courseID.Value;
                // Lazy Loading
                viewModel.Enrollments = viewModel.Courses
                    .Where(c => c.CourseID == courseID).Single().Enrollments;
                // Explicit Loading
                //var selectedCourse = viewModel.Courses
                //    .Where(x => x.CourseID == courseID).Single();
                //db.Entry(selectedCourse).Collection(x => x.Enrollments).Load();
                //foreach (Enrollment enrollment in selectedCourse.Enrollments)
                //{
                //    db.Entry(enrollment).Reference(x => x.Student).Load();
                //}
                //viewModel.Enrollments = selectedCourse.Enrollments;
            }

            return View(viewModel);
        }

        // GET: Instructors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // GET: Instructors/Create
        public ActionResult Create()
        {
            var instructor = new Instructor();
            instructor.Courses = new List<Course>();
            PopulateAssignedCourseData(instructor);
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Lastname,Firstname,HireDate,OfficeAssignment")] Instructor instructor, string[] selectedCourses)
        {
            // Fill courses List on Instructor
            if (selectedCourses != null)
            {
                instructor.Courses = new List<Course>();
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = db.Courses.Find(int.Parse(course));
                    instructor.Courses.Add(courseToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                db.Instructors.Add(instructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Instructor instructor = db.Instructors.Find(id);
            Instructor instructor = db.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .Where(i => i.ID == id)
                .Single();



            if (instructor == null)
            {
                return HttpNotFound();
            }

            PopulateAssignedCourseData(instructor);
            //ViewBag.ID = new SelectList(db.OfficeAssignments, "InstructorID", "Location", instructor.ID);
            return View(instructor);
        }

        private void PopulateAssignedCourseData(Instructor instructor)
        {
            // Find Courses
            var allCourses = db.Courses;
            // Find instructor Courses
            var instructorCourses = new HashSet<int>(instructor.Courses.Select(c => c.CourseID));
            // Fill AssignedCourseData ViewModel
            var viewModel = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
            // Give ViewModel To View (ViewBag)
            ViewBag.Courses = viewModel;
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var instructorToUpdate = db.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .Where(i => i.ID == id)
                .Single();

            if (TryUpdateModel(instructorToUpdate, "",
                new string[] { "Lastname", "Firstname", "HireDate", "OfficeAssignment" }))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment.Location))
                    {
                        instructorToUpdate.OfficeAssignment = null;
                    }

                    // Update Instructor Courses
                    UpdateInstructorCourses(instructorToUpdate, selectedCourses);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Unable to save Changes");
                }
            }

            PopulateAssignedCourseData(instructorToUpdate);
            return View(instructorToUpdate);
        }

        private void UpdateInstructorCourses(Instructor instructorToUpdate, string[] selectedCourses)
        {
            // Check if selectedCourses is NULL
            if (selectedCourses == null)
            {
                instructorToUpdate.Courses = new List<Course>();
                return;
            }
            // Maybe i am going to need InstructorCourses
            var selectedCoursesHS = new HashSet<string>(selectedCourses); // This is the list that comes from the View
            var instructorsCourses = new HashSet<int>(instructorToUpdate.Courses.Select(c => c.CourseID)); // This is the List that comes from DB
            // Loop all Courses
            // 1. IF selectedCourses Contains specific Course, 
            // 2. If  instructorCourses NOT Contains specific Course
            // 3. add it to Instructor
            foreach (var course in db.Courses)
            {
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!instructorsCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.Courses.Add(course);
                    }

                }
                // ELSE 
                // if instructorCourses  Contains specific Course
                // Remove Course
                else
                {
                    if (instructorsCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.Courses.Remove(course);
                    }
                }
            }
        }

        // GET: Instructors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Instructor instructor = db.Instructors.Find(id);
            Instructor instructor = db.Instructors
                .Include(i => i.OfficeAssignment)
                .Where(i => i.ID == id)
                .Single();

            db.Instructors.Remove(instructor);

            var department = db.Departments
                .Where(d => d.InstructorID == id)
                .SingleOrDefault();

            if (department != null)
            {
                department.InstructorID = null;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
