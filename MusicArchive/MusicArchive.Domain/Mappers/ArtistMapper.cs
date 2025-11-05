using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Mappers;

public static class ArtistMapper {
    public static Artist ToDomain(this MusicArchive.Data.Models.Artist data) {
        return new Artist {
            Id = data.Id,
            Name = data.Name,
            BeginDate = data.BeginDate,
            EndDate = data.EndDate,
            Location = data.Location,
        };
    }

    public static List<Artist> ToDomain(this IEnumerable<MusicArchive.Data.Models.Artist> list) {
        return list.Select(x => x.ToDomain()).ToList();
    }

    public static MusicArchive.Data.Models.Artist ToData(this Artist domain) {
        return new MusicArchive.Data.Models.Artist {
            Id = domain.Id,
            Name = domain.Name,
            BeginDate = domain.BeginDate,
            EndDate = domain.EndDate,
            Location = domain.Location,
        };
    }

    public static List<MusicArchive.Data.Models.Artist> ToData(this IEnumerable<Artist> list) {
        return list.Select(x => x.ToData()).ToList();
    }
}