using Microsoft.AspNetCore.Mvc;
using DoreDoreWeb.Models;
using System.Linq;

namespace DoreDoreWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly DbfinalProjeContext _context;

        public AdminController(DbfinalProjeContext context)
        {
            _context = context;
        }

        // Kullanıcıları listeleme
        public IActionResult Users()
        {
            if (HttpContext.Session.GetString("UserLv") != "True")
            {
                return Unauthorized(); // Admin değilse izin verme
            }
            var users = _context.Users.ToList();
            return View(users);
        }

        // Kullanıcı düzenleme GET
        public IActionResult EditUser(int id)
        {
            if (HttpContext.Session.GetString("UserLv") != "True")
            {
                return Unauthorized(); // Admin değilse izin verme
            }
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Kullanıcı düzenleme POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(User updatedUser)
        {
            if (HttpContext.Session.GetString("UserLv")!="True")
            {
                return Unauthorized(); // Admin değilse izin verme
            }



            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id);
                if (user != null)
                {
                    user.UserName = updatedUser.UserName;
                    user.UserEposta = updatedUser.UserEposta;
                    user.UserPassword = updatedUser.UserPassword;
                    user.UserLv = updatedUser.UserLv;
                    user.BirthDate = updatedUser.BirthDate;
                    user.Gender = updatedUser.Gender;
                    user.ProfilePicture = updatedUser.ProfilePicture;

                    _context.SaveChanges();
                    return RedirectToAction(nameof(Users));
                }
                return NotFound();
            }
            return View(updatedUser);
        }

        // Kullanıcı silme
        public IActionResult DeleteUser(int id)
        {
            if (HttpContext.Session.GetString("UserLv") != "True")
            {
                return Unauthorized(); // Admin değilse izin verme
            }
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Users));
        }
    }
}
