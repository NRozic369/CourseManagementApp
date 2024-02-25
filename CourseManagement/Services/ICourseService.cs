using CourseManagement.Models;

namespace CourseManagement.Services
{
    public interface ICourseService
    {
        public Task<List<CoursesListModel>> GetAllCourses();
        public Task<CourseNewEditModel> GetCourseByIdForCreateEdit(int id);
        public Task<CourseDetailsModel> GetCourseDetailsById(int id);
        public Task CreateCourse(CourseNewEditModel course);
        public Task UpdateCourse(int id, CourseNewEditModel course);
        public Task DeleteCourse(int id);
        public Task<bool> CheckIfCourseCapacityFull(int id);
    }
}
