using Attendance.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.Models
{
    public class User : BaseModel<string>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public Role Role { get; set; }

        // For many to many relationships, it can be null tho
        public IList<AttendanceModel> Attendance { get; set; } = new List<AttendanceModel>();
        public IList<Course> Courses { get; set; } = new List<Course>();
    }
}