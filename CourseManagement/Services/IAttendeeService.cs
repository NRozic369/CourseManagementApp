using CourseManagement.Models;

namespace CourseManagement.Services
{
    public interface IAttendeeService
    {
        public Task<AttendeeModel> GetAttendeeById(int id);
        public Task<List<AttendeeModel>> GetAttendeesByCourseId(int id);
        public Task CreateAttendee(AttendeeModel attendee);
        public Task UpdateAttendee(int id, AttendeeModel attendee);
        public Task DeleteAttendee(int id);
        public Task<bool> CheckIfAttendeeExists(int courseId, string firstName, string lastName, string email);
    }
}
