using Attendance.Models;
using Attendance.ViewModels;
using Attendance.ViewModels.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.Controllers
{
    public class SessionController : Controller
    {
        private readonly DataContext _ctx;

        public SessionController(DataContext ctx)
        {
            _ctx = ctx;
        }

        // Do i need it?
        public IActionResult Create()
        {
            ViewData["Courses"] = new SelectList(_ctx.Courses.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SessionForm sessionForm)
        {
            var session = new Session
            {
                Name = sessionForm.Name,
                Duration = sessionForm.Duration,
                CourseId = sessionForm.CourseId
            };

            _ctx.Add(session);
            await _ctx.SaveChangesAsync();

            return RedirectToAction(nameof(SessionRoom), session);
        }

        public IActionResult SessionRoom(Session session)
        {
            int duration = session.Duration;
            TimeSpan time = new TimeSpan(duration, 0, 0);
            string QRCode = "https://youtube.com";

            return View(new SessionViewModel
            {
                Id = session.Id,
                QRCode = QRCode,
                Duration = session.Duration,
                SessionDuration = time
            });
        }

        public IActionResult CheckIn(int sessionId)
        {
            var session = _ctx.Sessions.FirstOrDefault(x => x.Id.Equals(sessionId));
            if (session is null)
            {
                return NotFound();
            }

            var checkinForm = new CheckInForm { SessionId = session.Id };
            return View(checkinForm);
        }

        [HttpPost]
        public async Task<IActionResult> CheckIn(CheckInForm checkInForm)
        {
            // validation only
            if (checkInForm.StudentId == 0)
            {
                return BadRequest();
            }

            // Check if student exists.
            var student = _ctx.Students.FirstOrDefault(x => x.Id.Equals(checkInForm.StudentId));
            if (student is null)
            {
                // TODO: should return student Id does not exists, please register
                return NotFound();
            }



            // check-in student to a session
            _ctx.Attendance.Add(new AttendanceModel
            {
                SessionId = checkInForm.SessionId,
                StudentId = checkInForm.StudentId
            });
            await _ctx.SaveChangesAsync();

            return Ok();
        }
        private void StartSession()
        {

        }
    }
}
