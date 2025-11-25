using Microsoft.AspNetCore.Mvc;
using MusicArchive.Domain.Services;
using MusicArchive.Domain.Models;

namespace MusicArchive.Web.Controllers;

public class TrackController(
    ITrackService trackService
) : Controller {

    public IActionResult Index() {
        var tracks = trackService.GetTracks();
        return View(tracks);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(Track track) {
        if (!ModelState.IsValid) {
            var tracks = trackService.GetTracks();
            return View(tracks);
        }

        trackService.AddTrack(track);
        return RedirectToAction("Index");
    }

    public IActionResult Detail(int id) {
        var track = trackService.GetTrack(id);

        return View(track);
    }
}
