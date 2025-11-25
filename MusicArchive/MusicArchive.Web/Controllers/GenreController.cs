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

    public IActionResult Add() {
        return View(new GenreAddDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(GenreAddDto dto) {
        if (!ModelState.IsValid) {
            return View(dto);
        }

        var genre = new Genre {
            Name = dto.Name
        };

        genreService.AddGenre(genre);
        return RedirectToAction("Index");
    }

    public IActionResult Detail(int id) {
        var genre = genreService.GetGenre(id);

        return View(genre);
    }
}