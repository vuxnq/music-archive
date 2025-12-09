using Microsoft.Data.Sqlite;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class GenreSqlDao : IGenreDao {
    private readonly SqliteConnection _connection;

    public GenreSqlDao(SqliteConnection connection) {
        _connection = connection;
    }

    public List<Genre> GetGenres() {
        var result = new List<Genre>();

        var cmd = _connection.CreateCommand();
        cmd.CommandText = "SELECT id, name, description, isApproved FROM genre";

        using var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            result.Add(new Genre(reader));
        }
        return result;
    }

    public Genre GetGenre(int id) {
        var cmd = _connection.CreateCommand();
        cmd.CommandText = "SELECT id, name, description, isApproved FROM genre WHERE id = $id";
        cmd.Parameters.AddWithValue("$id", id);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new KeyNotFoundException($"genre {id} not found");

        return new Genre(reader);
        ;
    }

    public void AddGenre(Genre genre) {
        var cmd = _connection.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO genre (name, description, isApproved)
            VALUES ($name, $description, $isApproved);
            SELECT last_insert_rowid();
        ";

        cmd.Parameters.AddWithValue("$name", genre.Name);
        cmd.Parameters.AddWithValue("$description", genre.Description ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("$isApproved", genre.IsApproved);

        var result = cmd.ExecuteScalar();
        if (result != null && long.TryParse(result.ToString(), out var id)) {
            genre.Id = (int)id;
        }
    }
}