using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS_Starter.Model;
using LMS_Starter.Service;


namespace LMS_Starter
{
    [Route("api/lecturers")]
    public class LecturerController : Controller
    {
        private ILMSDataHandler _dbHandler;
        public LecturerController(ILMSDataHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }
        // GET: api/Lecturers
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbHandler.GetAllLecturers());
        }

        // GET: api/Lecturers/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_dbHandler.GetLecturerById(id));
        }

        // POST: api/Lecturer
        [HttpPost]
        public IActionResult Post([FromBody]Lecturer lecturer)
        {
            var newLecturer = Lecturer.CreateLecturerFromBody(lecturer);
            _dbHandler.AddLecturer(newLecturer);
            return Ok();
        }

        // PUT: api/Lecturers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Lecturer lecturer)
        {
            var newLecturer = Lecturer.CreateLecturerFromBody(lecturer);
            _dbHandler.EditLecturer(newLecturer, id);
            return Ok();
        }

        // DELETE: api/lecturers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dbHandler.DeleteLecturer(id);
            return Ok();
        }
    }
}
