using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS_Starter.Model;
using Microsoft.EntityFrameworkCore;


namespace LMS_Starter.Service
{
    public class LMSDataHandler:ILMSDataHandler
    {
        private LMSDBContext _ctx;
        public LMSDataHandler(LMSDBContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<Student> GetAllStudents()
        {
            return _ctx.Students
                        .Include(student => student.CourseToStudents)
                        .ThenInclude(CTS => CTS.Course)
                        .OrderBy(student => student.Id).ToList();
        }
        public Student GetStudentById(int id)
        {
            return _ctx.Students
                    .Include(student => student.CourseToStudents)
                    .ThenInclude(CTS => CTS.Course)
                    .FirstOrDefault(s => s.Id == id);
        }
        public int GetStudentCredit(int id)
        {
            var student = _ctx.Students.Find(id);
            if(student != null)
            {
                return student.CurrentCredit;
            }
            return -1;
        }
        public void AddStudent(Student student)
        {
            _ctx.Add(student);
            _save();
        }
        public void EditStudent(Student student,int id)
        {
            var studentToEdit = _ctx.Students.Find(id);
            studentToEdit.Name = student.Name != null ? student.Name : studentToEdit.Name;
            studentToEdit.ImageUrl = student.ImageUrl != null ? student.ImageUrl : studentToEdit.ImageUrl;
            studentToEdit.Email = student.Email != null ? student.Email : studentToEdit.Email;
            studentToEdit.CreditLimit = student.CreditLimit > 0 ? student.CreditLimit : studentToEdit.CreditLimit;
            _save();
        }
        public void DeleteStudent(int id)
        {
            Student studentToDelete = _ctx.Students.Find(id);
            if(studentToDelete != null)
            {
                _ctx.Students.Remove(studentToDelete);
            }
            _save();
        }
        public IEnumerable<Course> GetAllCourses()
        {
            return _ctx.Courses
                        .Include(c => c.CourseToLecturers)
                        .ThenInclude(CTL => CTL.Lecturer)
                        .Include(c => c.CourseToStudents)
                        .ThenInclude(CTS => CTS.Student)
                        .OrderBy(a => a.Id).ToList();
        }
        public Course GetCourseById(int id)
        {
            return _ctx.Courses
                        .Include(c => c.CourseToLecturers)
                        .ThenInclude(CTL => CTL.Lecturer)
                        .Include(c => c.CourseToStudents)
                        .ThenInclude(CTS => CTS.Student)
                        .FirstOrDefault(a => a.Id == id);
        }
        public void AddCourse(Course course)
        {
            _ctx.Add(course);
            _save();
        }
        public void EditCourse(Course course,int id)
        {
            var courseToEdit = _ctx.Courses.Find(id);
            courseToEdit.Name = course.Name != null ? course.Name : courseToEdit.Name;
            courseToEdit.Desc = course.Desc != null ? course.Desc : courseToEdit.Desc;
            courseToEdit.ImageUrl = course.ImageUrl != null ? course.ImageUrl : courseToEdit.ImageUrl;
            courseToEdit.Credit = course.Credit > 0 ? course.Credit : courseToEdit.Credit;
            courseToEdit.MaxStudents = course.MaxStudents > 0 ? course.MaxStudents : courseToEdit.MaxStudents;
            _save();
        }
        public void DeleteCourse(int id)
        {
            var courseToDelete = _ctx.Courses.Find(id);
            if (courseToDelete != null)
            {
                _ctx.Remove(courseToDelete);
            }
            _save();
        }
        public IEnumerable<Lecturer> GetAllLecturers()
        {
            return _ctx.Lecturers
                    .Include(l => l.CourseToLecturers)
                    .ThenInclude(CTL => CTL.Course)
                    .OrderBy(lecturer => lecturer.Id).ToList();
        }
        public Lecturer GetLecturerById(int id)
        {
            return _ctx.Lecturers
                    .Include(lecturer => lecturer.CourseToLecturers)
                        .ThenInclude(CTL => CTL.Course)
                        .FirstOrDefault(l => l.Id == id);
        }
        public void AddLecturer(Lecturer lecturer)
        {
            _ctx.Lecturers.Add(lecturer);
            _save();
        }
        public void EditLecturer(Lecturer lecturer,int id)
        {
            Lecturer lecturerToEdit = _ctx.Lecturers.Find(id);
            lecturerToEdit.Name = lecturer.Name != null ? lecturer.Name : lecturerToEdit.Name;
            lecturerToEdit.Desc = lecturer.Desc != null ? lecturer.Desc : lecturerToEdit.Desc;
            lecturerToEdit.Email = lecturer.Email != null ? lecturer.Email : lecturerToEdit.Email;
            lecturerToEdit.ImageUrl = lecturer.ImageUrl != null ? lecturer.ImageUrl : lecturerToEdit.ImageUrl;
            _save();
        }
        public void DeleteLecturer(int id)
        {
            var lecturerToDelete = _ctx.Lecturers.Find(id);
            if (lecturerToDelete != null)
            {
                _ctx.Lecturers.Remove(lecturerToDelete);
            }
            _save();
        }
        public bool IsEmailValid(string email)
        {
            var isValid = true;
            _ctx.Users.ToList().ForEach((User arg) => {
                if (arg.Email == email)
                {
                    isValid = false;
                }
            });
            return isValid;
        }
        public bool Register(User user)
        {
            if (IsEmailValid(user.Email))
            {
                _ctx.Users.Add(user);
                _save();
                return true;
            }
            else
            {
                return false;
            }
        }
        public string Login(string email,string password)
        {
            var user = _ctx.Users.Find(email);
            string token = null;
            if (user != null)
            {
                if (user.IsPasswordMatch(password))
                {
                    token = user.GenerateToken();
                    _save();
                }
            }
            return token;
        }
        public bool IsUserVerified(string email)
        {
            var user = _ctx.Users.Find(email);
            if (user != null)
            {
                if (user.IsVerified == true)
                {
                    return true;
                }
            }
            return false;
        }
        public string UpdateVerificationCodeAndReturnPhoneNo(string email,int code)
        {
            var user = _ctx.Users.Find(email);
            user.VerificationCode = code;
            _save();
            return user.Phone;
        }
        public string LoginWithSMS(string email, string code)
        {
            var user = _ctx.Users.Find(email);
            string token = null;
            if (user != null)
            {
                if (user.IsCodeMatch(code))
                {
                    token = user.GenerateToken();
                    _save();
                }
            }
            return token;
        }
        public bool IsLogin(string token)
        {
            bool isLogin = false;
            _ctx.Users.ToList().ForEach((User arg) => {
                if (arg.Token == token)
                {
                    isLogin = true;
                }
            });

            return isLogin;
        }
        public bool Verify(string email,string verificationCode)
        {
            bool isVerified = false;
            var user = _ctx.Users.Find(email);
            if (user != null && user.VerificationCode.ToString() == verificationCode)
            {
                isVerified = true;
                user.IsVerified = true;
                _save();
            }
            return isVerified;
        }
        public void EnrolStudentToCourse(int courseId,int studentId)
        {
            var newEnrol = new CourseToStudent();
            newEnrol.CourseId = courseId;
            newEnrol.StudentId = studentId;
            var student = _ctx.Students.Find(studentId);
            var course = _ctx.Courses.Find(courseId);
            course.CurrentStudents += 1;
            student.CurrentCredit += course.Credit;
            student.Fee += 100;
            _ctx.CourseToStudents.Add(newEnrol);
            _save();
        }
        public void EnrolLecturerToCourse(int courseId,int lecturerId)
        {
            var newEnrol = new CourseToLecturer();
            newEnrol.CourseId = courseId;
            newEnrol.LecturerId = lecturerId;
            _ctx.CourseToLecturers.Add(newEnrol);
            _save();
        }
        public void WithdrawStudentFromCourse(int courseId,int studentId)
        {
            var enrolmentToDelete = _ctx.CourseToStudents.Find(courseId, studentId);
            var student = _ctx.Students.Find(studentId);
            var course = _ctx.Courses.Find(courseId);
            if (enrolmentToDelete != null)
            {
                course.CurrentStudents -= 1;
                student.CurrentCredit -= course.Credit;
                student.Fee -= 100;
                _ctx.CourseToStudents.Remove(enrolmentToDelete);
            }
            _save();
        }
        public void WithdrawLecturerFromCourse(int courseId,int lecturerId)
        {
            var enrolmentToDelete = _ctx.CourseToLecturers.Find(courseId, lecturerId);
            if (enrolmentToDelete != null)
            {
                _ctx.CourseToLecturers.Remove(enrolmentToDelete);
            }
            _save();
        }


        private bool _save()
        {
            return (_ctx.SaveChanges() >= 0);
        }
    }
}
