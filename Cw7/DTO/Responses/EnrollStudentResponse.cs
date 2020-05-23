using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw7.DTO.Responses
{
    public class EnrollStudentResponse
    {
        public string IndexNumber { get; set; }
        public int Semester { get; set; }
        public string Studies { get; set; }
        public DateTime StartDate { get; set; }
        public int IdEnrollment { get; set; }
        public int IdStudies { get; set; }

    }
}
