using Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.ViewModels.Forms
{
    public class CheckInForm
    {
        public string StudentId { get; set; }
        public int SessionId { get; set; }
    }
}
