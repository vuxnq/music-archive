using System.ComponentModel.DataAnnotations;

namespace MusicArchive.Web.Models;

public class ReleaseAddDto {
    [Required]
    public string Title { get; set; }

    public string? Description { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    [Required]
    public int ArtistsId { get; set; }

    public int? GenreId { get; set; }
}
