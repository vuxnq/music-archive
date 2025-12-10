using MusicArchive.Data;
using MusicArchive.Domain.Mappers;
using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public class ReleaseService(IDataConnector connector) : IReleaseService {
    private readonly IReleaseDao _releaseDao = connector.CreateReleaseDao();
    private readonly IArtistDao _artistDao = connector.CreateArtistDao();
    private readonly IGenreDao _genreDao = connector.CreateGenreDao();
    private readonly ITrackDao _trackDao = connector.CreateTrackDao();

    public List<Release> GetReleases() {
        return _releaseDao.GetReleases().ToDomain();
    }

    public List<Release> GetUnapprovedReleases() {
        return GetReleases().Where(r => !r.IsApproved).ToList();
    }

    public List<Release> GetApprovedReleases() {
        return GetReleases().Where(r => r.IsApproved).ToList();
    }

    public Release GetRelease(int id) {
        var release = _releaseDao.GetRelease(id).ToDomain();

        release.Artist = _artistDao.GetArtist(release.ArtistsId).ToDomain();

        if (release.GenreId.HasValue) {
            release.Genre = _genreDao.GetGenre(release.GenreId.Value).ToDomain();
        }

        release.Tracks = _trackDao.GetTracks().Select(t => t.ToDomain()).Where(t => t.ReleaseId == release.Id).ToList();

        return release;
    }

    public void AddRelease(Release release) {
        connector.BeginTransaction();
        try {
            var data = release.ToData();
            _releaseDao.InsertRelease(data);
            release.Id = data.Id;

            foreach (var track in release.Tracks) {
                var tdata = track.ToData();
                tdata.ReleaseId = release.Id;
                _trackDao.InsertTrack(tdata);
                track.Id = tdata.Id;
            }

            connector.Commit();
        } catch {
            connector.Rollback();
            throw;
        }
    }

    public void ApproveRelease(Release release) {
        connector.BeginTransaction();
        try {
            release.IsApproved = true;
            var data = release.ToData();
            _releaseDao.UpdateRelease(data);

            foreach (var track in release.Tracks) {
                track.IsApproved = true;
                var tdata = track.ToData();
                _trackDao.UpdateTrack(tdata);
            }

            connector.Commit();
        } catch {
            connector.Rollback();
            throw;
        }
    }

    public void RejectRelease(Release release) {
        connector.BeginTransaction();
        try {
            foreach (var track in release.Tracks) {
                _trackDao.DeleteTrack(track.Id);
            }

            _releaseDao.DeleteRelease(release.Id);

            connector.Commit();
        } catch {
            connector.Rollback();
            throw;
        }
    }
}
