using Microsoft.AspNetCore.Mvc;
using DoreDoreWeb.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> EditProfile(User updatedUser, IFormFile ProfilePicture)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == updatedUser.Id);

                if (user != null)
                {
                    // Kullanıcı bilgilerini güncelle
                    user.UserName = updatedUser.UserName;
                    user.UserEposta = updatedUser.UserEposta;
                    user.BirthDate = updatedUser.BirthDate;
                    user.Gender = updatedUser.Gender;

                    // Profil fotoğrafı var mı kontrol et
                    if (ProfilePicture != null)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", ProfilePicture.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await ProfilePicture.CopyToAsync(stream);
                        }

                        // Veritabanına fotoğraf yolunu kaydet
                        user.ProfilePicture = "/images/" + ProfilePicture.FileName;
                    }

                    await _context.SaveChangesAsync();
                    
                    // Profil sayfasına yönlendir
                    return RedirectToAction(nameof(Profile));
                }
                else
                {
                    // Kullanıcı bulunamadı
                    return NotFound();
                }
            }

            // Geçersiz model durumu, formu tekrar render et
            return View(updatedUser);
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
