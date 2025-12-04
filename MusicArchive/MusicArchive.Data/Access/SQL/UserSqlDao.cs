using Microsoft.Data.Sqlite;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class UserSqlDao : IUserDao {
    private SqliteConnection connection;

    public UserSqlDao(SqliteConnection connection) {
        this.connection = connection;
    }

    public List<User> GetUsers() {
        var result = new List<User>();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT id, username, password FROM user";

        using var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            result.Add(new User(reader));
        }
        return result;
    }

    public User GetUser(int id) {
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT id, username, password FROM user WHERE id = $id";
        cmd.Parameters.AddWithValue("$id", id);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new KeyNotFoundException($"user {id} not found");

        return new User(reader);
    }

    public User GetUserByUsername(string username) {
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT id, username, password FROM user WHERE username = $username";
        cmd.Parameters.AddWithValue("$username", username);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new KeyNotFoundException($"user {username} not found");

        return new User(reader);
    }

    public void AddUser(User user) {
        var cmd = connection.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO user (username, password)
            VALUES ($username, $password);
            SELECT last_insert_rowid();
        ";

        cmd.Parameters.AddWithValue("$username", user.Username);
        cmd.Parameters.AddWithValue("$password", user.Password);

        var result = cmd.ExecuteScalar();
        if (result != null && long.TryParse(result.ToString(), out var id)) {
            user.Id = (int)id;
        }
    }
}
