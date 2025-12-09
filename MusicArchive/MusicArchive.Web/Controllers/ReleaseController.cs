using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicArchive.Domain.Services;
using MusicArchive.Domain.Models;
using MusicArchive.Web.Models;

namespace MusicArchive.Web.Controllers;

public class ReleaseController(
    IReleaseService releaseService
) : Controller {

    public IActionResult Index() {
        var releases = releaseService.GetApprovedReleases();
        return View(releases);
    }

    [Authorize]
    public IActionResult Add() {
        return View(new ReleaseAddDto { ReleaseDate = DateTime.Now.Date });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Add(ReleaseAddDto dto) {
        if (!ModelState.IsValid) {
            return View(dto);
        }

        var release = new Release {
            Title = dto.Title,
            Description = dto.Description,
            ReleaseDate = dto.ReleaseDate,
            ArtistsId = dto.ArtistsId,
            GenreId = dto.GenreId,
            Tracks = dto.Tracks?.Select(t => new Track {
                Title = t.Title,
                Duration = t.Duration
            }).ToList() ?? new List<Track>()
        };

        releaseService.AddRelease(release);

        return RedirectToAction("Index");
    }

    public IActionResult Detail(int id) {
        var release = releaseService.GetRelease(id);

        return View(release);
    }
}