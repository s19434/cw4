using System;
using CW4.DAL;
using CW4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CW4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private const string connectionString = "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=s19434;Integrated Security=True";

        [HttpGet]
        public IActionResult GetStudent()
        {
            List<Student> students = new List<Student>();
            using (var connection = new SqlConnection(connectionString))
            using (var commands = new SqlCommand())
            {
                commands.Connection = connection;
                commands.CommandText = "Select * From Student stu inner join Enrollment enr on stu.IdEnrollment = enr.IdEnrollment " +
                                       "inner join Studies s on s.IdStudy = enr.IdStudy;";

                connection.Open();
                var dr = commands.ExecuteReader();


                while (dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.IdEnrollment = Int32.Parse(dr["IdEnrollment"].ToString());

                    students.Add(st);
                }
            }

            return Ok(students);
        }


        [HttpGet("{index}")]
        public IActionResult GetStudent(String index)
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            using (var connection = new SqlConnection(connectionString))
            using (var commands = new SqlCommand())
            {
                commands.Connection = connection;
                commands.CommandText = $"Select * From Enrollment e inner join Student s on e.IdEnrollment = s.IdEnrollment where s.IndexNumber = @index;";
                commands.Parameters.AddWithValue("index", index);

                connection.Open();
                var dr = commands.ExecuteReader();

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
