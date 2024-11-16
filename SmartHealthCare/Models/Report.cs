namespace SmartHealthCare.Models
{
    public class Report
    {
        public int ReportID { get; set; }
        public string ReportType { get; set; }
        public int CreatedBy { get; set; }
        public string Content { get; set; }
        public DateTime GeneratedDate { get; set; }

        // Navigation property to the User who created the report
        public User Creator { get; set; }
    }
}
