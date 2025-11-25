using Microsoft.AspNetCore.Mvc;
using MusicArchive.Domain.Services;
using MusicArchive.Domain.Models;
using MusicArchive.Web.Models;

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
    public IActionResult Index(Release release) {
        if (!ModelState.IsValid) {
            var releases = releaseService.GetReleases();
            return View(releases);
        }

        releaseService.AddRelease(release);
        return RedirectToAction("Index");
    }

    public IActionResult Detail(int id) {
        var release = releaseService.GetRelease(id);

        return View(release);
    }
}