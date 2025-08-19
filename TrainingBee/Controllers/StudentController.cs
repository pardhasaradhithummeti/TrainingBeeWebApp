using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingBee.Models;
using TrainingBee.Services;

namespace TrainingBee.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        StudentService studentService = null;
        CourseService courseService = null;
        public StudentController(StudentService s , CourseService c)
        {
            studentService = s;
            courseService = c;
        }
        [Authorize(Roles ="Admin,User")]
        public async Task<IActionResult> Index()
        {
            var courseList = await studentService.GetStudentsAsync();
            return View(courseList);
        }
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Details(int id)
        {
            var student = await studentService.GetStudentsByRollNoAsync(id);

            return View(student);
        }
        async Task<SelectList> GetCourses()
        {
            var courseList = await courseService.GetCoursesAsync();
            SelectList list = new SelectList(courseList , "CourseId" , "CourseName");
            return list;
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Courses =await GetCourses();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentDTO studentDTO)
        {
           if (!ModelState.IsValid)
            {
                studentDTO.CourseName = ""; 
                await studentService.CreateStudent(studentDTO);
                return RedirectToAction("Index");
            }
            ViewBag.Courses = await GetCourses();
            return View();
        }
       
    }
}
