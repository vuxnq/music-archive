using Microsoft.Data.Sqlite;

namespace MusicArchive.Data.Models;

public class Artist {
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Location { get; set; }
    public bool IsApproved { get; set; }

    public Artist() {}

    public Artist(SqliteDataReader reader) {
        Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
        Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
        Description = reader.IsDBNull(2) ? null : reader.GetString(2);
        BeginDate = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3);
        EndDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
        Location = reader.IsDBNull(5) ? (string?)null : reader.GetString(5);
        IsApproved = reader.IsDBNull(6) ? false : reader.GetBoolean(6);
    }

    public override string ToString() {
        return $"id: {Id}, name: {Name}, beginDate: {BeginDate}, endDate: {EndDate}, location: {Location}, approved: {IsApproved}";
    }
}