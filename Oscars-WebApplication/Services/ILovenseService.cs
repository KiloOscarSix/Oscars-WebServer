using Oscars_WebApplication.Domain;

namespace Oscars_WebApplication.Services;

public interface ILovenseService
{
    List<LovenseUser> GetUsers();

    LovenseUser? GetUserById(string userId);
    void CreateUser(string userUid, LovenseUser user);
}