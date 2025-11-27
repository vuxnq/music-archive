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

    public Genre GetGenre(int id) {
        var genre = _genreDao.GetGenre(id).ToDomain();
        
        genre.Releases = _releaseDao.GetReleases().ToDomain()
            .Where(r => r.GenreId == id)
            .ToList();

        return genre;
    }

    public void AddGenre(Genre genre) {
        connector.BeginTransaction();
        try {
            var data = genre.ToData();
            _genreDao.AddGenre(data);
            genre.Id = data.Id;

            connector.Commit();
        } catch {
            connector.Rollback();
            throw;
        }
    }
}