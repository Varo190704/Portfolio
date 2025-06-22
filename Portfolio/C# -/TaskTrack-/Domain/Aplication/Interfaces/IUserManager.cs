using Domain.Utilities;

namespace Domain.Aplication.Interfaces;

public interface IUserManager
{
    IEnumerable<User> getAllUsers();
    User getUserByName(string name);
    void createUser(User user);
    void deleteUser(User user);
    void updateUser(User user);
    void thereisAdmin();
}