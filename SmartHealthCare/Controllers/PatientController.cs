
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHealthCare.DataAccess;
using SmartHealthCare.Models;

namespace SmartHealthCare.Controllers
{
    public class PatientController : Controller
    {
        private readonly DatabaseRepository _databaseRepository;

        public PatientController(DatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        // Adds a new patient
        [HttpPost]
        public async Task<IActionResult> AddPatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                await _databaseRepository.AddPatientAsync(patient);
                return RedirectToAction("PatientList");
            }
            return View(patient);
        }

        // Updates medical history of a patient
        [HttpPost]
        public async Task<IActionResult> UpdateMedicalHistory(int patientId, string medicalHistory)
        {
            await _databaseRepository.UpdatePatientMedicalHistoryAsync(patientId, medicalHistory);
            return RedirectToAction("PatientList");
        }

        // Displays list of patients
        public async Task<IActionResult> PatientList()
        {
            var patients = await _databaseRepository.GetAllPatientsAsync();
            return View(patients);
        }
    }
}