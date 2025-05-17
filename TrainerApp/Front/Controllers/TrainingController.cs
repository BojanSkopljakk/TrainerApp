using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainerApp.Data;
using TrainerApp.Services;

namespace Front.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ITrainingService _trainingService;

        private readonly ApplicationDbContext _context;

        public TrainingController(ITrainingService trainingService, ApplicationDbContext context)
        {
            _trainingService = trainingService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Book()
        {
            var trainers = await _context.Trainers.ToListAsync();
            ViewBag.Trainers = trainers;
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
            return RedirectToAction(nameof(Book));
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

        [HttpGet]
        public IActionResult Calendar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Calendar(TrainerCalendarDto dto)
        {
            var sessions = await _trainingService.GetTrainerScheduleAsync(dto.AccessCode, dto.Date, dto.ViewType);
            ViewBag.Sessions = sessions;
            ViewBag.Date = dto.Date.ToShortDateString();
            ViewBag.Type = dto.ViewType;
            return View();
        }


    }
}
