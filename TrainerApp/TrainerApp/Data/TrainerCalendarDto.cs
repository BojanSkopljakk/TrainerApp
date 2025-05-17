namespace TrainerApp.Data
{
    public class TrainerCalendarDto
    {
        public string AccessCode { get; set; } = null!;
        public DateTime Date { get; set; }
        public string ViewType { get; set; } = "daily";
    }
}
