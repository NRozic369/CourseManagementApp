using CourseManagement.Infrastructure;
using CourseManagement.Models;

namespace CourseManagement.Services
{
    public interface IAttendeeService
    {
        public Task<ProcessResponse<AttendeeModel>> GetAttendeeById(int id);
        public Task<ProcessResponse<List<AttendeeModel>>> GetAttendeesByCourseId(int id);
        public Task<ProcessResponse> CreateAttendee(AttendeeModel attendee);
        public Task<ProcessResponse> UpdateAttendee(int id, AttendeeModel attendee);
        public Task<ProcessResponse> DeleteAttendee(int id);
        public Task<bool> CheckIfAttendeeExists(int courseId, string firstName, string lastName, string email);
    }
}
