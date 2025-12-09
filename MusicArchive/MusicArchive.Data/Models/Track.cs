using Microsoft.Data.Sqlite;

namespace MusicArchive.Data.Models;

public class Track {
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }
    public int ReleaseId { get; set; }
    public bool IsApproved { get; set; }

    public Track() {}

    public Track(SqliteDataReader reader) {
        Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
        Title = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
        Description = reader.IsDBNull(2) ? null : reader.GetString(2);
        Duration = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
        ReleaseId = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
        IsApproved = reader.IsDBNull(5) ? false : reader.GetBoolean(5);
    }

    public override string ToString() {
        return $"id: {Id}, title: {Title}, description: {Description}, duration: {Duration}, releaseId: {ReleaseId}, approved: {IsApproved}";
    }
}