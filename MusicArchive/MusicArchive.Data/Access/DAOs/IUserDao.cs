using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public interface IUserDao {
    List<User> GetUsers();
    User GetUser(int id);
    User GetUserByUsername(string username);
    void InsertUser(User user);
    void UpdateUser(User user);
    void DeleteUser(int id);
}
