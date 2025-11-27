using Microsoft.Data.Sqlite;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class ArtistSqlDao : IArtistDao {
    private SqliteConnection connection;

    public ArtistSqlDao(SqliteConnection connection) {
        this.connection = connection;
    }

    public  List<Artist> GetArtists() {
        var result = new List<Artist>();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT id, name, beginDate, endDate, location FROM artist";

        using var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            result.Add(new Artist(reader));
        }
        return result;
    }

    public Artist GetArtist(int id) {
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT id, name, beginDate, endDate, location FROM artist WHERE id = $id";
        cmd.Parameters.AddWithValue("$id", id);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new KeyNotFoundException($"artist {id} not found");

        return new Artist(reader);
    }

    public void AddArtist(Artist artist) {
        var cmd = connection.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO artist (name, beginDate, endDate, location)
            VALUES ($name, $beginDate, $endDate, $location);
            SELECT last_insert_rowid();
        ";

        cmd.Parameters.AddWithValue("$name", artist.Name);
        cmd.Parameters.AddWithValue("$beginDate", artist.BeginDate);

        if (artist.EndDate.HasValue) cmd.Parameters.AddWithValue("$endDate", artist.EndDate.Value);
        else cmd.Parameters.AddWithValue("$endDate", DBNull.Value);

        cmd.Parameters.AddWithValue("$location", artist.Location ?? (object)DBNull.Value);

        var result = cmd.ExecuteScalar();
        if (result != null && long.TryParse(result.ToString(), out var id)) {
            artist.Id = (int)id;
        }
    }
}