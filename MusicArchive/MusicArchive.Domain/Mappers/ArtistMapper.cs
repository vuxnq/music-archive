using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Mappers;

public static class ArtistMapper {
    public static Artist ToDomain(this Data.Models.Artist data) {
        return new Artist {
            Id = data.Id,
            Name = data.Name,
            Description = data.Description,
            BeginDate = data.BeginDate,
            EndDate = data.EndDate,
            Location = data.Location,
            IsApproved = data.IsApproved,
        };
    }

    public static List<Artist> ToDomain(this IEnumerable<Data.Models.Artist> list) {
        return list.Select(x => x.ToDomain()).ToList();
    }

    public static Data.Models.Artist ToData(this Artist domain) {
        return new Data.Models.Artist {
            Id = domain.Id,
            Name = domain.Name,
            Description = domain.Description,
            BeginDate = domain.BeginDate,
            EndDate = domain.EndDate,
            Location = domain.Location,
            IsApproved = domain.IsApproved,
        };
    }

    public static List<Data.Models.Artist> ToData(this IEnumerable<Artist> list) {
        return list.Select(x => x.ToData()).ToList();
    }
}