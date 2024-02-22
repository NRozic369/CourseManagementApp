using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string CourseTitle { get; set; } = null!;
        [StringLength(4000)]
        public string? CourseDescription { get; set; }
        [Required]
        public DateTime CourseStartDateTime { get; set; }
        public List<Attendee> CourseAttendees { get; set; } = new List<Attendee>();
    }
}
