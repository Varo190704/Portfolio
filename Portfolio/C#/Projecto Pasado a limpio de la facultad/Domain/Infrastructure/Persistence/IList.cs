using Domain.Utilities;

namespace Domain.Infrastructure.Persistence;

public interface IList<T> where T : class
{
    IEnumerable<T> GetAll();
    T getByName(string name);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
}