using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw7.DTO.Requests
{
    public class EnrollStudentRequest
    {
        [Required] [MaxLength(100)] public string IndexNumber { get; set; }
        [Required] [MaxLength(100)] public string FirstName { get; set; }
        [Required] [MaxLength(100)] public string LastName { get; set; }
        [Required] public DateTime BirthDate { get; set; }
        [Required] [MaxLength(100)] public string Studies { get; set; }
    }
}
