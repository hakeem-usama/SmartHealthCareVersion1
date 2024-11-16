namespace SmartHealthCare.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public int UserID { get; set; }
        public string MedicalHistory { get; set; }

        public User User { get; set; }
    }
}
