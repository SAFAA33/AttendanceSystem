using Attendance.Models.Abstraction;
using System;
using System.Collections.Generic;

namespace Attendance.Models
{
    public class Session : BaseModel<int>
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public int CheckIns { get; set; } = 0;
        public DateTime CreatedAt { get; set; }

        // I don't need a created by property, because only the one who created the course is allowed to create session.
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public IList<AttendanceModel> Attendees { get; set; } = new List<AttendanceModel>();
    }
}