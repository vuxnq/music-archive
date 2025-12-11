using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicArchive.Domain.Services;
using MusicArchive.Domain.Models;
using MusicArchive.Web.Models;

namespace MusicArchive.Web.Controllers;

public class ArtistController(
    IArtistService artistService
) : Controller {

    public IActionResult Index() {
        var artists = artistService.GetApprovedArtists();
        return View(artists);
    }

    public IActionResult Detail(int id) {
        var artist = artistService.GetArtist(id, User.IsInRole("Moderator"));
        return View(artist);
    }

    [Authorize]
    public IActionResult Add() {
        return View(new ArtistAddDto { BeginDate = DateTime.Now.Date });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Add(ArtistAddDto dto) {
        if (!ModelState.IsValid) {
            return View(dto);
        }

        var artist = new Artist {
            Name = dto.Name,
            Description = dto.Description,
            BeginDate = dto.BeginDate,
            EndDate = dto.EndDate,
            Location = dto.Location
        };

        artistService.AddArtist(artist);
        TempData["SuccessMessage"] = "Artist submitted successfully.";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Moderator")]
    public IActionResult Approve(int id) {
        var artist = artistService.GetArtist(id, User.IsInRole("Moderator"));
        artistService.ApproveArtist(artist);
        TempData["SuccessMessage"] = "Artist approved successfully.";
        return RedirectToAction("Detail", new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Moderator")]
    public IActionResult Reject(int id) {
        var artist = artistService.GetArtist(id, User.IsInRole("Moderator"));
        artistService.RejectArtist(artist);
        TempData["SuccessMessage"] = "Artist rejected successfully.";
        return RedirectToAction("Index");
    }
}
