using Microsoft.Data.Sqlite;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class GenreSqlDao : IGenreDao {
    private SqliteConnection connection;

    public GenreSqlDao(SqliteConnection connection) {
        this.connection = connection;
    }

    public List<Genre> GetGenres() {
        var result = new List<Genre>();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT id, name, approved FROM genre";

        using var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            result.Add(new Genre(reader));
        }
        return result;
    }

    public Genre GetGenre(int id) {
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT id, name, approved FROM genre WHERE id = $id";
        cmd.Parameters.AddWithValue("$id", id);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new KeyNotFoundException($"genre {id} not found");

        return new Genre(reader);
        ;
    }

    public void AddGenre(Genre genre) {
        var cmd = connection.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO genre (name, approved)
            VALUES ($name, $approved);
            SELECT last_insert_rowid();
        ";

        cmd.Parameters.AddWithValue("$name", genre.Name);
        cmd.Parameters.AddWithValue("$approved", genre.Approved);

        var result = cmd.ExecuteScalar();
        if (result != null && long.TryParse(result.ToString(), out var id)) {
            genre.Id = (int)id;
        }
    }
}