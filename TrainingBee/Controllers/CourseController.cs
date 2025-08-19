using Microsoft.AspNetCore.Mvc;
using TrainingBee.Models;
using TrainingBee.Services;

namespace TrainingBee.Controllers
{
    public class CourseController : Controller
    {
        CourseService courseService = null;
        public CourseController(CourseService service)
        {
            courseService = service;
        }
        public async Task<IActionResult> Index()
        {
            var courseList = await courseService.GetCoursesAsync();
            return View(courseList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var course = await courseService.GetCoursesByIdAsync(id);
            return View(course);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseDTO course)
        {
            if (ModelState.IsValid)
            {
                await courseService.CreateCourseAsync(course);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
