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
        var artist = artistService.GetArtist(id);

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
            BeginDate = dto.BeginDate,
            EndDate = dto.EndDate,
            Location = dto.Location
        };

        artistService.AddArtist(artist);
        return RedirectToAction("Index");
    }
}