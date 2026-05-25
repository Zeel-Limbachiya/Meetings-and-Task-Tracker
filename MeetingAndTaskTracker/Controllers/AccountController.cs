using System.Diagnostics;
using MeetingAndTaskTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Register()
        {
            List<SelectListItem> roles =
            [
                new SelectListItem {Value="Admin", Text="Admin"},
                new SelectListItem {Value="Employee", Text="Employee"}
            ];
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                _Context.Users.Add(user);
                await _Context.SaveChangesAsync();
                TempData["Register_Success"] = "User Registered Successfully..";
                return RedirectToAction(nameof(Login));
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
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
