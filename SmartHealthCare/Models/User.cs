using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SmartHealthCare.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        [ValidateNever]
        public Role Role { get; set; }

    }
}
