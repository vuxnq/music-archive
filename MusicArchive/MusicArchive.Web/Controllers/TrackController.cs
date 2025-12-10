using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicArchive.Domain.Services;
using MusicArchive.Domain.Models;
using MusicArchive.Web.Models;

namespace MusicArchive.Web.Controllers;

public class TrackController(
    ITrackService trackService,
    IReleaseService releaseService
) : Controller {

    public IActionResult Index() {
        var tracks = trackService.GetApprovedTracks();
        return View(tracks);
    }

    public IActionResult Detail(int id) {
        var track = trackService.GetTrack(id);

        return View(track);
    }

    [Authorize]
    public IActionResult Add(int? releaseId = null) {
        var dto = new TrackAddDto {
            ReleaseId = releaseId ?? 0,
            ReleaseOptions = releaseService.GetApprovedReleases().Select(r => new SelectListItem(r.Title, r.Id.ToString())).ToList(),
        };
        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Add(TrackAddDto dto) {
        if (!ModelState.IsValid) {
            dto.ReleaseOptions = releaseService.GetApprovedReleases().Select(r => new SelectListItem(r.Title, r.Id.ToString())).ToList();
            return View(dto);
        }

        var track = new Track {
            Title = dto.Title,
            Description = dto.Description,
            Duration = dto.Duration,
            ReleaseId = dto.ReleaseId
        };

        trackService.AddTrack(track);
        TempData["SuccessMessage"] = "Track submitted successfully.";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Moderator")]
    public IActionResult Approve(int id) {
        var track = trackService.GetTrack(id);
        trackService.ApproveTrack(track);
        TempData["SuccessMessage"] = "Track approved successfully.";
        return RedirectToAction("Detail", new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Moderator")]
    public IActionResult Reject(int id) {
        var track = trackService.GetTrack(id);
        trackService.RejectTrack(track);
        TempData["SuccessMessage"] = "Track rejected successfully.";
        return RedirectToAction("Index");
    }
}
