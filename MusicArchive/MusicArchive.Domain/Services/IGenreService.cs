using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public interface IGenreService {
    List<Genre> GetGenres();
    List<Genre> GetUnapprovedGenres();
    List<Genre> GetApprovedGenres();
    Genre GetGenre(int id);
    void AddGenre(Genre genre);
    void ApproveGenre(Genre genre);
    void RejectGenre(Genre genre);
}