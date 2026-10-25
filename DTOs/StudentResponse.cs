using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class StudentResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime EnrollmentDate { get; set; }
    
        public string Email { get; set; } = string.Empty;
    }
}
