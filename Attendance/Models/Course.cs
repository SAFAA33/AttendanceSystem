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

        public IList<Session> Sessions { get; set; } = new List<Session>();
    }
}
