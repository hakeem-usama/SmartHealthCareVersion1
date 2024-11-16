namespace SmartHealthCare.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime NotificationDate { get; set; }
        public string Status { get; set; } // 'Sent', 'Pending'

        // Navigation property to the User
        public User User { get; set; }
    }
}
