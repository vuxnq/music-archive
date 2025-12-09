using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Mappers;

public static class GenreMapper {
    public static Genre ToDomain(this Data.Models.Genre data) {
        return new Genre {
            Id = data.Id,
            Name = data.Name,
            Description = data.Description,
            IsApproved = data.IsApproved
        };
    }

    public static List<Genre> ToDomain(this IEnumerable<Data.Models.Genre> list) {
        return list.Select(x => x.ToDomain()).ToList();
    }

    public static Data.Models.Genre ToData(this Genre domain) {
        return new Data.Models.Genre {
            Id = domain.Id,
            Name = domain.Name,
            Description = domain.Description,
            IsApproved = domain.IsApproved
        };
    }

    public static List<Data.Models.Genre> ToData(this IEnumerable<Genre> list) {
        return list.Select(x => x.ToData()).ToList();
    }
}