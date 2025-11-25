using Microsoft.AspNetCore.Mvc;
using MusicArchive.Domain.Services;
using MusicArchive.Domain.Models;
using MusicArchive.Web.Models;

namespace MusicArchive.Web.Controllers;

public class TrackController(
    ITrackService trackService
) : Controller {

    public IActionResult Index() {
        var tracks = trackService.GetTracks();
        return View(tracks);
    }

    public IActionResult Add() {
        return View(new TrackAddDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(TrackAddDto dto) {
        if (!ModelState.IsValid) {
            return View(dto);
        }

        var track = new Track {
            Title = dto.Title,
            Duration = dto.Duration,
            ReleaseId = dto.ReleaseId
        };

        trackService.AddTrack(track);
        return RedirectToAction("Index");
    }

    public IActionResult Detail(int id) {
        var track = trackService.GetTrack(id);

        return View(track);
    }
}
