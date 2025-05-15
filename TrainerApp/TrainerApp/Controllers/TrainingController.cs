using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainerApp.Data;
using TrainerApp.Services;

namespace TrainerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpPost("book")]
        public async Task<IActionResult> BookTraining([FromBody] BookTrainingDto dto)
        {
            var success = await _trainingService.BookTrainingAsync(dto);
            if (!success)
                return BadRequest("The selected time slot is not available.");

            return Ok(new { message = "Training booked successfully." });
        }
    }
}
