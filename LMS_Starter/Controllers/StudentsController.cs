using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS_Starter.Model;
using LMS_Starter.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS_Starter.Controllers
{
    [Route("api/students")]
    public class StudentsController : Controller
    {
        private ILMSDataHandler _dbHandler;
        public StudentsController(ILMSDataHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }
        // GET: api/students
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbHandler.GetAllStudents());
        }

        // GET: api/students/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_dbHandler.GetStudentById(id));
        }
        [HttpGet("credit/{id}")]
        public IActionResult GetCredit(int id)
        {
            return Ok(_dbHandler.GetStudentCredit(id));
        }
        // POST: api/students
        [HttpPost]
        public IActionResult Post([FromBody]Student student)
        {
            var newStudent = Student.CreateStudent(student);
            _dbHandler.AddStudent(newStudent);
            return Ok();
        }

        // PUT: api/students/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Student student)
        {
            var newStudent = Student.CreateStudent(student);
            _dbHandler.EditStudent(newStudent, id);
            return Ok();
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dbHandler.DeleteStudent(id);
            return Ok();
        }
    }
}
