using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Services
{
    public class EnrollmentService
    {
        private AppDbContext _context;
        public EnrollmentService(AppDbContext context)
        {
           this. _context = context;
        }

        public async Task<bool> IsEnrollmentExist(CreateEnrollmentRequest request)
        {
          return await _context.Enrollments.AnyAsync(e=>e.StudentId==request.StudentId
            &&e.CourseId==request.CourseId);
            
        }

        public async Task<bool> StudentExistsAsync(int studentId)
        {
            return await _context.Students
                .AnyAsync(s => s.StudentId == studentId);
        }

        public async Task<bool> CourseExistsAsync(int courseId)
        {
            return await _context.Courses
                .AnyAsync(c => c.CourseId == courseId);
        }
        public async Task AddNewEnrollment(CreateEnrollmentRequest request)
        {
            var enrollment = new Enrollment
            {
                StudentId = request.StudentId,
                CourseId = request.CourseId,
                EnrollmentDate = DateTime.Now,
            };
            await _context.Enrollments.AddAsync(enrollment);
           await _context.SaveChangesAsync();
        }


    }
}
