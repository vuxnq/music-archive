using Microsoft.Data.Sqlite;

namespace MusicArchive.Data.Models;

public class User {
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public User() {}

    public User(SqliteDataReader reader) {
        Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
        Username = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
        Password = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
    }

    public override string ToString() {
        return $"id: {Id}, username: {Username}";
    }
}