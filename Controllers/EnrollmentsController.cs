using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly EnrollmentService _enrollmentService;

        public EnrollmentsController(EnrollmentService enrollmentService)
        {
            this._enrollmentService = enrollmentService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewEnrollment(CreateEnrollmentRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            bool IsStudentExist = await _enrollmentService.StudentExistsAsync(request.StudentId);
            bool IsCourseExist = await _enrollmentService.CourseExistsAsync(request.CourseId);
            if(!IsStudentExist||!IsCourseExist)
                return NotFound();

            bool IsExist = await _enrollmentService.IsEnrollmentExist(request);
            if (IsExist)
                return Conflict("student already enroll in this course!");

            await _enrollmentService.AddNewEnrollment(request);
            return StatusCode(201);
        }
    }
}
