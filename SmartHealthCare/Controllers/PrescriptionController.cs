using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHealthCare.DataAccess;
using SmartHealthCare.Models;

namespace SmartHealthCare.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly DatabaseRepository _databaseRepository;

        public PrescriptionController(DatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        // Creates a new prescription
        [HttpPost]
        public async Task<IActionResult> CreatePrescription(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                await _databaseRepository.CreatePrescriptionAsync(prescription);
                return RedirectToAction("PrescriptionsList", new { patientId = prescription.PatientID });
            }
            return View(prescription);
        }

        // Displays list of prescriptions for a patient
        public async Task<IActionResult> PrescriptionsList(int patientId)
        {
            var prescriptions = await _databaseRepository.GetPrescriptionsByPatientIdAsync(patientId);
            return View(prescriptions);
        }
    }
}