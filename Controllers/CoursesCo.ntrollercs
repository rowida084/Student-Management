using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CoursesController(CourseService courseService)
        {
            this._courseService = courseService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCourse(int id)
        {
            var course=await _courseService.getCourseByID(id);
            if (course == null)
                return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCourse(CreateCourseRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var course = new Course
            {
            Title = request.Title,
            Description = request.Description,
            Credits = request.Credits
            };
            await _courseService.AddCourseAsync(course);
            return StatusCode(201, course);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            bool CourseIsDeleted = await _courseService.RemoveCourseAsync(id);
            if (CourseIsDeleted)
                return NoContent();
            return NotFound();
        }

        [HttpGet("courses/id/students")]
        public async Task<ActionResult> GetStudentsCourse(int id)
        {
            var CourseExists = await _courseService.FindCourse(id);
            if (CourseExists == null)
                return NotFound();
            var students = await _courseService.GetCoutseStudentsAsync(id);
            return Ok(students);
        }

        [HttpGet("course/id/average_grade")]
        public async Task<ActionResult> GetCoutseAverageGrades(int id)
        {
            var course =  await _courseService.FindCourse(id);
            if(course == null)
                return NotFound (nameof(course));
            var students = await _courseService.GetCoutseStudentsAsync (id);
            return Ok(students);
        }
    }
}
