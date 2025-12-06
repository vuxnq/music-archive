using Microsoft.Data.Sqlite;

namespace MusicArchive.Data.Models;

public class Artist {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Location { get; set; }
    public bool Approved { get; set; }

    public Artist() {}

    public Artist(SqliteDataReader reader) {
        Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
        Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
        BeginDate = reader.IsDBNull(2) ? DateTime.MinValue : reader.GetDateTime(2);
        EndDate = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3);
        Location = reader.IsDBNull(4) ? (string?)null : reader.GetString(4);
        Approved = reader.IsDBNull(5) ? false : reader.GetBoolean(5);
    }

    public override string ToString() {
        return $"id: {Id}, name: {Name}, beginDate: {BeginDate}, endDate: {EndDate}, location: {Location}, approved: {Approved}";
    }
}