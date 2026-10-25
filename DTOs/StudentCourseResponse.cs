namespace WebApplication1.DTOs
{
    public class StudentCourseResponse
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public int? Grade { get; set; }
    }
}
