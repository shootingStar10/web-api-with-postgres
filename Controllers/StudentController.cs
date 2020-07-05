using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApplication2.Models;
using MyWebApplication2.Services;

namespace MyWebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentModel student)
        {
            if (student == null)
            {
                return BadRequest("Student can not be empty.");
            }

            var isValid = _studentService.ValidateStudentModel(student);

            if (!isValid)
            {
                return BadRequest("Student model is not valid");
            }

            student.RollNo = Guid.NewGuid().ToString().Substring(0, 16);
            await _studentService.CreateStudent(student).ConfigureAwait(false);

            return Created("api/student", student);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var students = _studentService.GetAllStudents();

            return Ok(students);
        }

        [HttpGet("{roll_no}")]
        public IActionResult GetStudentByRollNo(string roll_no)
        {
            var (student, error) = _studentService.GetStudentByRollNo(roll_no);

            if (error != null)
            {
                return NotFound(error);
            }

            return Ok(student);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudentRecord([FromBody] StudentModel student)
        {
            if (student == null)
            {
                return BadRequest("Empty payload.");
            }

            var error = await _studentService.UpdateStudentRecord(student).ConfigureAwait(false);

            if (error != null)
            {
                return NotFound(error);
            }

            return NoContent();
        }

        [HttpDelete("{roll_no}")]
        public async Task<IActionResult> DeleteStudentRecord(string roll_no)
        {
            var error = await _studentService.DeleteStudentRecord(roll_no).ConfigureAwait(false);

            if (error != null)
            {
                return NotFound(error);
            }

            return NoContent();
        }
    }
}