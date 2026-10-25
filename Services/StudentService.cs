using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Services
{
    public class StudentService
    {
        private AppDbContext _context;
        public StudentService(AppDbContext context) 
        {
            this._context = context;
        }

      
        public Task<List<StudentResponse>> GetAllAsync()
        {
           return _context.Students.AsNoTracking().
                Select(s=>new StudentResponse
                {
                    Id=s.StudentId,
                    FullName=s.FullName,
                    Email=s.Email,
                    DateOfBirth=s.DateOfBirth,
                    EnrollmentDate=s.EnrollmentDate
                }).ToListAsync();
        }

        public Task<StudentResponse?> GetStudentByIDAsync(int id)
        {
            var student = _context.Students.AsNoTracking().
                Where(s=>s.StudentId==id)
                .Select(s => new StudentResponse
                {
                    Id = s.StudentId,
                    FullName = s.FullName,
                    Email = s.Email,
                    DateOfBirth = s.DateOfBirth,
                    EnrollmentDate = s.EnrollmentDate
                }
                ).FirstOrDefaultAsync();

            return student;
        }

        public Task<List<StudentResponse>> GetStudentByNameAsync(string name)
        {
            var sutdent = _context.Students.AsNoTracking()
                .Where(s => s.FullName.ToLower() == name.ToLower()).
                Select(s => new StudentResponse
                {
                    Id = s.StudentId,
                    FullName = s.FullName
                    ,
                    Email = s.Email,
                    EnrollmentDate = s.EnrollmentDate
                   ,
                    DateOfBirth = s.DateOfBirth,
                }).ToListAsync();
            return sutdent;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Students.
                AnyAsync(s => s.Email.ToLower() == email.ToLower());
        }

        public async Task AddStudentAsync(Student student)
        {
             _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

       public async Task<Student?> FindStudentAsync(int id)
        {
            var student =await _context.Students
               .FirstOrDefaultAsync(s => s.StudentId == id);

            return student;
        }

        public async Task Update(Student update, Student student)
        {
            student.FullName = update.FullName;
            student.DateOfBirth = update.DateOfBirth;
            student.EnrollmentDate = update.EnrollmentDate;
            student.Email = update.Email;
           // _context.Update(student); // the same dbcontext
          await  _context.SaveChangesAsync();
        }

        public async Task<bool> EmailExistsAsync(string email, int studentId)
        {
            return await _context.Students
                .AnyAsync(s => s.Email == email && s.StudentId != studentId);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await FindStudentAsync(id);
            if (student == null)
                return false;
            _context.Students.Remove(student);
         await  _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<StudentCourseResponse>> GetStudentCourseAsync(int id)
        {
             return await _context.Enrollments
                .Where(s => s.StudentId == id).
                Select(s => new StudentCourseResponse
                {
                    CourseId = s.CourseId,
                    CourseName = s.Course.Title,
                    Grade = s.Grade,
                }).ToListAsync();
        }
    }
}
