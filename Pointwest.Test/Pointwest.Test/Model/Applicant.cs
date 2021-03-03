using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Test.Model
{
    public class Applicant
    {     
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string Mobile { get; set; }

        [Required]
        public string PositionApplied { get; set; }

        [Required]
        public string Source { get; set; }
    }
}
