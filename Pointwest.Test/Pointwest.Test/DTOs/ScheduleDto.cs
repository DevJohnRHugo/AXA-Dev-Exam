using System.ComponentModel.DataAnnotations;

namespace Pointwest.Test.Dtos
{
    public class ScheduleDto
    {
        [Required]
        public string ProposedDate { get; set; }

        [Required]
        public string ProposedTime { get; set; }
    }
}