namespace SmartHealthCare.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }
        public int UserID { get; set; }
        public string Specialization { get; set; }
        public string Schedule { get; set; }

        public User User { get; set; }
    }
}
