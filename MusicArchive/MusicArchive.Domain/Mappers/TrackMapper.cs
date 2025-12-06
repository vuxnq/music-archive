using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Mappers;

public static class TrackMapper {
    public static Track ToDomain(this MusicArchive.Data.Models.Track data) {
        return new Track {
            Id = data.Id,
            Title = data.Title,
            Duration = data.Duration,
            ReleaseId = data.ReleaseId,
            Approved = data.Approved
        };
    }

    public static List<Track> ToDomain(this IEnumerable<MusicArchive.Data.Models.Track> list) {
        return list.Select(x => x.ToDomain()).ToList();
    }

    public static MusicArchive.Data.Models.Track ToData(this Track domain) {
        return new MusicArchive.Data.Models.Track {
            Id = domain.Id,
            Title = domain.Title,
            Duration = domain.Duration,
            ReleaseId = domain.ReleaseId,
            Approved = domain.Approved
        };
    }

    public static List<MusicArchive.Data.Models.Track> ToData(this IEnumerable<Track> list) {
        return list.Select(x => x.ToData()).ToList();
    }
}
