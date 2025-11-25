using Microsoft.Data.Sqlite;

namespace MusicArchive.Data.Models;

public class Track {
    public int Id { get; set; }
    public string Title { get; set; }
    public int Duration { get; set; }
    public int ReleaseId { get; set; }

    public Track() {}

    public Track(SqliteDataReader reader) {
        Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
        Title = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
        Duration = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
        ReleaseId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
    }

    public override string ToString() {
        return $"id: {Id}, title: {Title}, duration: {Duration}, releaseId: {ReleaseId}";
    }
}