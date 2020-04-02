using System;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;
using CW4.DAL;
using CW4.Models;

namespace CW4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private const string ConString = "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=s19434;User ID=apbds19434;Password=admin";

        [HttpGet]
        public IActionResult GetStudent()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "Select * From Student stu inner join Enrollment enr on stu.IdEnrollment = enr.IdEnrollment " +
                                       "inner join Studies s on s.IdStudy = enr.IdStudy;";

                con.Open();
                var dr = com.ExecuteReader();

                while (dr.Read())
                {
                    var student = new Enrollment();
                    student.IndexNumber = dr["IndexNumber"].ToString();
                    student.FirstName = dr["FirstName"].ToString();
                    student.LastName = dr["LastName"].ToString();
                    student.BirthDate = dr["BirthDate"].ToString();
                    student.IdEnrollment = Int32.Parse(dr["IdEnrollment"].ToString());

                    students.Add(student);
                }
            }

            return Ok(students);
        }
        [HttpGet("{index}")]
        public IActionResult GetStudent(String index)
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = $"Select * From Enrollment e inner join Student s on e.IdEnrollment = s.IdEnrollment where s.IndexNumber = @index;";
                com.Parameters.AddWithValue("index", index);

                con.Open();
                var dr = com.ExecuteReader();

                while (dr.Read())
                {
                    var enrollment = new Enrollment();
                    enrollment.IdEnrollment = Int32.Parse(dr["IdEnrollment"].ToString());
                    enrollment.Semester = Int32.Parse(dr["Semester"].ToString());
                    enrollment.IdStudy = Int32.Parse(dr["IdStudy"].ToString());
                    enrollment.StartDate = dr["StartDate"].ToString();

                    enrollments.Add(enrollment);
                }
            }

            return Ok(enrollments);
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("200: Usuwanie ukonczone");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id)
        {
            return Ok("200: Aktualizacja dokonczona");
        }
    }
}