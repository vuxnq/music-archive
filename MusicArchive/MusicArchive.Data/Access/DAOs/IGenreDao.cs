using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public interface IGenreDao {
    List<Genre> GetGenres();
    Genre GetGenre(int id);
    void InsertGenre(Genre genre);
    void UpdateGenre(Genre genre);
    void DeleteGenre(int id);
}