using System.Text.Json;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class UserTextDao : IUserDao {
    private string filePath;

    public UserTextDao(string filePath) {
        this.filePath = filePath;
    }

    public List<User> GetUsers() {
        var result = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(filePath));
        if (result == null) throw new FileNotFoundException($"file {filePath} not found");
        return result;
    }

    public User GetUser(int id) {
        var result = GetUsers().FirstOrDefault(a => a.Id == id);
        if (result == null) throw new KeyNotFoundException($"user {id} not found");
        return result;
    }

    public User GetUserByUsername(string username) {
        var result = GetUsers().FirstOrDefault(a => a.Username == username);
        if (result == null) throw new KeyNotFoundException($"user {username} not found");
        return result;
    }

    public void AddUser(User user) {
        var list = GetUsers();
        var nextId = list.Any() ? list.Max(a => a.Id) + 1 : 0;
        user.Id = nextId;
        list.Add(user);
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }
}
