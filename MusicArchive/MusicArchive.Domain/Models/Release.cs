namespace MusicArchive.Domain.Models;

public class Release
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    List<Artist>? Artists { get; set; }
    List<Genre> Genres { get; set; }
}