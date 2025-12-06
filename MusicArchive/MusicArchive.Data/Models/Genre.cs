using Microsoft.Data.Sqlite;

namespace MusicArchive.Data.Models;

public class Genre {
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Approved { get; set; }

    public Genre() {}

    public Genre(SqliteDataReader reader) {
        Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
        Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
        Approved = reader.IsDBNull(2) ? false : reader.GetBoolean(2);
    }

    public override string ToString() {
        return $"id: {Id}, name: {Name}, approved: {Approved}";
    }
}