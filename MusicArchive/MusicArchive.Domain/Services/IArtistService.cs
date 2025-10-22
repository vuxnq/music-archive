using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public interface IArtistService {
    public List<Artist> GetArtists();
}