using System.ComponentModel.DataAnnotations;

namespace MusicArchive.Web.Models;

public class GenreAddDto {
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
}
