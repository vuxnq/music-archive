using Microsoft.AspNetCore.Mvc;
using MusicArchive.Domain.Services;

namespace MusicArchive.Web.Controllers;

public class GenreController(
    IGenreService genreService
) : Controller {

    public IActionResult Index() {
        var genres = genreService.GetGenres();
        return View(genres);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(MusicArchive.Domain.Models.Genre genre) {
        if (!ModelState.IsValid) {
            var genres = genreService.GetGenres();
            return View(genres);
        }

        genreService.AddGenre(genre);
        return RedirectToAction("Index");
    }
}