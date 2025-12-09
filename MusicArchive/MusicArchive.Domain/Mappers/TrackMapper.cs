using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Mappers;

public static class TrackMapper {
    public static Track ToDomain(this Data.Models.Track data) {
        return new Track {
            Id = data.Id,
            Title = data.Title,
            Description = data.Description,
            Duration = data.Duration,
            ReleaseId = data.ReleaseId,
            IsApproved = data.IsApproved
        };
    }

    public static List<Track> ToDomain(this IEnumerable<Data.Models.Track> list) {
        return list.Select(x => x.ToDomain()).ToList();
    }

    public static Data.Models.Track ToData(this Track domain) {
        return new Data.Models.Track {
            Id = domain.Id,
            Title = domain.Title,
            Description = domain.Description,
            Duration = domain.Duration,
            ReleaseId = domain.ReleaseId,
            IsApproved = domain.IsApproved
        };
    }

    public static List<Data.Models.Track> ToData(this IEnumerable<Track> list) {
        return list.Select(x => x.ToData()).ToList();
    }
}
