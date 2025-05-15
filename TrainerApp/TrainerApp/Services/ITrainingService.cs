using TrainerApp.Data;

namespace TrainerApp.Services
{
    public interface ITrainingService
    {
        Task<bool> BookTrainingAsync(BookTrainingDto dto);
    }
}
