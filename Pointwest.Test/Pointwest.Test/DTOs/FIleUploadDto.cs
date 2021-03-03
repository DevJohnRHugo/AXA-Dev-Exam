namespace Pointwest.Test.DTOs
{
    public class FIleUploadDto
    {
        public File File { get; set; }
    }

    public class File
    {
        public string Mime { get; set; }
        public string Data { get; set; }
    }
}