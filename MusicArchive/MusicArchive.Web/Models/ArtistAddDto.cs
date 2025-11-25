using System.ComponentModel.DataAnnotations;

namespace MusicArchive.Web.Models;

public class ArtistAddDto {
    [Required]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime BeginDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    public string? Location { get; set; }
}
