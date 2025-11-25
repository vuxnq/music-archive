using Microsoft.AspNetCore.Mvc;
using MusicArchive.Domain.Services;
using MusicArchive.Domain.Models;
using MusicArchive.Web.Models;

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
    public IActionResult Index(Genre genre) {
        if (!ModelState.IsValid) {
            var genres = genreService.GetGenres();
            return View(genres);
        }

        genreService.AddGenre(genre);
        return RedirectToAction("Index");
    }

    public IActionResult Detail(int id) {
        var genre = genreService.GetGenre(id);

        return View(genre);
    }
}