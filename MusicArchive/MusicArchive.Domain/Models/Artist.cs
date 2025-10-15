namespace MusicArchive.Domain.Models;

public class Artist
{
    public int ArtistId { get; set; }
    public string Name { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Location { get; set; }
}