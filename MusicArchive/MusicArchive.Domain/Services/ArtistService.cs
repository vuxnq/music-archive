using MusicArchive.Data;
using MusicArchive.Domain.Mappers;
using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public class ArtistService(IDataConnector connector) : IArtistService {
    public List<Artist> GetArtists() {
        return connector.CreateArtistDao().GetArtists().ToDomain();
    }

    public void AddArtist(Artist artist) {
        var data = artist.ToData();
        connector.CreateArtistDao().AddArtist(data);
        
        artist.Id = data.Id;
    }
}