using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Test.Model
{
    public class Applicant
    {
        //[Key]
        //public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        [Required]
        public string Email { get; set; }

        [StringLength(15)]
        [MinLength(11)]
        [Required]
        public string Mobile { get; set; }

        [StringLength(50)]
        [Required]
        public string PositionApplied { get; set; }

        [StringLength(50)]
        [Required]
        public string Source { get; set; }
    }
}
