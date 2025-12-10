using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicArchive.Domain.Services;
using MusicArchive.Domain.Models;
using MusicArchive.Web.Models;

namespace MusicArchive.Web.Controllers;

public class GenreController(
    IGenreService genreService
) : Controller {

    public IActionResult Index() {
        var genres = genreService.GetApprovedGenres();
        return View(genres);
    }

    public IActionResult Detail(int id) {
        var genre = genreService.GetGenre(id);
        return View(genre);
    }

    [Authorize]
    public IActionResult Add() {
        return View(new GenreAddDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Add(GenreAddDto dto) {
        if (!ModelState.IsValid) {
            return View(dto);
        }

        var genre = new Genre {
            Name = dto.Name,
            Description = dto.Description,
        };

        genreService.AddGenre(genre);
        TempData["SuccessMessage"] = "Genre submitted successfully.";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Moderator")]
    public IActionResult Approve(int id) {
        var genre = genreService.GetGenre(id);
        genreService.ApproveGenre(genre);
        TempData["SuccessMessage"] = "Genre approved successfully.";
        return RedirectToAction("Detail", new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Moderator")]
    public IActionResult Reject(int id) {
        var genre = genreService.GetGenre(id);
        genreService.RejectGenre(genre);
        TempData["SuccessMessage"] = "Genre rejected successfully.";
        return RedirectToAction("Index");
    }
}
