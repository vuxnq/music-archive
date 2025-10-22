
using MusicArchive.Domain.Models;

namespace MusicArchive.Domain;

public static class ModelMapper {
    // Artist
    public static Artist ToDomain(this MusicArchive.Data.Models.Artist data) {
        return new Artist {
            Id = data.Id,
            Name = data.Name,
            BeginDate = data.BeginDate,
            EndDate = data.EndDate,
            Location = data.Location,
        };
    }

    public static MusicArchive.Data.Models.Artist ToModel(this Artist domain) {
        return new MusicArchive.Data.Models.Artist {
            Id = domain.Id,
            Name = domain.Name,
            BeginDate = domain.BeginDate,
            EndDate = domain.EndDate,
            Location = domain.Location,
        };
    }

    public static List<Artist> ToDomain(this IEnumerable<MusicArchive.Data.Models.Artist> list) {
        return list.Select(x => x.ToDomain()).ToList();
    }
    
    // Release
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


    public static List<Release> ToDomain(this IEnumerable<MusicArchive.Data.Models.Release> list) {
        return list.Select(x => x.ToDomain()).ToList();
    }

    // Genre
    public static Genre ToDomain(this MusicArchive.Data.Models.Genre data) {
        return new Genre {
            Id = data.Id,
            Name = data.Name
        };
    }

    public static MusicArchive.Data.Models.Genre ToData(this Genre domain) {
        return new MusicArchive.Data.Models.Genre {
            Id = domain.Id,
            Name = domain.Name
        };
    }


    public static List<Genre> ToDomain(this IEnumerable<MusicArchive.Data.Models.Genre> list) {
        return list.Select(x => x.ToDomain()).ToList();
    }
}