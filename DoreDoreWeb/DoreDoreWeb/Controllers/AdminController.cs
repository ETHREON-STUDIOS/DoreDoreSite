using Microsoft.AspNetCore.Mvc;
using DoreDoreWeb.Models;
using DoreDoreWeb.Models.ViewsModel;


namespace DoreDoreWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly DbfinalProjeContext _context;

        public AdminController(DbfinalProjeContext context)
        {
            _context = context;
        }

        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("UserLv") == "True";
        }

        // Kullanıcıları listeleme
        public IActionResult Users(int pageNumber = 1, int pageSize = 10)
        {
            if (!IsAdmin())
                return Unauthorized();

            // Toplam kullanıcı sayısını al
            var totalUsers = _context.Users.Count();
            var totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);

            // İlgili sayfadaki kullanıcıları al
            var users = _context.Users
                .OrderBy(u => u.UserName)  // İstediğiniz şekilde sıralayabilirsiniz
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Models.User'dan ViewsModel.User'a dönüştürme
            var viewModelUsers = users.Select(u => new Models.ViewsModel.Users
            {
                Id = u.Id,
                UserName = u.UserName,
                UserEposta = u.UserEposta,
                BirthDate = u.BirthDate,
                Gender = u.Gender
            }).ToList();

            // ViewModel ile sayfalama bilgilerini geçmek
            var viewModel = new UsersListViewModel
            {
                Users = viewModelUsers,  // ViewsModel.User tipi
                CurrentPage = pageNumber,
                TotalPages = totalPages
            };

            return View(viewModel);
        }




        // Kullanıcı düzenleme GET
        public IActionResult EditUser(int id)
        {
            if (!IsAdmin())
                return Unauthorized();

            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // Kullanıcı düzenleme POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(Models.User updatedUser, IFormFile profilePicture)
        {
            if (!IsAdmin())
                return Unauthorized();

            if (!ModelState.IsValid)
                return View(updatedUser);

            var user = await _context.Users.FindAsync(updatedUser.Id);
            if (user == null)
                return NotFound();

            user.UserName = updatedUser.UserName;
            user.UserEposta = updatedUser.UserEposta;
            user.UserPassword = updatedUser.UserPassword;
            user.UserLv = updatedUser.UserLv;
            user.BirthDate = updatedUser.BirthDate;
            user.Gender = updatedUser.Gender;

            // Eğer yeni bir profil fotoğrafı varsa, eski fotoğrafı değiştirelim
            if (profilePicture != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", profilePicture.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePicture.CopyToAsync(stream);
                }
                user.ProfilePicture = "/images/" + profilePicture.FileName;
            }

            // Profil fotoğrafı değiştirilmezse, eski fotoğraf yolunu koruyacak
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Users));
        }

        // Kullanıcı silme
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!IsAdmin())
                return Unauthorized();

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Users));
        }

        // Filmleri listeleme
        // Filmleri listeleme (Sayfalama ile)
        public IActionResult Films(int pageNumber = 1, int pageSize = 10)
        {
            if (!IsAdmin())
                return Unauthorized();

            // Toplam film sayısını al
            var totalFilms = _context.Films.Count();
            var totalPages = (int)Math.Ceiling(totalFilms / (double)pageSize);

            // İlgili sayfadaki filmleri al
            var films = _context.Films
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // ViewModel ile sayfalama bilgilerini geçmek
            var viewModel = new FilmListViewsModel
            {
                Films = films,
                CurrentPage = pageNumber,
                TotalPages = totalPages
            };

            return View(viewModel);
        }


        // Film düzenleme GET
        public IActionResult EditFilm(int id)
        {
            if (!IsAdmin())
                return Unauthorized();

            var film = _context.Films.FirstOrDefault(f => f.FilmId == id);
            if (film == null)
                return NotFound();

            return View(film);
        }

        // Film düzenleme POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFilm(Film updatedFilm, IFormFile filmImage, IFormFile filmDosya)
        {
            if (!IsAdmin())
                return Unauthorized();

            if (!ModelState.IsValid)
                return View(updatedFilm);

            var film = await _context.Films.FindAsync(updatedFilm.FilmId);
            if (film == null)
                return NotFound();

            film.FilmName = updatedFilm.FilmName;
            film.ReleaseDate = updatedFilm.ReleaseDate;
            film.FilmImdb = updatedFilm.FilmImdb;
            film.FilmDescriptiyon = updatedFilm.FilmDescriptiyon; // düzeltildi
            film.FilmDuratiyon = updatedFilm.FilmDuratiyon; // düzeltildi

            if (filmImage != null)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "filmimages", filmImage.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await filmImage.CopyToAsync(stream);
                }
                film.FilmImageDosyaYolu = "/filmimages/" + filmImage.FileName;
            }

            if (filmDosya != null)
            {
                var videoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "filmdosyalar", filmDosya.FileName);
                using (var stream = new FileStream(videoPath, FileMode.Create))
                {
                    await filmDosya.CopyToAsync(stream);
                }
                film.FilmDosyaYolu = "/filmdosyalar/" + filmDosya.FileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Films));
        }

        // Film silme
        public async Task<IActionResult> DeleteFilm(int id)
        {
            if (!IsAdmin())
                return Unauthorized();

            var film = await _context.Films.FindAsync(id);
            if (film == null)
                return NotFound();

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Films));
        }
        // Film ekleme sayfası
        public IActionResult AddFilm()
        {
            if (!IsAdmin())
                return Unauthorized();

            return View();
        }

        // Film ekleme POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFilm(Film film, IFormFile filmImage, IFormFile filmDosya)
        {
            if (!IsAdmin())
                return Unauthorized();

            if (!ModelState.IsValid)
                return View(film);

            if (filmImage != null)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "filmimages", filmImage.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await filmImage.CopyToAsync(stream);
                }
                film.FilmImageDosyaYolu = "/filmimages/" + filmImage.FileName;
            }

            if (filmDosya != null)
            {
                var videoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "filmdosyalar", filmDosya.FileName);
                using (var stream = new FileStream(videoPath, FileMode.Create))
                {
                    await filmDosya.CopyToAsync(stream);
                }
                film.FilmDosyaYolu = "/filmdosyalar/" + filmDosya.FileName;
            }

            await _context.Films.AddAsync(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Films));
        }
    }
}
