using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Mappers;

public static class GenreMapper {
    public static Genre ToDomain(this MusicArchive.Data.Models.Genre data) {
        return new Genre {
            Id = data.Id,
            Name = data.Name
        };
    }
    
    public static List<Genre> ToDomain(this IEnumerable<MusicArchive.Data.Models.Genre> list) {
        return list.Select(x => x.ToDomain()).ToList();
    }

    public static MusicArchive.Data.Models.Genre ToData(this Genre domain) {
        return new MusicArchive.Data.Models.Genre {
            Id = domain.Id,
            Name = domain.Name
        };
    }

    public static List<MusicArchive.Data.Models.Genre> ToData(this IEnumerable<Genre> list) {
        return list.Select(x => x.ToData()).ToList();
    }
}