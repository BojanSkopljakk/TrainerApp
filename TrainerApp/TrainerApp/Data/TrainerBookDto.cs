namespace TrainerApp.Data
{
    public class TrainerBookDto
    {
        public string AccessCode { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; }
    }
}
