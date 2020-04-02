using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CW4.Models;

namespace CW4.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}
