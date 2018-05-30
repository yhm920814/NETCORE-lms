using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Starter.Model
{
    public class CourseToLecturer
    {
        public Course Course { get; set; }
        public Lecturer Lecturer { get; set; }
        public int CourseId { get; set; }
        public int LecturerId { get; set; }
    }
}
