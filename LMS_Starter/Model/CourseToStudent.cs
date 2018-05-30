using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Starter.Model
{
    public class CourseToStudent
    {
        public Course Course { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
