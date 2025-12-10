using System.ComponentModel.DataAnnotations;

namespace MusicArchive.Web.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

public class ReleaseAddDto {
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    [Required]
    public int ArtistId { get; set; }
    public int? GenreId { get; set; }
    public List<TrackAddDto> Tracks { get; set; } = [];
    public List<SelectListItem> ArtistOptions { get; set; } = [];
    public List<SelectListItem> GenreOptions { get; set; } = [];
}
