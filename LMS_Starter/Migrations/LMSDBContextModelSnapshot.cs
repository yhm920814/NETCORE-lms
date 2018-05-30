﻿// <auto-generated />
using LMS_Starter.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LMS_Starter.Migrations
{
    [DbContext(typeof(LMSDBContext))]
    partial class LMSDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LMS_Starter.Model.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Credit");

                    b.Property<int>("CurrentStudents");

                    b.Property<string>("Desc");

                    b.Property<string>("ImageUrl");

                    b.Property<int>("MaxStudents");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("LMS_Starter.Model.CourseToLecturer", b =>
                {
                    b.Property<int>("CourseId");

                    b.Property<int>("LecturerId");

                    b.HasKey("CourseId", "LecturerId");

                    b.HasIndex("LecturerId");

                    b.ToTable("CourseToLecturers");
                });

            modelBuilder.Entity("LMS_Starter.Model.CourseToStudent", b =>
                {
                    b.Property<int>("CourseId");

                    b.Property<int>("StudentId");

                    b.HasKey("CourseId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("CourseToStudents");
                });

            modelBuilder.Entity("LMS_Starter.Model.Lecturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Desc");

                    b.Property<string>("Email");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("LMS_Starter.Model.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreditLimit");

                    b.Property<int>("CurrentCredit");

                    b.Property<string>("Email");

                    b.Property<int>("Fee");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("LMS_Starter.Model.User", b =>
                {
                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsVerified");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.Property<string>("Token");

                    b.Property<int>("VerificationCode");

                    b.HasKey("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LMS_Starter.Model.CourseToLecturer", b =>
                {
                    b.HasOne("LMS_Starter.Model.Course", "Course")
                        .WithMany("CourseToLecturers")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMS_Starter.Model.Lecturer", "Lecturer")
                        .WithMany("CourseToLecturers")
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMS_Starter.Model.CourseToStudent", b =>
                {
                    b.HasOne("LMS_Starter.Model.Course", "Course")
                        .WithMany("CourseToStudents")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMS_Starter.Model.Student", "Student")
                        .WithMany("CourseToStudents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
