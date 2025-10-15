using Microsoft.Data.Sqlite;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class ReleaseSqlDao : IReleaseDao {
    private string connectionString;

    public ReleaseSqlDao(string connectionString) {
        this.connectionString = connectionString;
    }
    
    public List<Release> GetReleases() {
        var result = new List<Release>();

        using var conn = new SqliteConnection(connectionString);
        conn.Open();
        
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT id, title, description, releaseDate, artistId, genreId FROM release";
        
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            result.Add(new Release(reader));
        }

        return result;
    }

    public Release GetRelease(int id) {
        using var conn = new SqliteConnection(connectionString);
        conn.Open();
        
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT id, title, description, releaseDate, artistId, genreId FROM release WHERE id = $id";
        cmd.Parameters.AddWithValue("$id", id);
        
        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new KeyNotFoundException($"release {id} not found");

        return new Release(reader);
    }
}