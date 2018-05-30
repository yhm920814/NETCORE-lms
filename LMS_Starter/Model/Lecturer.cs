using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Starter.Model
{
    public class Lecturer
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public string Desc { get; set; }
        public ICollection<CourseToLecturer> CourseToLecturers { get; set; }
        public static Lecturer CreateLecturerFromBody(Lecturer lecturer)
        {
            var newLecturer = new Lecturer();
            newLecturer.Name = lecturer.Name;
            newLecturer.Desc = lecturer.Desc;
            newLecturer.Email = lecturer.Email;
            newLecturer.ImageUrl = lecturer.ImageUrl;
            return newLecturer;
        }
    }
}
