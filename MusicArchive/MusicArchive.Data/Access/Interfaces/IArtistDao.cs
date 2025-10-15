using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public interface IArtistDao {
    List<Artist> GetArtists();
    Artist GetArtist(int id);
}