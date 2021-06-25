using System;
using System.Collections.Generic;

namespace Attendance.Models
{
    public class Session : BaseModel<int>
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public int CheckIns { get; set; } = 0;
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public IList<AttendanceModel> Attendees { get; set; } = new List<AttendanceModel>();
    }
}