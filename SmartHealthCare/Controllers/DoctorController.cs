
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHealthCare.DataAccess;
using SmartHealthCare.Models;

namespace SmartHealthCare.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DatabaseRepository _databaseRepository;

        public DoctorController(DatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        // Adds a new doctor
        [HttpPost]
        public async Task<IActionResult> AddDoctor(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                await _databaseRepository.AddDoctorAsync(doctor);
                return RedirectToAction("DoctorList");
            }
            return View(doctor);
        }

        // Updates the schedule of a doctor
        [HttpPost]
        public async Task<IActionResult> UpdateSchedule(int doctorId, string schedule)
        {
            await _databaseRepository.UpdateDoctorScheduleAsync(doctorId, schedule);
            return RedirectToAction("DoctorList");
        }

        // Displays list of doctors
        public async Task<IActionResult> DoctorList()
        {
            var doctors = await _databaseRepository.GetAllDoctorsAsync();
            return View(doctors);
        }
    }
}