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

        private readonly IEmailService _emailService;

        public TrainingController(
            ITrainingService trainingService,
            ApplicationDbContext context,
            IEmailService emailService) 
        {
            _trainingService = trainingService;
            _context = context;
            _emailService = emailService;
        }


        [HttpGet]
        public async Task<IActionResult> Book()
        {
            var trainers = await _context.Trainers.ToListAsync();
            ViewBag.Trainers = trainers;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Book(IFormCollection form)
        {
            string fullName = form["FullName"];
            string phoneNumber = form["PhoneNumber"];
            string trainerIdStr = form["TrainerId"];
            string datePart = form["DatePart"];
            string timePart = form["TimePart"];
            string durationStr = form["DurationMinutes"];

            if (!int.TryParse(trainerIdStr, out int trainerId) ||
                !int.TryParse(durationStr, out int duration) ||
                !DateTime.TryParse($"{datePart} {timePart}", out DateTime startTime))
            {
                ViewBag.Error = "Invalid input. Please check the form values.";
                return View();
            }

            var dto = new BookTrainingDto
            {
                FullName = fullName,
                PhoneNumber = phoneNumber,
                TrainerId = trainerId,
                StartTime = startTime,
                DurationMinutes = duration
            };

            var success = await _trainingService.BookTrainingAsync(dto);
            if (!success)
            {
                ViewBag.Error = "Invalid time or slot unavailable. Start time must be on the hour or half past.";
                return View();
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
        public async Task<IActionResult> Cancel(IFormCollection form)
        {
            string phoneNumber = form["PhoneNumber"];
            string datePart = form["DatePart"];
            string timePart = form["TimePart"];

            if (!DateTime.TryParse($"{datePart} {timePart}", out DateTime startTime))
            {
                ViewBag.Error = "Invalid date or time selected.";
                return View();
            }

            var dto = new CancelTrainingDto
            {
                PhoneNumber = phoneNumber,
                SessionStartTime = startTime
            };

            var success = await _trainingService.CancelTrainingAsync(dto);
            if (!success)
            {
                ViewBag.Error = "Could not cancel training. It may be too close to the session or not found.";
                return View();
            }

            ViewBag.Success = "Training canceled successfully.";
            return RedirectToAction(nameof(Cancel));
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
        [HttpGet]
        public IActionResult TrainerBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TrainerBook(IFormCollection form)
        {
            string accessCode = form["AccessCode"];
            string fullName = form["FullName"];
            string phoneNumber = form["PhoneNumber"];
            string datePart = form["DatePart"];
            string timePart = form["TimePart"];
            string durationStr = form["DurationMinutes"];

            if (!int.TryParse(durationStr, out int duration) ||
                !DateTime.TryParse($"{datePart} {timePart}", out DateTime startTime))
            {
                ViewBag.Error = "Invalid input. Please check the form values.";
                return View();
            }

            var dto = new TrainerBookDto
            {
                AccessCode = accessCode,
                FullName = fullName,
                PhoneNumber = phoneNumber,
                StartTime = startTime,
                DurationMinutes = duration
            };

            var success = await _trainingService.BookByTrainerAsync(dto);
            if (!success)
            {
                ViewBag.Error = "The time slot is not available or the trainer access code is invalid.";
                return View();
            }

            ViewBag.Success = "Session booked successfully!";
            return RedirectToAction(nameof(TrainerBook));
        }
        [HttpGet]
        public IActionResult TrainerCancel()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TrainerCancel(string accessCode)
        {
            var trainer = await _context.Trainers.FirstOrDefaultAsync(t => t.AccessCode == accessCode);
            if (trainer == null)
            {
                ViewBag.Error = "Invalid access code.";
                return View();
            }

            var upcoming = await _context.TrainingSessions
                .Include(s => s.User)
                .Include(s => s.Trainer)
                .Where(s => s.TrainerId == trainer.Id && s.StartTime >= DateTime.UtcNow)
                .OrderBy(s => s.StartTime)
                .ToListAsync();

            ViewBag.Sessions = upcoming;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmCancel(string accessCode, DateTime sessionStartTime)
        {
            var dto = new TrainerCancelDto
            {
                AccessCode = accessCode,
                SessionStartTime = sessionStartTime
            };

            var success = await _trainingService.CancelByTrainerAsync(dto);
            if (!success)
            {
                ViewBag.Error = "Failed to cancel session.";
            }
            else
            {
                ViewBag.Success = "Session canceled successfully.";
            }

            return await TrainerCancel(accessCode); 
        }



    }
}
