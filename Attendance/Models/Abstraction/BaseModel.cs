using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.Models.Abstraction
{
    public class BaseModel<TKey>
    {
        public TKey Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
