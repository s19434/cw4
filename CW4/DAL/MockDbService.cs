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
            // _students = new List<Student>
            //
            //     {
            //        new Student{IdStudent=1, FirstName="ANN", LastName="SMITH"},
            //        new Student{IdStudent=2, FirstName="DEN", LastName="PETERSEN"},
            //        new Student{IdStudent=3, FirstName="PETER", LastName="PARKER"},
            //
            //    };
        }

        public IEnumerable<Student> GetStudents()
        {
            //...sql con
            return _students;
        }
    }
}
