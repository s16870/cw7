using Cw7.Models;
using System.Collections.Generic;

namespace Cw7.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}
