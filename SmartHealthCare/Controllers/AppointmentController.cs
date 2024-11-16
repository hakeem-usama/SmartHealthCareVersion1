
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHealthCare.DataAccess;
using SmartHealthCare.Models;

namespace SmartHealthCare.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly DatabaseRepository _databaseRepository;

        public AppointmentController(DatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        // Displays appointment booking form
        public IActionResult BookAppointment()
        {
            return View();
        }

        // Books a new appointment
        [HttpPost]
        public async Task<IActionResult> BookAppointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                await _databaseRepository.BookAppointmentAsync(appointment);
                return RedirectToAction("AppointmentsList");
            }
            return View(appointment);
        }

        // Approves an appointment
        public async Task<IActionResult> ApproveAppointment(int appointmentId)
        {
            await _databaseRepository.ApproveAppointmentAsync(appointmentId);
            return RedirectToAction("AppointmentsList");
        }

        // Cancels an appointment
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            await _databaseRepository.CancelAppointmentAsync(appointmentId);
            return RedirectToAction("AppointmentsList");
        }

        // Displays a list of appointments
        public async Task<IActionResult> AppointmentsList()
        {
            var appointments = await _databaseRepository.GetAllAppointmentsAsync();
            return View(appointments);
        }
    }
}