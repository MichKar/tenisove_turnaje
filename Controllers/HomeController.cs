using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TenisoveTurnaje.Models;

namespace TenisoveTurnaje.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private UserManager<User> userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager) {
            _logger = logger;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index() {
            User user = await userManager.GetUserAsync(HttpContext.User);
            string message = $"Ahoj {user.UserName}! Vítej na tomto webu pro tenisové turnaje.";
            return View("Index", message);
        }

        public IActionResult Info() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
