using Microsoft.Data.Sqlite;

namespace MusicArchive.Domain.Models;

public class Genre {
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsApproved { get; set; }

    public List<Release> Releases { get; set; } = [];


    public Genre() {}

    public override string ToString() {
        return $"id: {Id}, name: {Name}, description: {Description}, approved: {IsApproved}";
    }
}