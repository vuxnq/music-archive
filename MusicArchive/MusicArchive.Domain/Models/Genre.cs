using Microsoft.Data.Sqlite;

namespace MusicArchive.Domain.Models;

public class Genre {
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Approved { get; set; }

    public List<Release> Releases { get; set; } = new List<Release>();


    public Genre() {}

    public override string ToString() {
        return $"id: {Id}, name: {Name}";
    }
}