using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CW4.Models;

namespace CW4.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;
        static MockDbService()
        {
             _students = new List<Student>
            
                 {
                   new Student(1, "Jan", "Kowalski"),
                   new Student(2, "Anna", "Malewski"),
                   new Student(3, "Andrzej", "Andrzejewicz")

                };
        }

        public IEnumerable<Student> GetStudents()
        {
            //...sql con
            return _students;
        }
    }
}
