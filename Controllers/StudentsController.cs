using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Services;
namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentsController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetStudentByID(int id)
        {
            var student = await _studentService.GetStudentByIDAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpGet("search")]
        public async Task<ActionResult> GetStudentByName([FromQuery]string Name)
        {
            var students = await _studentService.GetStudentByNameAsync(Name);
            if (students.Count == 0)
            {
                return NotFound();
            }

            return Ok(students);
        }

        [HttpPost]
        public async Task<ActionResult> CreateStudent(StudentRequest Request)
        {
            var IsEmailExist= await _studentService.EmailExistsAsync(Request.Email);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(IsEmailExist)
            {
                return Conflict("This Email Is Already Exist!");
            }
            var NewStudent = new Student
            {
                FullName = Request.FullName,
                Email = Request.Email,
                DateOfBirth = Request.DateOfBirth,
                EnrollmentDate = Request.EnrollmentDate
            };
            await _studentService.AddStudentAsync(NewStudent);
            return StatusCode(201, NewStudent);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent(int id,Student update)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            

            var student =await _studentService.FindStudentAsync(id);

            if(student == null)
                return NotFound(ModelState);
            
       
            var IsEmailExist = await _studentService.EmailExistsAsync(update.Email,id);

            if (IsEmailExist)
                return Conflict("This Email Is Already Exists!");

          await _studentService.Update(update, student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            bool IsDeleted= await _studentService.DeleteStudentAsync(id);
            if(IsDeleted)
                { return NoContent(); }
            return NotFound();
        }

        [HttpGet("id/course")]
        public async Task<ActionResult> GetStudentCourse(int id)
        {
            var student = await _studentService.GetStudentByIDAsync(id);
            if (student == null)
                return NotFound();
            var students= await _studentService.GetStudentCourseAsync(id);
            return Ok(students);
        }

    }
    }
