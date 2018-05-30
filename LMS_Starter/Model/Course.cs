using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Starter.Model
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
        public int MaxStudents { get; set; }
        public int CurrentStudents { get; set; }
        public int Credit { get; set; }
        public ICollection<CourseToStudent> CourseToStudents { get; set; } 
        public ICollection<CourseToLecturer> CourseToLecturers { get; set; }
        public static Course CreateCourse(Course courseFromBody)
        {
            var newCourse = new Course();
            newCourse.Name = courseFromBody.Name;
            newCourse.Desc = courseFromBody.Desc;
            newCourse.ImageUrl = courseFromBody.ImageUrl;
            newCourse.MaxStudents = courseFromBody.MaxStudents;
            newCourse.CurrentStudents = courseFromBody.CurrentStudents;
            newCourse.Credit = courseFromBody.Credit;
            return newCourse;
        }
    }
}
