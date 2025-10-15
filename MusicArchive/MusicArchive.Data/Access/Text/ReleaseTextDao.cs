using System.Text.Json;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class ReleaseTextDao : IReleaseDao {
    private string filePath;

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
}