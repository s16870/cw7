using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Cw7.DTO.Requests
{
    public class PromoteStudentsRequest
    {
        [Required] [MaxLength(100)] public string Studies { get; set; }
        [Required] public int Semester { get; set; }
    }
}
