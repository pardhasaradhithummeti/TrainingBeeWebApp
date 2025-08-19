using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingWebApi.DTO;
using TrainingWebApi.Models;

namespace TrainingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        TrainingDbContext db = null!;
        public StudentController(TrainingDbContext dbContext)
        {
            db = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = db.Students.Include("CourseJoined").ToList();
            if (students == null || students.Count == 0)
            {
                return NotFound("No students found");
            }
            List<StudentDTO> studentDtos = new List<StudentDTO>();
            foreach (var student in students)
            {
                var studentDTO = new StudentDTO
                {
                    RollNo = student.RollNo,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    City = student.City,
                    BirthDate = student.BirthDate,
                    CourseId = student.CourseId,
                    CourseName = student.CourseJoined.CourseName // Assuming Course is a navigation property
                };
                studentDtos.Add(studentDTO);
            }
            return Ok(studentDtos);
        }
        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = db.Students.Include("CourseJoined").FirstOrDefault(s=>s.RollNo == id);
            if (student == null)
            {
                return NotFound();
            }
            var studentDTO = new StudentDTO
            {
                RollNo = student.RollNo,
                FirstName = student.FirstName,
                LastName = student.LastName,
                City = student.City,
                BirthDate = student.BirthDate,
                CourseId = student.CourseId,
                CourseName = student.CourseJoined.CourseName // Assuming Course is a navigation property
            };
            return Ok(studentDTO);
        }
        [HttpPost]
        public IActionResult CreateStudent(StudentDTO studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest("Student object is null");
            }
            var student = new Student
            {
                FirstName = studentDto.FirstName     ,
                LastName = studentDto.LastName,
                City = studentDto.City,
                BirthDate = studentDto.BirthDate,
                CourseId = studentDto.CourseId,
                // CourseJoined = db.Courses.Find(studentDto.CourseId) // Uncomment if you want to set the navigation property
            };
            db.Students.Add(student);
            db.SaveChanges();
            return Ok($"Students Created with ID = {student.RollNo}");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, StudentDTO studentDto)
        {
            var student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound("Student not found with ID = " + id);
            }
            student.FirstName = studentDto.FirstName;
            student.LastName = studentDto.LastName;
            student.City = studentDto.City;
            student.BirthDate = studentDto.BirthDate;
            student.CourseId = studentDto.CourseId;
            db.SaveChanges();
            return Ok(student);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound("Student not found with ID = " + id);
            }
            db.Students.Remove(student);
            db.SaveChanges();
            return Ok("Student deleted successfully");
        }
    }

}