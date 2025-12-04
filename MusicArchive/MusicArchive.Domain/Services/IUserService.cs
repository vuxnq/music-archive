using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public interface IUserService {
    List<User> GetUsers();
    User GetUser(int id);
    User GetUserByUsername(string username);
    void AddUser(User user);
}
