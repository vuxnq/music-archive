namespace MusicArchive.Domain.Models;

public class Genre
{
    public int GenreID { get; set; }
    public string Name { get; set; }
    public Genre? parentGenre { get; set; }
}