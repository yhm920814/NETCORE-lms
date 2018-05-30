using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS_Starter.Model;
using LMS_Starter.Service;

namespace LMS_Starter.Controllers
{
    [Route("api/Enrolment")]
    public class EnrolmentController : Controller
    {
        private ILMSDataHandler _dbHandler;
        public EnrolmentController(ILMSDataHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }

        
        // POST: api/Enrolment/student
        [HttpPost("student")]
        public void EnrolStudent([FromBody]EnrolmentData data)
        {
            _dbHandler.EnrolStudentToCourse(data.CourseId, data.StudentId);
        }
        // POST: api/Enrolment/lecturer
        [HttpPost("lecturer")]
        public void EnrolLecturer([FromBody]EnrolmentData data)
        {
            _dbHandler.EnrolLecturerToCourse(data.CourseId,data.LecturerId);
        }
        
        // DELETE: api/enrolment/student
        [HttpDelete("student")]
        public void WithdrawStudent([FromBody]EnrolmentData data)
        {
            _dbHandler.WithdrawStudentFromCourse(data.CourseId, data.StudentId);
        }
        // DELETE: api/enrolment/lecturer
        [HttpDelete("lecturer")]
        public void WithdrawLecturer([FromBody]EnrolmentData data)
        {
            _dbHandler.WithdrawLecturerFromCourse(data.CourseId, data.LecturerId);
        }
    }
    public class EnrolmentData
    {
        public int CourseId;
        public int StudentId;
        public int LecturerId;
    }
}
