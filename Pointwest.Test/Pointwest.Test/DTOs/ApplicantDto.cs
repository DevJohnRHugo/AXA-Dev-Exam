using System.ComponentModel.DataAnnotations;

namespace Pointwest.Test.DTOs
{
    public class ApplicantDto
    {
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