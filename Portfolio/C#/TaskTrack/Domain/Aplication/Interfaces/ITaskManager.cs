using Domain.Utilities;
using Task = Domain.Utilities.Task;
    
namespace Domain.Aplication.Interfaces;

public interface ITaskManager
{
    IEnumerable<Task> getAllTasks();
    Task getTasksByName(String name);
    void createTask(Task project);
    void deleteTask(Task project);
    void updateTask(Task project);
    void AddDependency(Task task, Task dependency);
    void RemoveDependency(Task task, Task dependency);
    void AssResource(Task task, Resource resource);
    void DeleteResource(Task task, Resource resource);
    void RecalculateCriticalPath();
    void AlwaysUpdateTasks(List<Resource> updatedResources, List<Task> Tasks);
}