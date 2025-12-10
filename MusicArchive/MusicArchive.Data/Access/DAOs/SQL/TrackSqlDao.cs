using Microsoft.Data.Sqlite;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class TrackSqlDao : ITrackDao {
    private readonly SqliteConnection _connection;

    public TrackSqlDao(SqliteConnection connection) {
        _connection = connection;
    }

    public List<Track> GetTracks() {
        var result = new List<Track>();

        var cmd = _connection.CreateCommand();
        cmd.CommandText = "SELECT id, title, description, duration, releaseId, isApproved FROM track";

        using var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            result.Add(new Track(reader));
        }
        return result;
    }

    public Track GetTrack(int id) {
        var cmd = _connection.CreateCommand();
        cmd.CommandText = "SELECT id, title, description, duration, releaseId, isApproved FROM track WHERE id = $id";
        cmd.Parameters.AddWithValue("$id", id);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new KeyNotFoundException($"track {id} not found");

        return new Track(reader);
    }

    public void InsertTrack(Track track) {
        var cmd = _connection.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO track (title, description, duration, releaseId, isApproved)
            VALUES ($title, $description, $duration, $releaseId, $isApproved);
            SELECT last_insert_rowid();
        ";

        cmd.Parameters.AddWithValue("$title", track.Title);
        cmd.Parameters.AddWithValue("$description", track.Description ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("$duration", track.Duration);
        cmd.Parameters.AddWithValue("$releaseId", track.ReleaseId);
        cmd.Parameters.AddWithValue("$isApproved", track.IsApproved);

        var result = cmd.ExecuteScalar();
        if (result != null && long.TryParse(result.ToString(), out var id)) {
            track.Id = (int)id;
        }
    }

    public void UpdateTrack(Track track) {
        var cmd = _connection.CreateCommand();
        cmd.CommandText = @"
            UPDATE track
            SET title = $title,
                description = $description,
                duration = $duration,
                releaseId = $releaseId,
                isApproved = $isApproved
            WHERE id = $id;
        ";

        cmd.Parameters.AddWithValue("$id", track.Id);
        cmd.Parameters.AddWithValue("$title", track.Title);
        cmd.Parameters.AddWithValue("$description", track.Description ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("$duration", track.Duration);
        cmd.Parameters.AddWithValue("$releaseId", track.ReleaseId);
        cmd.Parameters.AddWithValue("$isApproved", track.IsApproved);

        int affected = cmd.ExecuteNonQuery();
        if (affected == 0) throw new KeyNotFoundException($"track {track.Id} not found");
    }

    public void DeleteTrack(int id) {
        var cmd = _connection.CreateCommand();
        cmd.CommandText = @"DELETE FROM track WHERE id = $id;";
        cmd.Parameters.AddWithValue("$id", id);

        int affected = cmd.ExecuteNonQuery();
        if (affected == 0) throw new KeyNotFoundException($"track {id} not found");
    }
}
