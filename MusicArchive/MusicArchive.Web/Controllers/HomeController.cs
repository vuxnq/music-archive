using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MusicArchive.Domain.Services;
using MusicArchive.Web.Models;

namespace MusicArchive.Web.Controllers;

public class HomeController(
    IArtistService artistService,
    IReleaseService releaseService,
    IGenreService genreService
) : Controller {

    public IActionResult Index() {
        return View(new HomeIndexDto {
            Artists = artistService.GetApprovedArtists().ToArray().Reverse().Take(5).ToList(),
            Releases = releaseService.GetApprovedReleases().ToArray().Reverse().Take(5).ToList(),
            Genres = genreService.GetApprovedGenres()
        });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
