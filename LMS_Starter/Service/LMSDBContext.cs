using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS_Starter.Model;

namespace LMS_Starter.Service
{
    public class LMSDBContext:DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CourseToLecturer> CourseToLecturers { get; set; }
        public DbSet<CourseToStudent> CourseToStudents { get; set; }
        public LMSDBContext(DbContextOptions<LMSDBContext> options) :base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Student>().HasKey(a => a.Id);
            modelBuilder.Entity<Course>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Course>().HasKey(a => a.Id);
            modelBuilder.Entity<Lecturer>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Lecturer>().HasKey(a => a.Id);
            modelBuilder.Entity<User>().HasKey(a => a.Email);
            modelBuilder.Entity<CourseToStudent>().HasKey(a => new { a.CourseId,a.StudentId});
            modelBuilder.Entity<CourseToStudent>()
                .HasOne(CTS => CTS.Course)
                .WithMany(course => course.CourseToStudents)
                .HasForeignKey(CTS => CTS.CourseId);
            modelBuilder.Entity<CourseToStudent>()
                .HasOne(CTS => CTS.Student)
                .WithMany(s => s.CourseToStudents)
                .HasForeignKey(CTS => CTS.StudentId);
            modelBuilder.Entity<CourseToLecturer>().HasKey(a => new { a.CourseId, a.LecturerId });
            modelBuilder.Entity<CourseToLecturer>()
                .HasOne(CTL => CTL.Course)
                .WithMany(c => c.CourseToLecturers)
                .HasForeignKey(CTL => CTL.CourseId);
            modelBuilder.Entity<CourseToLecturer>()
                .HasOne(CTL => CTL.Lecturer)
                .WithMany(l => l.CourseToLecturers)
                .HasForeignKey(CTL => CTL.LecturerId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // DB setting is set here 
            var connectionString = "Server=tcp:lms-sep.database.windows.net,1433;Initial Catalog=lms-sep-groupa;Persist Security Info=False;User ID=dbadmin;Password=iLOVEACIC88;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
