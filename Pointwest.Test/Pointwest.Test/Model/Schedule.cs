using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Test.Model
{
    public class Schedule
    {
        [Required]
        public string ProposedDate { get; set; }

        [Required]
        public string ProposedTime { get; set; }
        public bool Online { get; } = true;
    }
}
