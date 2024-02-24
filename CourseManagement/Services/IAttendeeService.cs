using CourseManagement.Models;

namespace CourseManagement.Services
{
    public interface IAttendeeService
    {
        public Task<Attendee> GetAttendeeById(int id);
        public Task<List<Attendee>> GetAttendeesByCourseId(int id);
        public Task CreateAttendee(Attendee attendee);
        public Task UpdateAttendee(int id, Attendee attendee);
        public Task DeleteAttendee(int id);
        public Task<bool> CheckIfAttendeeExists(int courseId, string firstName, string lastName, string email);
    }
}
