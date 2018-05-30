using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS_Starter.Model;

namespace LMS_Starter.Service
{
    public interface ILMSDataHandler
    {
        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(int id);
        int GetStudentCredit(int id);
        void AddStudent(Student student);
        void EditStudent(Student student, int id);
        void DeleteStudent(int id);
        IEnumerable<Course> GetAllCourses();
        Course GetCourseById(int id);
        void AddCourse(Course course);
        void EditCourse(Course course,int id);
        void DeleteCourse(int id);
        IEnumerable<Lecturer> GetAllLecturers();
        Lecturer GetLecturerById(int id);
        void AddLecturer(Lecturer lecturer);
        void EditLecturer(Lecturer lecturer,int id);
        void DeleteLecturer(int id);
        bool IsEmailValid(string email);
        bool Register(User user);
        string Login(string email,string password);
        bool IsLogin(string token);
        bool IsUserVerified(string email);
        string UpdateVerificationCodeAndReturnPhoneNo(string email, int code);
        string LoginWithSMS(string email, string code);
        bool Verify(string email, string verificationCode);
        void EnrolStudentToCourse(int courseId,int studentId);
        void EnrolLecturerToCourse(int courseId, int lecturerId);
        void WithdrawStudentFromCourse(int courseId, int studentId);
        void WithdrawLecturerFromCourse(int courseId, int lecturerId);

    }
}
