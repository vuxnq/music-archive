using System.ComponentModel.DataAnnotations;

namespace MusicArchive.Web.Models;

public class TrackAddDto {
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    [Required]
    public int Duration { get; set; }
    [Required]
    public int ReleaseId { get; set; }
}
