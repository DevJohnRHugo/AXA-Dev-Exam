using System.ComponentModel.DataAnnotations;

namespace Pointwest.Test.Model
{
    public class Application2
    {
        [Key]
        public int Id { get; set; }

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

        public File File { get; set; }

        public string ProposedDate { get; set; }

        public string ProposedTime { get; set; }

        public bool Online { get; } = true;
    }


    //  public class File
    // {
    //     public string Mime { get; set; }

    //     public string Data { get; set; }
    // }
}