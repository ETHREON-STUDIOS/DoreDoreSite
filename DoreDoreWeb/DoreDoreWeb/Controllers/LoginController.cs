using Microsoft.AspNetCore.Mvc;

namespace DoreDoreWeb.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult SingUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string FullName, string Email, string Password, string ConfirmPassword)
        {
            if (Password != ConfirmPassword)
            {
                ViewBag.Error = "Şifreler uyuşmuyor!";
                return View();
            }

            // Buraya kullanıcıyı veritabanına kaydetme kodları gelecek.
            // Şimdilik başarılı olduğunu varsayalım:

            return RedirectToAction("Login");


        }


        [HttpGet]
        public IActionResult Login() { 

        return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            // Burada giriş kontrolü yaparsın.
            if (Email == "admin@example.com" && Password == "123456")
            {
                // Giriş başarılıysa ana sayfaya yönlendir
                return RedirectToAction("Index", "Home");
            }

            // Hatalıysa tekrar login ekranı göster
            ViewBag.Error = "Geçersiz kullanıcı adı veya şifre!";
            return View();
        }

    }
}
