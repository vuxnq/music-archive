using MusicArchive.Data;
using MusicArchive.Domain.Mappers;
using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public class GenreService(IDataConnector connector) : IGenreService {
    public List<Genre> GetGenres() {
        return connector.CreateGenreDao().GetGenres().ToDomain();
    }

    public void AddGenre(Genre genre) {
        var data = genre.ToData();
        connector.CreateGenreDao().AddGenre(data);
        genre.Id = data.Id;
    }
}