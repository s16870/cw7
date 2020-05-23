using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw7.DTO.Requests;
using Cw7.DTO.Responses;
using Cw7.Models;

namespace Cw7.Services
{
    public interface IStudentDbService
    {
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
        PromoteStudentsResponse PromoteStudents(PromoteStudentsRequest request);
        Student GetStudent(String id);
        IEnumerable<Student> GetStudents();
        Boolean CheckStudent(String indexNumber);
    }
}
