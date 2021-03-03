using System.ComponentModel.DataAnnotations;

namespace Pointwest.Test.Dtos
{
    public class ApplicantDto
    {   
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