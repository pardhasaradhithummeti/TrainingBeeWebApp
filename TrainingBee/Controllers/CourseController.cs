using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingBee.Models;
using TrainingBee.Services;

namespace TrainingBee.Controllers
{

    [Authorize]
    public class CourseController : Controller
    {
        CourseService courseService = null;
        //injecting the course service instance
        public CourseController(CourseService service)
        {
            courseService = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var courseList = await courseService.GetCoursesAsync();
            return View(courseList);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var course = await courseService.GetCoursesByIdAsync(id);
            return View(course);
        }
        [Authorize(Roles = "Admin")]
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
