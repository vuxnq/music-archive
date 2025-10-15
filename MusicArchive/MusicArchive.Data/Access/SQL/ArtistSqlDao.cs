using Microsoft.Data.Sqlite;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class ArtistSqlDao : IArtistDao {
    private string connectionString;

    public ArtistSqlDao(string connectionString) {
        this.connectionString = connectionString;
    }
    
    public  List<Artist> GetArtists() {
        var result = new List<Artist>();

        using var conn = new SqliteConnection(connectionString);
        conn.Open();
        
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT id, name, beginDate, endDate, location FROM artist";
        
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            result.Add(new Artist(reader));
        }
        return result;
    }

    public Artist GetArtist(int id) {
        using var conn = new SqliteConnection(connectionString);
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT id, name, beginDate, endDate, location FROM artist WHERE id = $id";
        cmd.Parameters.AddWithValue("$id", id);
        
        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new KeyNotFoundException($"artist {id} not found");

        return new Artist(reader);
    }
}