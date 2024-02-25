using CourseManagement.DataAccessLayer;
using CourseManagement.Entities;
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

        public async Task CreateAttendee(AttendeeModel attendee)
        {
            Attendee attendeeToCreate = new Attendee
            {
                FirstName = attendee.FirstName,
                LastName = attendee.LastName,
                Email = attendee.Email,
                CourseId = attendee.CourseId,
            };

            _context.Attendees.Add(attendeeToCreate);
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

        public async Task<AttendeeModel> GetAttendeeById(int id)
        {
            var attendee = await _context.Attendees.FindAsync(id);
            if (attendee == null)
            {
                throw new Exception("Selected attendee cannot be found.");
            }

            return new AttendeeModel
            {
                Id = attendee.Id,
                FirstName = attendee.FirstName,
                LastName = attendee.LastName,
                Email = attendee.Email,
                CourseId = attendee.CourseId
            };
        }

        public async Task<List<AttendeeModel>> GetAttendeesByCourseId(int id)
        {
            var attendees = await _context.Attendees.Where(x => x.CourseId == id).Select(x => new AttendeeModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                CourseId = x.CourseId
            }).ToListAsync();

            return attendees;
        }

        public async Task UpdateAttendee(int id, AttendeeModel attendee)
        {
            var attendeeToUpdate = await _context.Attendees.FindAsync(id);
            if (attendeeToUpdate == null)
            {
                throw new Exception("Selected attendee not found.");
            }

            attendeeToUpdate.FirstName = attendee.FirstName;
            attendeeToUpdate.LastName = attendee.LastName;
            attendeeToUpdate.Email = attendee.Email;

            await _context.SaveChangesAsync();
        }
    }
}
