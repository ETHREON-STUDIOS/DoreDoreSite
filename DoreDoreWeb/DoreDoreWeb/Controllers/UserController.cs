using Microsoft.AspNetCore.Mvc;
using DoreDoreWeb.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace DoreDoreWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly DbfinalProjeContext _context;

        public UserController(DbfinalProjeContext context)
        {
            _context = context;
        }

        // Profil sayfasını görüntüleme
        public IActionResult Profile()
        {
            var result = GetUserIdFromSession(); // Kullanıcı ID'sini oturumdan alıyoruz
            if (result != null) return result; // Eğer giriş yapılmamışsa, yönlendir

            var userId = HttpContext.Session.GetInt32("UserId").Value;
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Profil düzenleme sayfasına yönlendirme
        public IActionResult EditProfile()
        {
            var result = GetUserIdFromSession(); // Kullanıcı ID'sini oturumdan alıyoruz
            if (result != null) return result; // Eğer giriş yapılmamışsa, yönlendir

            var userId = HttpContext.Session.GetInt32("UserId").Value;
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }   

            return View(user);
        }

        // Profil düzenlemesi işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProfile(User updatedUser)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id);

                if (user.Id != null)
                {
                    // Kullanıcıyı güncelle
                    user.UserName = updatedUser.UserName;
                    user.UserEposta = updatedUser.UserEposta;
                    user.BirthDate = updatedUser.BirthDate;
                    user.Gender = updatedUser.Gender;
                    user.ProfilePicture = updatedUser.ProfilePicture;

                    // Değişiklikleri kaydet
                    _context.SaveChanges();

                    // Profil sayfasına yönlendir
                    return RedirectToAction(nameof(Profile));
                }
                else
                {
                    // Kullanıcı bulunamadı, hata mesajı
                    return NotFound();
                }
            }

            return View(updatedUser); // Geçerli olmayan model durumu için tekrar formu render et
        }

        // Kullanıcının ID'sini Session'dan almayı sağlayacak metot
        private IActionResult GetUserIdFromSession()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                // Kullanıcı giriş yapmamışsa, login sayfasına yönlendirme yapalım
                return RedirectToAction("Login", "Login"); // Login Controller'ınızda Login metoduna yönlendirme yapın
            }

            return null; // Giriş yapmışsa, herhangi bir yönlendirme yapma
        }
    }
}
