using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public interface IArtistService {
    List<Artist> GetArtists();
    Artist GetArtist(int id);
    void AddArtist(Artist artist);
}