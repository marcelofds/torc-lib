using TorcLib.Domain.Aggregates;
using TorcLib.Domain.Aggregates.Repositories;

namespace TorcLib.Data.Repositories;

public class UserRepository : IUserRepository
{
    public User? Get(string username, string password)
    {
        var users = new List<User>
        {
            new() {Id = 1, UserName = "libusr1", Password = "123@", Role = "Manager"},
            new() {Id = 2, UserName = "libusr2", Password = "456@", Role = "Employee"}
        };
        return users.FirstOrDefault(u => u.UserName == username && u.Password == password);
    }
}