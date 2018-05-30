using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS_Starter.Model;
using LMS_Starter.Service;
using System.Net;

namespace LMS_Starter.Controllers
{
    [Route("api/courses")]
    public class CoursesController : Controller
    {
        private IAuthentication _authentication;
        private ILMSDataHandler _dbHandler;
        public CoursesController(IAuthentication authentication,ILMSDataHandler dbHandler)
        {
            _authentication = authentication;
            _dbHandler = dbHandler;
        }
        // GET api/courses
        [HttpGet]
        public IActionResult Get()
        {
            if(_authentication.IsLogin(Request.Headers["Token"]))
            {
                return Ok(_dbHandler.GetAllCourses());
            }
            else
            {
                return Unauthorized();
                //Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                //return new JsonResult("Not Authorized");
            }
        }

        // GET api/courses/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (_authentication.IsLogin(Request.Headers["Token"]))
            {
                return Ok(_dbHandler.GetCourseById(id));
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST api/courses
        [HttpPost]
        public IActionResult Post([FromBody]Course course)
        {
            if (_authentication.IsLogin(Request.Headers["Token"]))
            {
                var newCourse = Course.CreateCourse(course);
                _dbHandler.AddCourse(newCourse);
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        // PUT api/courses/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Course course)
        {
            if (_authentication.IsLogin(Request.Headers["Token"]))
            {
                var newCourse = Course.CreateCourse(course);
                _dbHandler.EditCourse(newCourse,id);
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        // DELETE api/courses/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_authentication.IsLogin(Request.Headers["Token"]))
            {
                _dbHandler.DeleteCourse(id);
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
