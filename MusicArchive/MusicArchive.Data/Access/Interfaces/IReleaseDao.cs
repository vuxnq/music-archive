using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public interface IReleaseDao {
    List<Release> GetReleases();
    Release GetRelease(int id);
}