namespace TrainerApp.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; } // 30 ili 60
    }
}
