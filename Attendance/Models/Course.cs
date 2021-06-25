using Attendance.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.Models
{
    public class Course: BaseModel<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        // I mean the teacher only, because a course can be created by teachers only,
        // so user cannot be null and must contain only teacher Id
        public string UserId { get; set; }
        public User User { get; set; }
        public IList<Session> Sessions { get; set; } = new List<Session>();
    }
}
