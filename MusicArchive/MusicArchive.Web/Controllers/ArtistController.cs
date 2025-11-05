using Microsoft.AspNetCore.Mvc;
using MusicArchive.Domain.Services;

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
    public IActionResult Index(MusicArchive.Domain.Models.Artist artist) {
        if (!ModelState.IsValid) {
            var artists = artistService.GetArtists();
            return View(artists);
        }

        artistService.AddArtist(artist);
        return RedirectToAction("Index");
    }
}