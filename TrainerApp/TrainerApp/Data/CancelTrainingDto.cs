namespace TrainerApp.Data
{
    public class CancelTrainingDto
    {
        public string PhoneNumber { get; set; } = null!;
        public DateTime SessionStartTime { get; set; }
    }
}
