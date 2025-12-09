using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public interface IReleaseService {
    List<Release> GetReleases();
    List<Release> GetUnapprovedReleases();
    List<Release> GetApprovedReleases();
    Release GetRelease(int id);
    void AddRelease(Release release);
}