using Attendance.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.Controllers
{
    public class CourseController : Controller
    {
        private readonly DataContext _ctx;

        public CourseController(DataContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (course is null)
            {
                return BadRequest();
            }

            _ctx.Add(course);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
