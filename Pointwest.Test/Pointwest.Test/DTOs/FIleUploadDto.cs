using System.ComponentModel.DataAnnotations;

namespace Pointwest.Test.Dtos
{
    public class FileUploadDto
    {
        [Required]
        public FileDto File { get; set; }
    }

    public class FileDto
    {
        public string Mime { get; set; }
        public string Data { get; set; }
    }
}