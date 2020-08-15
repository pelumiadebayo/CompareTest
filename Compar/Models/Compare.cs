using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Compar.Models
{
    public class Compare
    {
        [Required]
        public string FirstStudentName { get; set; }
        [Required]
        public string FirstStudentFile { get; set; }
        [Required]
        public string SecondStudentName { get; set; }
        [Required]
        public string SecondStudentFile { get; set; }
    }
}
