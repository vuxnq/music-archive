using Microsoft.Data.Sqlite;

namespace MusicArchive.Domain.Models;

public class Artist {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Location { get; set; }

    public Artist() {}
    
    public override string ToString() {
        return $"(domain) id: {Id}, name: {Name}, beginDate: {BeginDate}, endDate: {EndDate}, location: {Location}";
    }
}