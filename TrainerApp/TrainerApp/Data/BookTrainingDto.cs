namespace TrainerApp.Data
{
    public class BookTrainingDto
    {
        public string PhoneNumber { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public int TrainerId { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; } // 30 ili 60
    }
}
