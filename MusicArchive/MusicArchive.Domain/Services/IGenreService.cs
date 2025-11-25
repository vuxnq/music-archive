using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public interface IGenreService {
    public List<Genre> GetGenres();
    public Genre GetGenre(int id);
    public void AddGenre(Genre genre);
}