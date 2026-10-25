using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class StudentRequest
    {
        [Required]
        [MaxLength(150)]
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime EnrollmentDate { get; set; }
        [Required]
        [EmailAddress]
        public string Email {  get; set; }= string.Empty;

    }
}
