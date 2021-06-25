using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.Models
{
    public class AttendanceModel
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public DateTime AttendedAt { get; set; } = DateTime.Now;
    }
}
