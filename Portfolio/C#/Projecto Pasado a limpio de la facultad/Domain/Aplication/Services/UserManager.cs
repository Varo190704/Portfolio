using Domain.Aplication.Interfaces;
using Domain.Utilities;
using IList = Domain.Infrastructure.Persistence;

namespace Domain.Aplication.Services;

public class UserManager : IUserManager
{
    private readonly IList.IList<User> UserRepo;
    private const string AdminEmail    = "admin@sistema.com";
    private const string AdminPassword = "$Admin123";
    
    public UserManager(IList.IList<User> userRepo)
    {
        UserRepo = userRepo;
        thereisAdmin();
    }

    public void thereisAdmin()
    {
        User existing = UserRepo.getByName(AdminEmail);
        if (existing == null)
        {
            User admin = new User { Name = "Administrador", Surname = "Sistema", Email = AdminEmail, Password = AdminPassword, BirthDate = new DateOnly(2004, 10, 10)};
            admin.AdminSistem = true;
            admin.login = true;
            UserRepo.Add(admin);
        }
    }
    
    public IEnumerable<User> getAllUsers()
    {
        return UserRepo.GetAll();
    }

    public User getUserByName(string email)
    {
        return UserRepo.getByName(email);
    }

    public void createUser(User user)
    {
        if (!Equals(UserRepo.getByName(user.Email), null))
        {
            throw new ArgumentException("User already exists or other user is using this email");
        }
        UserRepo.Add(user);
    }
    public void deleteUser(User user)
    {
        if (UserRepo.getByName(user.Email) == null)
        {
            throw new ArgumentException("User does not exist");
        }
        UserRepo.Remove(user);
    }
    public void updateUser(User user)
    {
        if (UserRepo.getByName(user.Email) == null)
        {
            throw new ArgumentException("User does not exist");
        }
        UserRepo.Update(user);
    }
}