using System.Reflection;
using Domain.Utilities;

namespace Domain.Infrastructure.Persistence;

public class RepositoryInMemory<T>: IList<T> where T : class
{
    protected readonly List<T> _entities = new List<T>();

    public IEnumerable<T> GetAll()
    {
        return _entities;
    }
    
    public virtual T getByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null");
        }
        foreach (T entity in _entities)
        {
            if (Equals(typeof(T).GetProperty("Name").GetValue(entity),name))
            {
                return entity;
            }
        }
        return null;
    }
    
    public void Add(T entity)
    {
        _entities.Add(entity);
    }

    public virtual void Update(T entity)
    {
        if (Equals(entity, null))
        {
            throw new ArgumentException("Invalid Entity");
        }

        string nameToUpdate = (string)typeof(T).GetProperty("Name").GetValue(entity); 
        
        T entityToUpdate = getByName(nameToUpdate);
        if (Equals(entityToUpdate, null))
        {
            throw new ArgumentException("Entity not found");
        }

        foreach (PropertyInfo property in typeof(T).GetProperties())
        {
            if (property.CanRead && property.CanWrite)
            {
                property.SetValue(entityToUpdate, property.GetValue(entity));
            }
        }
    }
    
    public void Remove(T entity)
    {
        _entities.Remove(entity);
    }
    
    
}