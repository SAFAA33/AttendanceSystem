using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.ViewModels
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public TimeSpan SessionDuration { get; set; }
        public int Duration { get; set; }
        public string QRCode { get; set; }

    }
}
