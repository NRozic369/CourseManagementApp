using CourseManagement.Models;

namespace CourseManagement.Services
{
    public interface ICourseService
    {
        public Task<List<Course>> GetAllCourses();
        public Task<Course> GetCourseById(int id);
        public Task CreateCourse(Course course);
        public Task UpdateCourse(int id, Course course);
        public Task DeleteCourse(int id);
    }
}
