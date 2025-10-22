using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public interface IGenreService {
    public List<Genre> GetGenres();
}