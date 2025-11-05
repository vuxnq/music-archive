using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public interface IReleaseService {
    List<Release> GetReleases();
    void AddRelease(Release release);
}