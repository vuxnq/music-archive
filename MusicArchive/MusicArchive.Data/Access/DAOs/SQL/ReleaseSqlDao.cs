using Microsoft.Data.Sqlite;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class ReleaseSqlDao : IReleaseDao {
    private readonly SqliteConnection _connection;

    public ReleaseSqlDao(SqliteConnection connection) {
        _connection = connection;
    }

    public List<Release> GetReleases() {
        var result = new List<Release>();

        var cmd = _connection.CreateCommand();
        cmd.CommandText = "SELECT id, title, description, releaseDate, artistId, genreId, isApproved FROM release";

        using var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            result.Add(new Release(reader));
        }
        return result;
    }

    public Release GetRelease(int id) {
        var cmd = _connection.CreateCommand();
        cmd.CommandText = "SELECT id, title, description, releaseDate, artistId, genreId, isApproved FROM release WHERE id = $id";
        cmd.Parameters.AddWithValue("$id", id);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new KeyNotFoundException($"release {id} not found");

        return new Release(reader);
    }

    public void InsertRelease(Release release) {
        var cmd = _connection.CreateCommand();

        cmd.CommandText = @"
            INSERT INTO ""release"" (title, description, releaseDate, artistId, genreId, isApproved)
            VALUES ($title, $description, $releaseDate, $artistId, $genreId, $isApproved);
            SELECT last_insert_rowid();
        ";

        cmd.Parameters.AddWithValue("$title", release.Title);
        cmd.Parameters.AddWithValue("$description", release.Description ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("$releaseDate", release.ReleaseDate);
        cmd.Parameters.AddWithValue("$artistId", release.ArtistsId);
        if (release.GenreId.HasValue) cmd.Parameters.AddWithValue("$genreId", release.GenreId.Value);
        else cmd.Parameters.AddWithValue("$genreId", DBNull.Value);
        cmd.Parameters.AddWithValue("$isApproved", release.IsApproved);

        var result = cmd.ExecuteScalar();
        if (result != null && long.TryParse(result.ToString(), out var id)) {
            release.Id = (int)id;
        }
    }

    public void UpdateRelease(Release release) {
        var cmd = _connection.CreateCommand();
        cmd.CommandText = @"
            UPDATE ""release""
            SET title = $title,
                description = $description,
                releaseDate = $releaseDate,
                artistId = $artistId,
                genreId = $genreId,
                isApproved = $isApproved
            WHERE id = $id;
        ";

        cmd.Parameters.AddWithValue("$id", release.Id);
        cmd.Parameters.AddWithValue("$title", release.Title);
        cmd.Parameters.AddWithValue("$description", release.Description ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("$releaseDate", release.ReleaseDate);
        cmd.Parameters.AddWithValue("$artistId", release.ArtistsId);
        if (release.GenreId.HasValue) cmd.Parameters.AddWithValue("$genreId", release.GenreId.Value);
        else cmd.Parameters.AddWithValue("$genreId", DBNull.Value);
        cmd.Parameters.AddWithValue("$isApproved", release.IsApproved);

        int affected = cmd.ExecuteNonQuery();
        if (affected == 0) throw new KeyNotFoundException($"release {release.Id} not found");
    }

    public void DeleteRelease(int id) {
        var cmd = _connection.CreateCommand();
        cmd.CommandText = @"DELETE FROM ""release"" WHERE id = $id;";
        cmd.Parameters.AddWithValue("$id", id);

        int affected = cmd.ExecuteNonQuery();
        if (affected == 0) throw new KeyNotFoundException($"release {id} not found");
    }
}