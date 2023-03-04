using Microsoft.AspNetCore.Mvc;
using auth.Interfaces;
using auth.Model;
using auth.Authorization;

namespace auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class StudentsController : Controller
    {
        public IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }


        [HttpGet]
        public async Task<IEnumerable<StudentViewModel>> GetStudents()
        {
            var students = await _studentService.GetStudents();
            return students;
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id) 
        {
            var student = _studentService.GetStudentById(id);
            return Ok(student);
        }

        [HttpPost("AddStudent")]
        public IActionResult AddStudent(Student model)
        {
            _studentService.AddStudent(model);
            return Ok(new { message = "Student created successfully" });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Student model)
        {
            _studentService.UpdateStudent(id,model);
            return Ok(new {message="Student Updated successfully"});
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _studentService.Delete(id);
            return Ok(new { message = "Student deleted successfully" });
        }
    }
}
