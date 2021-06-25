using Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.ViewModels.Forms
{
    public class SessionForm
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public int CourseId { get; set; }
    }
}
