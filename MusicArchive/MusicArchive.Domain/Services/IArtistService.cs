using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public interface IArtistService {
    List<Artist> GetArtists();
    List<Artist> GetUnapprovedArtists();
    List<Artist> GetApprovedArtists();
    Artist GetArtist(int id);
    void AddArtist(Artist artist);
    void ApproveArtist(Artist artist);
    void RejectArtist(Artist artist);
}