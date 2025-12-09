using MusicArchive.Domain.Models;

namespace MusicArchive.Web.Models;

public class DashboardIndexDto {
    public List<Artist> Artists { get; set; } = [];
    public List<Release> Releases { get; set; } = [];
    public List<Track> Tracks { get; set; } = [];
    public List<Genre> Genres { get; set; } = [];
}