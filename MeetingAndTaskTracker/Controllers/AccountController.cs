using System.Diagnostics;
using MeetingAndTaskTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetingAndTaskTracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly MeetTrackDbContext _Context;

        public AccountController(MeetTrackDbContext context)
        {
            _Context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var usr = _Context.Users.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();
            if (usr != null)
            {
                HttpContext.Session.SetInt32("UserId", usr.Id);
                HttpContext.Session.SetString("UserName", usr.UserName);
                HttpContext.Session.SetString("Role", usr.Role);
                return RedirectToAction(nameof(Dashboard), "Account");
            }
            else
            {
                ViewBag.Error = "Invalid Username or Password";
                return View();
            }
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
