using CourseManagement.Infrastructure;
using CourseManagement.Models;

namespace CourseManagement.Services
{
    public interface ICourseService
    {
        public Task<PageResponse<CoursesListModel>> GetAllCourses(int curPage, int pageSize, string searchString);
        public Task<ProcessResponse<CourseNewEditModel>> GetCourseByIdForCreateEdit(int id);
        public Task<ProcessResponse<CourseDetailsModel>> GetCourseDetailsById(int id);
        public Task<ProcessResponse> CreateCourse(CourseNewEditModel course);
        public Task<ProcessResponse> UpdateCourse(int id, CourseNewEditModel course);
        public Task<ProcessResponse> DeleteCourse(int id);
        public Task<bool> CheckIfCourseCapacityFull(int id);
        public Task<bool> CheckCoursePIN(int id, string pin);
    }
}
