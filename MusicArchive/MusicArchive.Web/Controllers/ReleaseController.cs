using Microsoft.AspNetCore.Mvc;
using MusicArchive.Domain.Services;

namespace MusicArchive.Web.Controllers;

public class ReleaseController(
    IReleaseService releaseService
) : Controller {

    public IActionResult Index() {
        var releases = releaseService.GetReleases();
        return View(releases);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(MusicArchive.Domain.Models.Release release) {
        if (!ModelState.IsValid) {
            var releases = releaseService.GetReleases();
            return View(releases);
        }

        releaseService.AddRelease(release);
        return RedirectToAction("Index");
    }
}