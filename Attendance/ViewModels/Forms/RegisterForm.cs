using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.ViewModels.Forms
{
    public class RegisterForm
    {
        public string ReturnUrl { get; set; }
        public string Name { get; set; }
        public Roles Role { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
    public enum Roles
    {
        Student,
        Teacher
    }
}
