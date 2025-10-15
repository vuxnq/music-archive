using System.Text.Json;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class GenreTextDao : IGenreDao {
    private string filePath;

    public GenreTextDao(string filePath) {
        this.filePath = filePath;
    }
    
    public List<Genre> GetGenres() {
        var result = JsonSerializer.Deserialize<List<Genre>>(File.ReadAllText(filePath));
        if (result == null) throw new FileNotFoundException($"file {filePath} not found");
        return result;
    }

    public Genre GetGenre(int id) {
        var result = GetGenres().FirstOrDefault(a => a.Id == id);
        if (result == null) throw new KeyNotFoundException($"genre {id} not found");
        return result;
    }
}