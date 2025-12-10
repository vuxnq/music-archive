using MusicArchive.Data;
using MusicArchive.Domain.Mappers;
using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public class ArtistService(IDataConnector connector) : IArtistService {
    private readonly IArtistDao _artistDao = connector.CreateArtistDao();
    private readonly IReleaseDao _releaseDao = connector.CreateReleaseDao();

    public List<Artist> GetArtists() {
        return _artistDao.GetArtists().ToDomain();
    }

    public List<Artist> GetUnapprovedArtists() {
        return GetArtists().Where(a => !a.IsApproved).ToList();
    }

    public List<Artist> GetApprovedArtists() {
        return GetArtists().Where(a => a.IsApproved).ToList();
    }

    public Artist GetArtist(int id) {
        var artist = _artistDao.GetArtist(id).ToDomain();

        artist.Releases = _releaseDao.GetReleases().ToDomain()
            .Where(r => r.ArtistsId == id)
            .ToList();

        return artist;
    }

    public void AddArtist(Artist artist) {
        connector.BeginTransaction();
        try {
            var data = artist.ToData();
            _artistDao.InsertArtist(data);
            artist.Id = data.Id;

            connector.Commit();
        } catch {
            connector.Rollback();
            throw;
        }
    }

    public void ApproveArtist(Artist artist) {
        connector.BeginTransaction();
        try {
            artist.IsApproved = true;
            var data = artist.ToData();
            _artistDao.UpdateArtist(data);

            connector.Commit();
        } catch {
            connector.Rollback();
            throw;
        }
    }

    public void RejectArtist(Artist artist) {
        connector.BeginTransaction();
        try {
            _artistDao.DeleteArtist(artist.Id);
            connector.Commit();
        } catch {
            connector.Rollback();
            throw;
        }
    }
}
