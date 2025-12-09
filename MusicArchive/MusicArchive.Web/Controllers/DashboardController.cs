using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicArchive.Domain.Services;
using MusicArchive.Web.Models;

namespace MusicArchive.Web.Controllers;

public class DashboardController(
    IArtistService artistService,
    IReleaseService releaseService,
    ITrackService trackService,
    IGenreService genreService
    ) : Controller {
    
    [Authorize(Roles = "Moderator")]
    public IActionResult Index() {
        return View(new DashboardIndexDto {
            Artists = artistService.GetUnapprovedArtists(),
            Releases = releaseService.GetUnapprovedReleases(),
            Genres = genreService.GetUnapprovedGenres(),
            Tracks = trackService.GetUnapprovedTracks(),
        });
    }
}