using Microsoft.Data.Sqlite;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class ArtistSqlDao : IArtistDao {
    private readonly SqliteConnection _connection;

    public ArtistSqlDao(SqliteConnection connection) {
        _connection = connection;
    }

    public  List<Artist> GetArtists() {
        var result = new List<Artist>();

        var cmd = _connection.CreateCommand();
        cmd.CommandText = "SELECT id, name, description, beginDate, endDate, location, isApproved FROM artist";

        using var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            result.Add(new Artist(reader));
        }
        return result;
    }

    public Artist GetArtist(int id) {
        var cmd = _connection.CreateCommand();
        cmd.CommandText = "SELECT id, name, description, beginDate, endDate, location, isApproved FROM artist WHERE id = $id";
        cmd.Parameters.AddWithValue("$id", id);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new KeyNotFoundException($"artist {id} not found");

        return new Artist(reader);
    }

    public void AddArtist(Artist artist) {
        var cmd = _connection.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO artist (name, description, beginDate, endDate, location, isApproved)
            VALUES ($name, $description, $beginDate, $endDate, $location, $isApproved);
            SELECT last_insert_rowid();
        ";

        cmd.Parameters.AddWithValue("$name", artist.Name);
        cmd.Parameters.AddWithValue("$description", artist.Description ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("$beginDate", artist.BeginDate);

        if (artist.EndDate.HasValue) cmd.Parameters.AddWithValue("$endDate", artist.EndDate.Value);
        else cmd.Parameters.AddWithValue("$endDate", DBNull.Value);

        cmd.Parameters.AddWithValue("$location", artist.Location ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("$isApproved", artist.IsApproved);

        var result = cmd.ExecuteScalar();
        if (result != null && long.TryParse(result.ToString(), out var id)) {
            artist.Id = (int)id;
        }
    }
}