using Microsoft.Data.Sqlite;

namespace MusicArchive.Domain.Models;

public class Release {
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int ArtistsId { get; set; }
    public int? GenreId { get; set; }

    public Release() {}

    public override string ToString() {
        return $"id: {Id}, title: {Title}, description: {Description}, releaseDate: {ReleaseDate}, artistsId: {ArtistsId}, genreId: {GenreId}";
    }
}