using System;

namespace MusicArchive.Domain.Models;

public class Track {
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }
    public int ReleaseId { get; set; }
    public bool IsApproved { get; set; }

    public Release? Release { get; set; }

    public Track() {}

    public override string ToString() {
        return $"id: {Id}, title: {Title}, description: {Description}, duration: {Duration}, releaseId: {ReleaseId}, approved: {IsApproved}";
    }
}
