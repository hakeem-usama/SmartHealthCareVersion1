using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHealthCare.DataAccess;
using SmartHealthCare.Models;

namespace SmartHealthCare.Controllers
{
    public class NotificationController : Controller
    {
        private readonly DatabaseRepository _databaseRepository;

        public NotificationController(DatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        // Sends a new notification
        [HttpPost]
        public async Task<IActionResult> SendNotification(Notification notification)
        {
            if (ModelState.IsValid)
            {
                await _databaseRepository.SendNotificationAsync(notification);
                return RedirectToAction("NotificationList", new { userId = notification.UserID });
            }
            return View(notification);
        }

        // Displays list of notifications for a user
        public async Task<IActionResult> NotificationList(int userId)
        {
            var notifications = await _databaseRepository.GetNotificationsForUserAsync(userId);
            return View(notifications);
        }
    }
}