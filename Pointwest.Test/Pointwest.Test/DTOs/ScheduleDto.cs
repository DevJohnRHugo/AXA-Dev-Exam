namespace Pointwest.Test.DTOs
{
    public class ScheduleDto
    {
        public string ProposedDate { get; set; }
        public string ProposedTime { get; set; }
        public bool Online { get; } = true;
    }
}