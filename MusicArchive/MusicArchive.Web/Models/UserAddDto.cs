using System.ComponentModel.DataAnnotations;

namespace MusicArchive.Web.Models;

public class UserAddDto {
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string RepeatPassword { get; set; }
}
