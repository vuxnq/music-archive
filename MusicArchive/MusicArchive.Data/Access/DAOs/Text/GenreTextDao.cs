using System.Text.Json;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class GenreTextDao : IGenreDao {
    private readonly string filePath;

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

    public void InsertGenre(Genre genre) {
        var list = GetGenres();
        var nextId = list.Any() ? list.Max(a => a.Id) + 1 : 0;
        genre.Id = nextId;
        list.Add(genre);
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }

    public void UpdateGenre(Genre genre) {
        var list = GetGenres();
        var idx = list.FindIndex(a => a.Id == genre.Id);
        if (idx == -1) throw new KeyNotFoundException($"genre {genre.Id} not found");
        list[idx] = genre;
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }

    public void DeleteGenre(int id) {
        var list = GetGenres();
        var idx = list.FindIndex(a => a.Id == id);
        if (idx == -1) throw new KeyNotFoundException($"genre {id} not found");
        list.RemoveAt(idx);
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }
}