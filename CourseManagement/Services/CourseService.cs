using CourseManagement.DataAccessLayer;
using CourseManagement.Entities;
using CourseManagement.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CourseManagement.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        private readonly byte[] salt = Encoding.ASCII.GetBytes("opakjogpkjdopajgkoirkjatki");

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckCoursePIN(int id, string pin)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            var hashedPin = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: pin,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));

            return hashedPin == course.EditDeleteCoursePIN;
        }

        public async Task<bool> CheckIfCourseCapacityFull(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            var courseAttendeesCount = await _context.Attendees.Where(x => x.CourseId == id).CountAsync();
            return courseAttendeesCount + 1 <= course.MaxNumberOfAtendees;
        }

        public async Task CreateCourse(CourseNewEditModel course)
        {
            Course courseToCreate = new Course
            {
                CourseTitle = course.CourseTitle,
                CourseDescription = course.CourseDescription,
                CourseTeacher = course.CourseTeacher,
                CourseTeacherEmail = course.CourseTeacherEmail,
                CourseStartDateTime = course.CourseStartDateTime,
                MaxNumberOfAtendees = course.MaxNumberOfAtendees,

                EditDeleteCoursePIN = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: course.EditDeleteCoursePIN,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8)),
            };

            _context.Courses.Add(courseToCreate);
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

        public async Task<List<CoursesListModel>> GetAllCourses()
        {
            return await _context.Courses.Select(x => new CoursesListModel
            {
                Id = x.Id,
                CourseTitle = x.CourseTitle,
                CourseStartDateTime = x.CourseStartDateTime,
                CourseTeacher = x.CourseTeacher,
                CourseCapacity = $"{x.CourseAttendees.Count}/{x.MaxNumberOfAtendees}",
                IsCapacityFull = x.CourseAttendees.Count == x.MaxNumberOfAtendees
            }).ToListAsync();
        }

        public async Task<CourseNewEditModel> GetCourseByIdForCreateEdit(int id)
        {
            var course = await _context.Courses.Include(x => x.CourseAttendees).FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                throw new Exception("Selected course cannot be found.");
            }
            return new CourseNewEditModel
            {
                Id = course.Id,
                CourseTitle = course.CourseTitle,
                CourseDescription = course.CourseDescription,
                CourseTeacher = course.CourseTeacher,
                CourseTeacherEmail = course.CourseTeacherEmail,
                CourseStartDateTime = course.CourseStartDateTime,
                MaxNumberOfAtendees = course.MaxNumberOfAtendees,
                EditDeleteCoursePIN = string.Empty,
                CourseAttendees = new List<Attendee>(course.CourseAttendees),
            };
        }
        public async Task<CourseDetailsModel> GetCourseDetailsById(int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                throw new Exception("Selected course cannot be found.");
            }

            var courseAttendeeCount = await _context.Attendees.Where(x => x.CourseId == id).CountAsync();
            return new CourseDetailsModel
            {
                CourseTitle = course.CourseTitle,
                CourseDescription = course.CourseDescription,
                CourseTeacher = course.CourseTeacher,
                CourseTeacherEmail = course.CourseTeacherEmail,
                CourseStartDateTime = course.CourseStartDateTime,
                CourseCapacity = $"{courseAttendeeCount}/{course.MaxNumberOfAtendees}"
            };
        }

        public async Task UpdateCourse(int id, CourseNewEditModel course)
        {
            var courseFromDb = await _context.Courses.FindAsync(id);
            if (courseFromDb == null)
            {
                throw new Exception("Selected course cannot be found");
            }
            courseFromDb.CourseTitle = course.CourseTitle;
            courseFromDb.CourseDescription = course.CourseDescription;
            courseFromDb.CourseTeacher = course.CourseTeacher;
            courseFromDb.CourseTeacherEmail = course.CourseTeacherEmail;
            courseFromDb.CourseStartDateTime = course.CourseStartDateTime;
            courseFromDb.MaxNumberOfAtendees = course.MaxNumberOfAtendees;

            if (await CheckCoursePIN(id, course.EditDeleteCoursePIN))
            {
                await _context.SaveChangesAsync();

            }
            else
            {
                throw new Exception("Incorrect PIN, changes not saved!");
            }
        }
    }
}
