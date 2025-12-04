using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public interface IUserDao {
    List<User> GetUsers();
    User GetUser(int id);
    User GetUserByUsername(string username);
    void AddUser(User user);
}
