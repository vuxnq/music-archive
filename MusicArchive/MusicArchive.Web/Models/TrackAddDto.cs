using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicArchive.Web.Models;

public class TrackAddDto {
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    [Required]
    public int Duration { get; set; }
    [Required]
    public int ReleaseId { get; set; }
    public List<SelectListItem> ReleaseOptions { get; set; } = [];
}
