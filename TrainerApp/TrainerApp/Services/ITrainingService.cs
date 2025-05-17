using TrainerApp.Data;
using TrainerApp.Models;

namespace TrainerApp.Services
{
    public interface ITrainingService
    {
        Task<bool> BookTrainingAsync(BookTrainingDto dto);
        Task<bool> CancelTrainingAsync(CancelTrainingDto dto);
        Task<List<TrainingSession>> GetTrainerScheduleAsync(string accessCode, DateTime date, string viewType);

    }
}
