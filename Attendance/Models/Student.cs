using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.Models
{
    public class Student : BaseModel<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public IList<AttendanceModel> Attendance { get; set; } = new List<AttendanceModel>();
    }
}
