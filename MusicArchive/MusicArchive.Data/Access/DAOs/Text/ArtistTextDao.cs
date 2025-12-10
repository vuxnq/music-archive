using System.Text.Json;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class ArtistTextDao : IArtistDao {
    private readonly string filePath;

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

    public void InsertArtist(Artist artist) {
        var list = GetArtists();
        var nextId = list.Any() ? list.Max(a => a.Id) + 1 : 0;
        artist.Id = nextId;
        list.Add(artist);
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }

    public void UpdateArtist(Artist artist) {
        var list = GetArtists();
        var idx = list.FindIndex(a => a.Id == artist.Id);
        if (idx == -1) throw new KeyNotFoundException($"artist {artist.Id} not found");
        list[idx] = artist;
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }

    public void DeleteArtist(int id) {
        var list = GetArtists();
        var idx = list.FindIndex(a => a.Id == id);
        if (idx == -1) throw new KeyNotFoundException($"artist {id} not found");
        list.RemoveAt(idx);
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }
}