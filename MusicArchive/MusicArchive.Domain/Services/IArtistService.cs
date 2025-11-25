using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public interface IArtistService {
    public List<Artist> GetArtists();
    public Artist GetArtist(int id);
    public void AddArtist(Artist artist);
}