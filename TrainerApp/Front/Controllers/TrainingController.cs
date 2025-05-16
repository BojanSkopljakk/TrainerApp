using Microsoft.AspNetCore.Mvc;
using TrainerApp.Data;
using TrainerApp.Services;

namespace Front.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpGet]
        public IActionResult Book()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Book(BookTrainingDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var success = await _trainingService.BookTrainingAsync(dto);
            if (!success)
            {
                ViewBag.Error = "The selected time slot is not available.";
                return View(dto);
            }

            ViewBag.Success = "Training booked successfully!";
            return View();
        }

        [HttpGet]
        public IActionResult Cancel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(CancelTrainingDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var success = await _trainingService.CancelTrainingAsync(dto);
            if (!success)
            {
                ViewBag.Error = "Could not cancel training.";
                return View(dto);
            }

            ViewBag.Success = "Training canceled successfully.";
            return View();
        }

    }
}
