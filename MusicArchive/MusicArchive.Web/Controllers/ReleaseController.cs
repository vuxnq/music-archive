using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicArchive.Domain.Services;
using MusicArchive.Domain.Models;
using MusicArchive.Web.Models;

namespace MusicArchive.Web.Controllers;

public class ReleaseController(
    IReleaseService releaseService,
    IArtistService artistService,
    IGenreService genreService
) : Controller {

    public IActionResult Index() {
        var releases = releaseService.GetApprovedReleases();
        return View(releases);
    }

    public IActionResult Detail(int id) {
        var release = releaseService.GetRelease(id, User.IsInRole("Moderator"));

        return View(release);
    }

    [Authorize]
    public IActionResult Add(int? artistId = null) {
        var dto = new ReleaseAddDto {
            ReleaseDate = DateTime.Now.Date,
            ArtistId = artistId ?? 0,
            ArtistOptions = artistService.GetApprovedArtists().Select(a => new SelectListItem(a.Name, a.Id.ToString())).ToList(),
            GenreOptions = genreService.GetApprovedGenres().Select(g => new SelectListItem(g.Name, g.Id.ToString())).ToList(),
        };
        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Add(ReleaseAddDto dto) {
        if (!ModelState.IsValid) {
            dto.ArtistOptions = artistService.GetApprovedArtists().Select(a => new SelectListItem(a.Name, a.Id.ToString())).ToList();
            dto.GenreOptions = genreService.GetApprovedGenres().Select(g => new SelectListItem(g.Name, g.Id.ToString())).ToList();
            return View(dto);
        }

        var release = new Release {
            Title = dto.Title,
            Description = dto.Description,
            ReleaseDate = dto.ReleaseDate,
            ArtistsId = dto.ArtistId,
            GenreId = dto.GenreId,
            Tracks = dto.Tracks?.Select(t => new Track {
                Title = t.Title,
                Duration = t.Duration
            }).ToList() ?? []
        };

        releaseService.AddRelease(release);
        TempData["SuccessMessage"] = "Release submitted successfully.";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Moderator")]
    public IActionResult Approve(int id) {
        var release = releaseService.GetRelease(id, User.IsInRole("Moderator"));
        releaseService.ApproveRelease(release);
        TempData["SuccessMessage"] = "Release approved successfully.";
        return RedirectToAction("Detail", new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Moderator")]
    public IActionResult Reject(int id) {
        var release = releaseService.GetRelease(id, User.IsInRole("Moderator"));
        releaseService.RejectRelease(release);
        TempData["SuccessMessage"] = "Release rejected successfully.";
        return RedirectToAction("Index");
    }
}
