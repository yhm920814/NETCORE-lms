using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Starter.Model
{
    public class Student
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public int Fee { get; set; }
        public int CreditLimit { get; set; }
        public int CurrentCredit { get; set; }
        public ICollection<CourseToStudent> CourseToStudents { get; set; }
        public static Student CreateStudent(Student studentFromBody)
        {
            var newStudent = new Student();
            newStudent.Name = studentFromBody.Name;
            newStudent.ImageUrl = studentFromBody.ImageUrl;
            newStudent.Email = studentFromBody.Email;
            newStudent.Fee = studentFromBody.Fee;
            newStudent.CreditLimit = studentFromBody.CreditLimit;
            newStudent.CurrentCredit = studentFromBody.CurrentCredit;
            return newStudent;
        }
    }
}
