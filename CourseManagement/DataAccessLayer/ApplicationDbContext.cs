using CourseManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
    }
}
