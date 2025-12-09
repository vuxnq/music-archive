using Microsoft.Data.Sqlite;

namespace MusicArchive.Data.Models;

public class Genre {
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsApproved { get; set; }

    public Genre() {}

    public Genre(SqliteDataReader reader) {
        Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
        Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
        Description = reader.IsDBNull(2) ? null : reader.GetString(2);
        IsApproved = reader.IsDBNull(3) ? false : reader.GetBoolean(3);
    }

    public override string ToString() {
        return $"id: {Id}, name: {Name}, approved: {IsApproved}";
    }
}