using Microsoft.Data.Sqlite;

namespace MusicArchive.Domain.Models;

public class User {
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsModerator { get; set; }

    public User() {}

    public override string ToString() {
        return $"id: {Id}, username: {Username}";
    }
}
