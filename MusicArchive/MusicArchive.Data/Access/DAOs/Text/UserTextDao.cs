using System.Text.Json;
using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public class UserTextDao : IUserDao {
    private readonly string filePath;

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

    public void InsertUser(User user) {
        var list = GetUsers();
        var nextId = list.Any() ? list.Max(a => a.Id) + 1 : 0;
        user.Id = nextId;
        list.Add(user);
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }

    public void UpdateUser(User user) {
        var list = GetUsers();
        var idx = list.FindIndex(a => a.Id == user.Id);
        if (idx == -1) throw new KeyNotFoundException($"user {user.Id} not found");
        list[idx] = user;
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }

    public void DeleteUser(int id) {
        var list = GetUsers();
        var idx = list.FindIndex(a => a.Id == id);
        if (idx == -1) throw new KeyNotFoundException($"user {id} not found");
        list.RemoveAt(idx);
        var opts = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(list, opts));
    }
}
