using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Mappers;

public static class ReleaseMapper {
    public static Release ToDomain(this MusicArchive.Data.Models.Release data) {
        return new Release {
            Id = data.Id,
            Title = data.Title,
            Description = data.Description,
            ReleaseDate = data.ReleaseDate,
            ArtistsId = data.ArtistsId,
            GenreId = data.GenreId
        };
    }

    public static List<Release> ToDomain(this IEnumerable<MusicArchive.Data.Models.Release> list) {
        return list.Select(x => x.ToDomain()).ToList();
    }
    
    public static MusicArchive.Data.Models.Release ToData(this Release domain) {
        return new MusicArchive.Data.Models.Release {
            Id = domain.Id,
            Title = domain.Title,
            Description = domain.Description,
            ReleaseDate = domain.ReleaseDate,
            ArtistsId = domain.ArtistsId,
            GenreId = domain.GenreId
        };
    }
    
    public static List<MusicArchive.Data.Models.Release> ToData(this IEnumerable<Release> list) {
        return list.Select(x => x.ToData()).ToList();
    }
}