using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Mappers;

public static class ReleaseMapper {
    public static Release ToDomain(this Data.Models.Release data) {
        return new Release {
            Id = data.Id,
            Title = data.Title,
            Description = data.Description,
            ReleaseDate = data.ReleaseDate,
            ArtistsId = data.ArtistsId,
            GenreId = data.GenreId,
            IsApproved = data.IsApproved
        };
    }

    public static List<Release> ToDomain(this IEnumerable<Data.Models.Release> list) {
        return list.Select(x => x.ToDomain()).ToList();
    }

    public static Data.Models.Release ToData(this Release domain) {
        return new Data.Models.Release {
            Id = domain.Id,
            Title = domain.Title,
            Description = domain.Description,
            ReleaseDate = domain.ReleaseDate,
            ArtistsId = domain.ArtistsId,
            GenreId = domain.GenreId,
            IsApproved = domain.IsApproved
        };
    }

    public static List<Data.Models.Release> ToData(this IEnumerable<Release> list) {
        return list.Select(x => x.ToData()).ToList();
    }
}