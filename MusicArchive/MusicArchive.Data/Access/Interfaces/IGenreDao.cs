using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public interface IGenreDao {
    List<Genre> GetGenres();
    Genre GetGenre(int id);
    void AddGenre(Genre genre);
}