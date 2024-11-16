namespace SmartHealthCare.Models
{
    public class Prescription
    {
        public int PrescriptionID { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public string Medication { get; set; }
        public string Dosage { get; set; }
        public string Instructions { get; set; }
        public DateTime DateIssued { get; set; }

        // Navigation properties
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
