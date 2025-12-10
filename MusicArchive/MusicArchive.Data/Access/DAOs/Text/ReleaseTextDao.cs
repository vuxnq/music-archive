using System.Text.Json;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class ReleaseTextDao : IReleaseDao {
    private readonly string filePath;

    public ReleaseTextDao(string filePath) {
        this.filePath = filePath;
    }

    public List<Release> GetReleases() {
        var result = JsonSerializer.Deserialize<List<Release>>(File.ReadAllText(filePath));
        if (result == null) throw new FileNotFoundException($"file {filePath} not found");
        return result;
    }

    public Release GetRelease(int id) {
        var result = GetReleases().FirstOrDefault(a => a.Id == id);
        if (result == null) throw new KeyNotFoundException($"release {id} not found");
        return result;
    }

    public void InsertRelease(Release release) {
        var list = GetReleases();
        var nextId = list.Any() ? list.Max(a => a.Id) + 1 : 0;
        release.Id = nextId;
        list.Add(release);
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }

    public void UpdateRelease(Release release) {
        var list = GetReleases();
        var idx = list.FindIndex(a => a.Id == release.Id);
        if (idx == -1) throw new KeyNotFoundException($"release {release.Id} not found");
        list[idx] = release;
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }

    public void DeleteRelease(int id) {
        var list = GetReleases();
        var idx = list.FindIndex(a => a.Id == id);
        if (idx == -1) throw new KeyNotFoundException($"release {id} not found");
        list.RemoveAt(idx);
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }
}