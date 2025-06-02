using System.Reflection;
using Domain.Utilities;

namespace Domain.Infrastructure.Persistence;

public class AlertRepositoryInMemory : RepositoryInMemory<Alert>
{
    public Alert getById(int id)
    {
        if (id<=0)
        {
            throw new ArgumentException("Invalid id");
        }
        foreach (Alert entity in _entities)
        {
            if (entity.Id == id)
            {
                return entity;
            }
        }
        return null;
    }
    
    public override void Update(Alert alert)
    {
        if (Equals(alert, null))
        {
            throw new ArgumentException("Invalid Entity");
        }

        Alert entityToUpdate = getById(alert.Id);
    
        if (Equals(entityToUpdate, null))
        {
            throw new ArgumentException("Entity not found");
        }
        entityToUpdate.Message = alert.Message;
    }
}