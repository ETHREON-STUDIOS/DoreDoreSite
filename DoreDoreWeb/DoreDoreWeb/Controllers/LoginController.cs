using DoreDoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoreDoreWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly DbfinalProjeContext _dbfinalProjeContext;

        public LoginController(DbfinalProjeContext dbfinalProjeContext)
        {
            _dbfinalProjeContext = dbfinalProjeContext;
        }

        [HttpGet]
        public IActionResult SingUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SingUp(User user, IFormFile ProfilePicture)
        {
            if (ProfilePicture != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", ProfilePicture.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfilePicture.CopyToAsync(stream);
                }
                user.ProfilePicture = "/images/" + ProfilePicture.FileName; // Veritabanına fotoğraf yolu kaydediliyor
            }

            await _dbfinalProjeContext.AddAsync(user);
            await _dbfinalProjeContext.SaveChangesAsync();

            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            // Giriş kontrolü yapılıyor
            var user = _dbfinalProjeContext.Users.FirstOrDefault(u => u.UserEposta == Email && u.UserPassword == Password);
            if (user != null)
            {
                // Kullanıcı adı ve profil fotoğrafını session'a kaydediyoruz
                HttpContext.Session.SetString("UserName", user.UserName ?? string.Empty);
                HttpContext.Session.SetString("ProfilePicture", user.ProfilePicture ?? string.Empty);
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserLv", user.UserLv.ToString() ?? string.Empty);


                // Giriş başarılıysa ana sayfaya yönlendir
                return RedirectToAction("Index", "Home"); // Ana sayfa veya istediğiniz sayfaya yönlendirin
            }

     
            ViewBag.Error = "Geçersiz kullanıcı adı veya şifre!";
            return View();  // Aynı sayfada kalıp hata mesajını gösteriyor
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Tüm session'ı temizler
            return RedirectToAction("Index", "Home");
        }
    }
}
