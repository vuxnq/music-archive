using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public interface IReleaseDao {
    List<Release> GetReleases();
    Release GetRelease(int id);
    void InsertRelease(Release release);
    void UpdateRelease(Release release);
    void DeleteRelease(int id);
}