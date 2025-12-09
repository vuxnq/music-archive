using Microsoft.Data.Sqlite;

namespace MusicArchive.Domain.Models;

public class Release {
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int ArtistsId { get; set; }
    public int? GenreId { get; set; }
    public bool IsApproved { get; set; }

    public Artist? Artist { get; set; }
    public Genre? Genre { get; set; }
    public List<Track> Tracks { get; set; } = [];

    public Release() {}

    public override string ToString() {
        return $"id: {Id}, title: {Title}, description: {Description}, releaseDate: {ReleaseDate}, artistsId: {ArtistsId}, genreId: {GenreId}";
    }
}