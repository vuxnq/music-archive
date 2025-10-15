using System.Text.Json;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class ArtistTextDao : IArtistDao {
    private string filePath;

    public ArtistTextDao(string filePath) {
        this.filePath = filePath;
    }
    
    public List<Artist> GetArtists() {
        var result = JsonSerializer.Deserialize<List<Artist>>(File.ReadAllText(filePath));
        if (result == null) throw new FileNotFoundException($"file {filePath} not found");
        return result;
    }

    public Artist GetArtist(int id) {
        var result = GetArtists().FirstOrDefault(a => a.Id == id);
        if (result == null) throw new KeyNotFoundException($"artist {id} not found");
        return result;
    }
}