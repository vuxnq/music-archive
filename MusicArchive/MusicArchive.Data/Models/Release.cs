using Microsoft.Data.Sqlite;

namespace MusicArchive.Data.Models;

public class Release {
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int ArtistsId { get; set; }
    public int? GenreId { get; set; }

    public Release() {}

    public Release(SqliteDataReader reader) {
        Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
        Title = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
        Description = reader.IsDBNull(2) ? null : reader.GetString(2);
        ReleaseDate = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3);
        ArtistsId = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
        GenreId = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5);
    }

    public override string ToString() {
        return $"id: {Id}, title: {Title}, description: {Description}, releaseDate: {ReleaseDate}, artistsId: {ArtistsId}, genreId: {GenreId}";
    }
}