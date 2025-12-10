using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public interface IArtistDao {
    List<Artist> GetArtists();
    Artist GetArtist(int id);
    void InsertArtist(Artist artist);
    void UpdateArtist(Artist artist);
    void DeleteArtist(int id);
}
