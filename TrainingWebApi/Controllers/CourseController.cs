using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingWebApi.DTO;
using TrainingWebApi.Models;

namespace TrainingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        TrainingDbContext db = null;

        //Constructor Dependency Injection
        public CourseController(TrainingDbContext context)
        {
            db = context;
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var course = db.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            db.Courses.Remove(course);
            db.SaveChanges();
            return Ok("Course deleted");
        }

        [HttpPut("{id}")]
        public IActionResult EditCourse(int id, CourseDTO modifiedCourse)
        {
            //fetch the existing course with the given id
            var existingCourse = db.Courses.Find(id);
            if (existingCourse == null)
            {
                return NotFound();
            }
            //save the changes in the existing course object
            existingCourse.CourseName = modifiedCourse.CourseName;
            existingCourse.CourseDuration = modifiedCourse.CourseDuration;
            existingCourse.CourseFees = modifiedCourse.CourseFees;
            db.SaveChanges();
            return Ok("Course updated");
        }

        [HttpPost]
        public IActionResult CreateCourse(CourseDTO course)
        {
            if (course == null)
            {
                return BadRequest("course object is null");
            }
            //copy the DTO object values to the Course object which will be added to database
            Course c = new Course
            {
                CourseDuration = course.CourseDuration,
                CourseName = course.CourseName,
                CourseFees = course.CourseFees
            };
            db.Courses.Add(c);
            db.SaveChanges();
            return Ok("Course created with Id = " + c.CourseId);
        }
        [HttpGet("{id}")]
        public IActionResult GetCourseById(int id)
        {
            var course = db.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpGet]
        public IActionResult GetCourses()
        {
            var courseList = db.Courses.ToList();
            if (courseList.Count == 0)
            {
                return NotFound();
            }
            return Ok(courseList);
        }
    }
}
