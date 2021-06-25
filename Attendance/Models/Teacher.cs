namespace Attendance.Models
{
    public class Teacher : BaseModel<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }

    }
}
