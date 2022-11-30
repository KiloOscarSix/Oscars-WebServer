using Oscars_WebApplication.Domain;

namespace Oscars_WebApplication.Services;

public class LovenseService : ILovenseService
{
    private readonly Dictionary<string, LovenseUser> _users;

    public LovenseService()
    {
        _users = new Dictionary<string, LovenseUser>();
    }
    
    public List<LovenseUser> GetUsers()
    {
        return _users.Values.ToList();
    }

    public LovenseUser? GetUserById(string userId)
    {
        return _users[userId];
    }

    public void CreateUser(string userId, LovenseUser user)
    {
        _users.Add(userId, user);
    }
}