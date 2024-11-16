using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHealthCare.DataAccess;
using SmartHealthCare.Models;

namespace SmartHealthCare.Controllers
{
    public class HealthMetricsController : Controller
    {
        private readonly DatabaseRepository _databaseRepository;

        public HealthMetricsController(DatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        // Adds health metrics for a patient
        [HttpPost]
        public async Task<IActionResult> AddHealthMetrics(HealthMetrics metrics)
        {
            if (ModelState.IsValid)
            {
                await _databaseRepository.AddHealthMetricsAsync(metrics);
                return RedirectToAction("MetricsList", new { patientId = metrics.PatientID });
            }
            return View(metrics);
        }

        // Displays health metrics for a patient
        public async Task<IActionResult> MetricsList(int patientId)
        {
            var metrics = await _databaseRepository.GetHealthMetricsByPatientIdAsync(patientId);
            return View(metrics);
        }
    }
}