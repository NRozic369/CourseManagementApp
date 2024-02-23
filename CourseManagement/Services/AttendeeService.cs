using CourseManagement.DataAccessLayer;
using CourseManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Services
{
    public class AttendeeService : IAttendeeService
    {
        private readonly ApplicationDbContext _context;
        public AttendeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfAttendeeExists(int courseId, string firstName, string lastName, string email)
        {
            var courseAttendees = await _context.Attendees.Where(x => x.CourseId == courseId).ToListAsync();
            return courseAttendees.Any(x => x.FirstName == firstName && x.LastName == lastName && x.Email == email);
        }

        public async Task CreateAttendee(Attendee attendee)
        {
            _context.Attendees.Add(attendee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAttendee(int id)
        {
            var attendeeToDelete = await _context.Attendees.FindAsync(id);
            if (attendeeToDelete == null)
            {
                throw new Exception("Selected attendee cannot be found.");
            }
            _context.Attendees.Remove(attendeeToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Attendee>> GetAttendeesByCourseId(int id)
        {
            var attendees = await _context.Attendees.Where(x => x.CourseId == id).ToListAsync();
            return attendees;
        }

        public Task UpdateAttendee(int id, Attendee attendee)
        {
            throw new NotImplementedException();
        }


    }
}
