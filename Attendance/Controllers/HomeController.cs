using Attendance.Models;
using Attendance.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.Controllers
{
    public class HomeController : UserController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _ctx;

        public HomeController(ILogger<HomeController> logger, DataContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }


        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {

            ViewData["Courses"] = new SelectList(_ctx.Courses.Where(x => x.UserId == UserId).ToList(), "Id", "Name");
            var courses = await _ctx.Courses
                .Include(x => x.Sessions)
                    .ThenInclude(y => y.Attendees)
                        .ThenInclude(a => a.User)
                .Where(x => x.UserId == UserId)
                .ToListAsync();
            return View(courses);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
