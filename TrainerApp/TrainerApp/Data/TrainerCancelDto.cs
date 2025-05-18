namespace TrainerApp.Data
{
    public class TrainerCancelDto
    {
        public string AccessCode { get; set; } = null!;
        public DateTime SessionStartTime { get; set; }
    }
}
