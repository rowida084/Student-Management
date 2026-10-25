using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Services
{
    public class CourseService
    {
        private AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<CourseResponse?> getCourseByID(int id)
        {
            return await _context.Courses.Where(c => c.CourseId == id)
                .Select(c=>new CourseResponse
                {
                    CourseId = c.CourseId,
                    Title = c.Title,
                    Description = c.Description,
                    Credits = c.Credits,
                })
           .FirstOrDefaultAsync();
        }

        public async Task<Course?> FindCourse(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseId == id);
            return course;
        }
        public async Task AddCourseAsync(Course course)
        {
          await  _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveCourseAsync(int id)
        {
            var course = await FindCourse(id);
            if(course ==null) 
                return false;
            return true;
        }

        public async Task<List<StudentResponse>> GetCoutseStudentsAsync (int id)
        {
            return await _context.Enrollments.Where(e => e.CourseId == id)
                .Select(e => new StudentResponse
                {
                    Id = e.StudentId,
                    FullName = e.Student.FullName,
                    DateOfBirth = e.Student.DateOfBirth,
                    EnrollmentDate = e.Student.EnrollmentDate,
                    Email = e.Student.Email
                }).ToListAsync();
        }

        public async Task<double?> GetCourseGradesAverage(int id)
        {
            return await _context.Enrollments.
                  Where(e => e.CourseId == id && e.Grade != null).
                  AverageAsync(e => (double?)e.Grade);
        }
    }
}
