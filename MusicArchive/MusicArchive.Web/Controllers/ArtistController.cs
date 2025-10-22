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
}