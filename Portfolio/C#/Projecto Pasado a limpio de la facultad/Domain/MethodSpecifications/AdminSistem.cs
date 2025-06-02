using Domain.Utilities;

namespace Domain.Specification;

public static class AdminSistem
{
    public static User Create(String name, String surname, String email, String password, DateOnly birthDate)
    {
        User user = new User(name, surname, email, password, birthDate);
        return user;
    }

    public static List<User> Delete(List<User> users, User user)
    {
        if (user == null || users == null)
        {
            throw new ArgumentException("User cannot be null");
        }
        else
        {
            List<User> newList = new List<User>();
        
            foreach (User u in users)
            {
                if (!Equals(u, user))
                {
                    newList.Add(u);
                }
            }
            users = newList;
        }
            
        return users;
    }

    public static void Update(List<User> users, User userWithNewData)
    {
        User existingUser = users.Find(u => u.Email == userWithNewData.Email);
        if (existingUser != null)
        {
            existingUser.Name = userWithNewData.Name;
            existingUser.Surname = userWithNewData.Surname;
            existingUser.Types = userWithNewData.Types;
            existingUser.Password = userWithNewData.Password;
            existingUser.BirthDate = userWithNewData.BirthDate;
            existingUser.AdminSistem = userWithNewData.AdminSistem;
        }
    }
}