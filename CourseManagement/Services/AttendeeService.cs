using CourseManagement.DataAccessLayer;
using CourseManagement.Entities;
using CourseManagement.Infrastructure;
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

        public async Task<ProcessResponse> CreateAttendee(AttendeeModel attendee)
        {
            Attendee attendeeToCreate = new Attendee
            {
                FirstName = attendee.FirstName,
                LastName = attendee.LastName,
                Email = attendee.Email,
                CourseId = attendee.CourseId,
            };

            try
            {
                _context.Attendees.Add(attendeeToCreate);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ProcessResponse
                {
                    IsSuccessful = false,
                    Message = "Creating attendee failed"
                };
            }

            return new ProcessResponse
            {
                IsSuccessful = true,
                Message = "Attendee created"
            };
        }

        public async Task<ProcessResponse> DeleteAttendee(int id)
        {
            var attendeeToDelete = await _context.Attendees.FindAsync(id);
            if (attendeeToDelete == null)
            {
                return new ProcessResponse
                {
                    IsSuccessful = false,
                    Message = "Selected attendee cannot be found"
                };
            }
            try
            {
                _context.Attendees.Remove(attendeeToDelete);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return new ProcessResponse
                {
                    IsSuccessful = false,
                    Message = "Deleting attendee failed"
                };
            }

            return new ProcessResponse
            {
                IsSuccessful = true,
                Message = "Attendee successfully deleted"
            };

        }

        public async Task<ProcessResponse<AttendeeModel>> GetAttendeeById(int id)
        {
            var attendee = await _context.Attendees.FindAsync(id);
            if (attendee == null)
            {
                return new ProcessResponse<AttendeeModel>
                {
                    IsSuccessful = false,
                    Message = "Selected attendee cannot be found",
                    Data = null
                };
            }

            var result = new AttendeeModel
            {
                Id = attendee.Id,
                FirstName = attendee.FirstName,
                LastName = attendee.LastName,
                Email = attendee.Email,
                CourseId = attendee.CourseId
            };

            return new ProcessResponse<AttendeeModel>
            {
                IsSuccessful = true,
                Message = "Selected attendee displayed",
                Data = result
            };
        }

        public async Task<ProcessResponse<List<AttendeeModel>>> GetAttendeesByCourseId(int id)
        {
            var attendees = await _context.Attendees.Where(x => x.CourseId == id).Select(x => new AttendeeModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                CourseId = x.CourseId
            }).ToListAsync();

            return new ProcessResponse<List<AttendeeModel>>
            {
                IsSuccessful = true,
                Message = "Attendees for selected course displayed",
                Data = attendees
            };
        }

        public async Task<ProcessResponse> UpdateAttendee(int id, AttendeeModel attendee)
        {
            var attendeeToUpdate = await _context.Attendees.FindAsync(id);
            if (attendeeToUpdate == null)
            {
                return new ProcessResponse
                {
                    IsSuccessful = false,
                    Message = "Selected attendee cannot be found"
                };
            }

            try
            {
                attendeeToUpdate.FirstName = attendee.FirstName;
                attendeeToUpdate.LastName = attendee.LastName;
                attendeeToUpdate.Email = attendee.Email;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ProcessResponse
                {
                    IsSuccessful = false,
                    Message = "Selected attendee update failed"
                };
            }

            return new ProcessResponse
            {
                IsSuccessful = true,
                Message = "Attendee succesfully updated"
            };
        }
    }
}
