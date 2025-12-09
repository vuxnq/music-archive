using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Mappers;

public static class UserMapper {
    public static User ToDomain(this Data.Models.User data) {
        return new User {
            Id = data.Id,
            Username = data.Username,
            Password = data.Password,
            IsModerator = data.IsModerator,
        };
    }

    public static List<User> ToDomain(this IEnumerable<Data.Models.User> list) {
        return list.Select(x => x.ToDomain()).ToList();
    }

    public static Data.Models.User ToData(this User domain) {
        return new Data.Models.User {
            Id = domain.Id,
            Username = domain.Username,
            Password = domain.Password,
            IsModerator = domain.IsModerator,
        };
    }

    public static List<Data.Models.User> ToData(this IEnumerable<User> list) {
        return list.Select(x => x.ToData()).ToList();
    }
}
