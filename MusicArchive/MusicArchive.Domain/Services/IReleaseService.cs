using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public interface IReleaseService {
    List<Release> GetReleases();
    public Release GetRelease(int id);
    void AddRelease(Release release);
}