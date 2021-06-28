using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Attendance
{
    public struct AttendanceConstants
    {
        public struct Policies
        {
            public const string Teacher = nameof(Teacher);
            public const string Admin = nameof(Admin);
            public const string Student = nameof(Student);
        }

        public struct IdentityResources
        {
            public const string RoleScope = "role";
        }

        public struct Claims
        {
            public const string Role = "role";
            public static readonly Claim TeacherClaim = new Claim(Role, Roles.Teacher);
        }

        public struct Roles
        {
            public const string Teacher = nameof(Teacher);
            public const string Admin = nameof(Admin);
            public const string Student = nameof(Student);
        }
    }
}
