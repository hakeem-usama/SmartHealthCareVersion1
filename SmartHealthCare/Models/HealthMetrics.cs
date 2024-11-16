namespace SmartHealthCare.Models
{
    public class HealthMetrics
    {
        public int MetricID { get; set; }
        public int PatientID { get; set; }
        public int HeartRate { get; set; }
        public string BloodPressure { get; set; }
        public decimal Temperature { get; set; }
        public DateTime RecordedDate { get; set; }

        // Navigation property to the Patient
        public Patient Patient { get; set; }
    }
}
