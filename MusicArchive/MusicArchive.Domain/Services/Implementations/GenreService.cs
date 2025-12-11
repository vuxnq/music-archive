using MusicArchive.Data;
using MusicArchive.Domain.Mappers;
using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public class GenreService(IDataConnector connector) : IGenreService {
    private readonly IReleaseDao _releaseDao = connector.CreateReleaseDao();
    private readonly IGenreDao _genreDao = connector.CreateGenreDao();

    public List<Genre> GetGenres() {
        return _genreDao.GetGenres().ToDomain();
    }

    public List<Genre> GetUnapprovedGenres() {
        return GetGenres().Where(g => !g.IsApproved).ToList();
    }

    public List<Genre> GetApprovedGenres() {
        return GetGenres().Where(g => g.IsApproved).ToList();
    }

    public Genre GetGenre(int id, bool includeUnapproved = false) {
        var genre = _genreDao.GetGenre(id).ToDomain();

        genre.Releases = _releaseDao.GetReleases().ToDomain()
            .Where(r => r.GenreId == id && (includeUnapproved || r.IsApproved))
            .ToList();

        return genre;
    }

    public void AddGenre(Genre genre) {
        connector.BeginTransaction();
        try {
            var data = genre.ToData();
            _genreDao.InsertGenre(data);
            genre.Id = data.Id;

            connector.Commit();
        } catch {
            connector.Rollback();
            throw;
        }
    }

    public void ApproveGenre(Genre genre) {
        connector.BeginTransaction();
        try {
            genre.IsApproved = true;
            var data = genre.ToData();
            _genreDao.UpdateGenre(data);

            connector.Commit();
        } catch {
            connector.Rollback();
            throw;
        }
    }

    public void RejectGenre(Genre genre) {
        connector.BeginTransaction();
        try {
            _genreDao.DeleteGenre(genre.Id);
            connector.Commit();
        } catch {
            connector.Rollback();
            throw;
        }
    }
}
