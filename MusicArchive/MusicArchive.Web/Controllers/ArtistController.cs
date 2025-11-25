using Microsoft.AspNetCore.Mvc;
using MusicArchive.Domain.Services;
using MusicArchive.Domain.Models;
using MusicArchive.Web.Models;

namespace MusicArchive.Web.Controllers;

public class ArtistController(
    IArtistService artistService
) : Controller {

    public IActionResult Index() {
        var artists = artistService.GetArtists();
        return View(artists);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(Artist artist) {
        if (!ModelState.IsValid) {
            var artists = artistService.GetArtists();
            return View(artists);
        }

        artistService.AddArtist(artist);
        return RedirectToAction("Index");
    }

    public IActionResult Detail(int id) {
        var artist = artistService.GetArtist(id);

        return View(artist);
    }
}