using Attendance.Models;
using Attendance.ViewModels.Forms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.Controllers
{
    public class CourseController : UserController
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
        [Authorize(Policy = AttendanceConstants.Policies.Teacher)]
        public async Task<IActionResult> Create(CourseForm courseForm)
        {
            if (courseForm is null)
            {
                return BadRequest();
            }

            var course = new Course
            {
                Name = courseForm.Name,
                Description = courseForm.Description,
                UserId = UserId
            };

            _ctx.Add(course);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
