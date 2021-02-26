using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Test.Model
{
    public class Schedule
    {
        public string ProposedDate { get; set; }
        public string ProposedTime { get; set; }
        public bool Online { get; } = true;
    }
}
