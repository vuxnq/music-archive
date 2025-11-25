using System.Text.Json;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class TrackTextDao : ITrackDao {
    private string filePath;

    public TrackTextDao(string filePath) {
        this.filePath = filePath;
    }

    public List<Track> GetTracks() {
        var result = JsonSerializer.Deserialize<List<Track>>(File.ReadAllText(filePath));
        if (result == null) throw new FileNotFoundException($"file {filePath} not found");
        return result;
    }

    public Track GetTrack(int id) {
        var result = GetTracks().FirstOrDefault(a => a.Id == id);
        if (result == null) throw new KeyNotFoundException($"track {id} not found");
        return result;
    }

    public void AddTrack(Track track) {
        var list = GetTracks();
        var nextId = list.Any() ? list.Max(a => a.Id) + 1 : 0;
        track.Id = nextId;
        list.Add(track);
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }
}
