using MusicArchive.Data;
using MusicArchive.Domain.Mappers;
using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public class UserService(IDataConnector connector) : IUserService {
    private readonly IUserDao _userDao = connector.CreateUserDao();

    public List<User> GetUsers() {
        return _userDao.GetUsers().ToDomain();
    }

    public User GetUser(int id) {
        return _userDao.GetUser(id).ToDomain();
    }

    public User GetUserByUsername(string username) {
        return _userDao.GetUserByUsername(username).ToDomain();
    }

    public void AddUser(User user) {
        connector.BeginTransaction();
        try {
            var data = user.ToData();
            _userDao.InsertUser(data);
            user.Id = data.Id;

            connector.Commit();
        } catch {
            connector.Rollback();
            throw;
        }
    }
}
