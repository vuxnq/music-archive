using Microsoft.Data.Sqlite;

namespace MusicArchive.Data.Models;

public class Genre {
    public int Id { get; set; }
    public string Name { get; set; }

    public Genre() {}
    
    public Genre(SqliteDataReader reader) {
        Id = reader.GetInt32(0);
        Name = reader.GetString(1);
    }

    public override string ToString() {
        return $"id: {Id}, name: {Name}";
    }
}