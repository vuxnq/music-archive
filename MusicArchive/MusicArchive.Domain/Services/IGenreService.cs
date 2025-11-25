using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public interface IGenreService {
    List<Genre> GetGenres();
    Genre GetGenre(int id);
    void AddGenre(Genre genre);
}