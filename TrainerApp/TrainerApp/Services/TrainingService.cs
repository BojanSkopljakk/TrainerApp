using Microsoft.EntityFrameworkCore;
using TrainerApp.Data;
using TrainerApp.Models;

namespace TrainerApp.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ApplicationDbContext _context;

        public TrainingService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> BookTrainingAsync(BookTrainingDto dto)
        {
            // Provera da li je termin slobodan
            var overlap = await _context.TrainingSessions.AnyAsync(t =>
                t.TrainerId == dto.TrainerId &&
                t.StartTime < dto.StartTime.AddMinutes(dto.DurationMinutes) &&
                t.StartTime.AddMinutes(t.DurationMinutes) > dto.StartTime);

            if (overlap) return false;

            // Kreiraj novog korisnika u bazi ukoliko nije poznat broj 
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == dto.PhoneNumber);
            if (user == null)
            {
                user = new User
                {
                    FullName = dto.FullName,
                    PhoneNumber = dto.PhoneNumber
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            // Kreiraj novu sesiju 
            var session = new TrainingSession
            {
                TrainerId = dto.TrainerId,
                UserId = user.Id,
                StartTime = dto.StartTime,
                DurationMinutes = dto.DurationMinutes
            };

            _context.TrainingSessions.Add(session);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CancelTrainingAsync(CancelTrainingDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == dto.PhoneNumber);
            if (user == null)
                return false;

            var session = await _context.TrainingSessions
                .FirstOrDefaultAsync(s => s.UserId == user.Id && s.StartTime == dto.SessionStartTime);

            if (session == null)
                return false;

            var cancelLimitHours = 24; 
            var now = DateTime.UtcNow;

            if ((session.StartTime - now).TotalHours < cancelLimitHours)
                return false; // Previse je kasno za otkazivanje

            _context.TrainingSessions.Remove(session);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
