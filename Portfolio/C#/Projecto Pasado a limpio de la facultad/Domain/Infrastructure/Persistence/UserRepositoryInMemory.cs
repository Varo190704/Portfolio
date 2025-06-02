using System.Reflection;
using Domain.Utilities;

namespace Domain.Infrastructure.Persistence;

public class UserRepositoryInMemory : RepositoryInMemory<User> 
{
    public override User getByName(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Name cannot be null");
        }
        foreach (User entity in _entities)
        {
            if (Equals(typeof(User).GetProperty("Email").GetValue(entity),email))
            {
                return entity;
            }
        }
        return null;
    }
    
    public override void Update(User user)
    {
        if (Equals(user, null))
        {
            throw new ArgumentException("Invalid Entity");
        }

        string EmailToUpdate = user.Email; 
        
        User entityToUpdate = getByName(EmailToUpdate);
        if (Equals(entityToUpdate, null))
        {
            throw new ArgumentException("Entity not found");
        }

        foreach (PropertyInfo property in typeof(User).GetProperties())
        {
            if (property.CanRead && property.CanWrite)
            {
                property.SetValue(entityToUpdate, property.GetValue(user));
            }
        }
    }
}