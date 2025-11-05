using Microsoft.Data.Sqlite;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class ReleaseSqlDao : IReleaseDao {
    private SqliteConnection connection;

    public ReleaseSqlDao(SqliteConnection connection) {
        this.connection = connection;
    }

    public List<Release> GetReleases() {
        var result = new List<Release>();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT id, title, description, releaseDate, artistId, genreId FROM release";

        using var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            result.Add(new Release(reader));
        }
        return result;
    }

    public Release GetRelease(int id) {
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT id, title, description, releaseDate, artistId, genreId FROM release WHERE id = $id";
        cmd.Parameters.AddWithValue("$id", id);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new KeyNotFoundException($"release {id} not found");

        return new Release(reader);
    }

    public void AddRelease(Release release) {
        var cmd = connection.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO ""release"" (title, description, releaseDate, artistId, genreId)
            VALUES ($title, $description, $releaseDate, $artistId, $genreId);
            SELECT last_insert_rowid();
        ";

        cmd.Parameters.AddWithValue("$title", release.Title);
        cmd.Parameters.AddWithValue("$description", release.Description ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("$releaseDate", release.ReleaseDate);
        cmd.Parameters.AddWithValue("$artistId", release.ArtistsId);
        if (release.GenreId.HasValue) cmd.Parameters.AddWithValue("$genreId", release.GenreId.Value);
        else cmd.Parameters.AddWithValue("$genreId", DBNull.Value);

        var result = cmd.ExecuteScalar();
        if (result != null && long.TryParse(result.ToString(), out var id)) {
            release.Id = (int)id;
        }
    }
}