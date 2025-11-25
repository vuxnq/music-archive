using Microsoft.AspNetCore.Mvc;
using MusicArchive.Domain.Services;
using MusicArchive.Domain.Models;
using MusicArchive.Web.Models;

namespace MusicArchive.Web.Controllers;

public class ReleaseController(
    IReleaseService releaseService,
    ITrackService trackService
) : Controller {

    public IActionResult Index() {
        var releases = releaseService.GetReleases();
        return View(releases);
    }

    public IActionResult Add() {
        return View(new ReleaseAddDto { ReleaseDate = DateTime.Now.Date });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(ReleaseAddDto dto) {
        if (!ModelState.IsValid) {
            return View(dto);
        }

        var release = new Release {
            Title = dto.Title,
            Description = dto.Description,
            ReleaseDate = dto.ReleaseDate,
            ArtistsId = dto.ArtistsId,
            GenreId = dto.GenreId
        };
        releaseService.AddRelease(release);

        foreach (var t in dto.Tracks) {
            var track = new Track {
                Title = t.Title,
                Duration = t.Duration,
                ReleaseId = release.Id
            };
            trackService.AddTrack(track);
        }

        return RedirectToAction("Index");
    }

    public IActionResult Detail(int id) {
        var release = releaseService.GetRelease(id);

        return View(release);
    }
}