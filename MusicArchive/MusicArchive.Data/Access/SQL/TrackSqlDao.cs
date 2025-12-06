using Microsoft.Data.Sqlite;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class TrackSqlDao : ITrackDao {
    private SqliteConnection connection;

    public TrackSqlDao(SqliteConnection connection) {
        this.connection = connection;
    }

    public List<Track> GetTracks() {
        var result = new List<Track>();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT id, title, duration, releaseId, approved FROM track";

        using var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            result.Add(new Track(reader));
        }
        return result;
    }

    public Track GetTrack(int id) {
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT id, title, duration, releaseId, approved FROM track WHERE id = $id";
        cmd.Parameters.AddWithValue("$id", id);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new KeyNotFoundException($"track {id} not found");

        return new Track(reader);
    }

    public void AddTrack(Track track) {
        var cmd = connection.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO track (title, duration, releaseId, approved)
            VALUES ($title, $duration, $releaseId, $approved);
            SELECT last_insert_rowid();
        ";

        cmd.Parameters.AddWithValue("$title", track.Title);
        cmd.Parameters.AddWithValue("$duration", track.Duration);
        cmd.Parameters.AddWithValue("$releaseId", track.ReleaseId);
        cmd.Parameters.AddWithValue("$approved", track.Approved);

        var result = cmd.ExecuteScalar();
        if (result != null && long.TryParse(result.ToString(), out var id)) {
            track.Id = (int)id;
        }
    }
}
