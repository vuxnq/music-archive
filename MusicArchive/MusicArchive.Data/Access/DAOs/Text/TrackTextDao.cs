using System.Text.Json;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class TrackTextDao : ITrackDao {
    private readonly string filePath;

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

    public void InsertTrack(Track track) {
        var list = GetTracks();
        var nextId = list.Any() ? list.Max(a => a.Id) + 1 : 0;
        track.Id = nextId;
        list.Add(track);
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }

    public void UpdateTrack(Track track) {
        var list = GetTracks();
        var idx = list.FindIndex(a => a.Id == track.Id);
        if (idx == -1) throw new KeyNotFoundException($"track {track.Id} not found");
        list[idx] = track;
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }

    public void DeleteTrack(int id) {
        var list = GetTracks();
        var idx = list.FindIndex(a => a.Id == id);
        if (idx == -1) throw new KeyNotFoundException($"track {id} not found");
        list.RemoveAt(idx);
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }
}
