using CourseManagement.DataAccessLayer;
using CourseManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourse(int id)
        {
            var courseToDelete = await _context.Courses.FindAsync(id);
            if (courseToDelete == null)
            {
                throw new Exception("Selected course cannot be found.");
            }

            _context.Courses.Remove(courseToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Course>> GetAllCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> GetCourseById(int id)
        {
            var course = await _context.Courses.Include(x => x.CourseAttendees).FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                throw new Exception("Selected course cannot be found.");
            }
            return course;
        }

        public async Task UpdateCourse(int id, Course course)
        {
            var courseFromDb = await _context.Courses.FindAsync(id);
            if (courseFromDb == null)
            {
                throw new Exception("Selected course cannot be found");
            }
            courseFromDb.CourseTitle = course.CourseTitle;
            courseFromDb.CourseDescription = course.CourseDescription;
            courseFromDb.CourseStartDateTime = course.CourseStartDateTime;

            await _context.SaveChangesAsync();
        }
    }
}
